using CRM.Application.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedDomain.Extensions;

namespace CRM.Application.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        /// <summary>API Availability Test</summary>
        /// <remarks>This endpoint allows checking if the API is available.</remarks>
        /// <returns></returns>
        /// <response code="200">pong</response>
        [HttpGet("ping")]
        [AllowAnonymous]
        public string Ping()
        {
            return "pong";
        }

        /// <summary>Request Access Token</summary>
        /// <remarks>This endpoint allows requesting an access token for the API.</remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">{ "accessToken", "refreshToken", "expiresIn", "tokenType" }</response>
        /// <response code="400">{ "message": "Invalid username or password" }</response>
        [HttpPost("token")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromForm] AuthRequest request)
        {
            var user = await _tokenService.GetDatabaseUser(request, Request.GetRequestIP());

            if (string.IsNullOrEmpty(user.ID.ToString()))
            {
                return BadRequest(new { message = "Invalid username or password" });
            }

            var accessToken = _tokenService.GenerateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken(user);

            return new TokenResponse()
            {
                AccessToken = accessToken.Token,
                ExpiresIn = accessToken.ExpiresIn,
                RefreshToken = refreshToken
            };
        }

        /// <summary>Check Access Token Validity</summary>
        /// <remarks>This endpoint allows checking if the access token is still valid.</remarks>                       
        /// <returns></returns>
        /// <response code="200">{ message = "Token is valid" }</response>
        /// <response code="401"></response>
        [HttpGet("validate")]
        [Authorize]
        public IActionResult ValidateToken()
        {
            return Ok(new { message = "Token is valid" });
        }

        /// <summary>User Logout</summary>
        /// <remarks>This endpoint allows user logout.</remarks>
        /// <returns></returns>
        /// <response code="200">{ message = "User logged out!" }</response>
        [HttpDelete("logout")]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            return Ok(new { message = "User logged out!" });
        }

        /// <summary>Refresh Access Token</summary>
        /// <remarks>This endpoint allows requesting the refresh of an access token for the API.</remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">{ "accessToken", "refreshToken", "expiresIn", "tokenType" }</response>
        /// <response code="400">{ "message": "Invalid user or refreshToken" }</response>
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public ActionResult<dynamic> Refresh([FromForm] RefreshTokenRequest request)
        {
            var user = _tokenService.GetUserFromToken(request.Token);

            if (!user.Email.ToUpper().Equals(request.Email.ToUpper()) || !_tokenService.ValidateRefreshToken(request.RefreshToken, user))
            {
                return BadRequest(new { message = "Invalid user or refreshToken" });
            }

            if (_tokenService.IsRefreshTokenExpired(request.RefreshToken))
            {
                return BadRequest(new { message = "RefreshToken expired" });
            }

            var accessToken = _tokenService.GenerateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken(user);

            return new TokenResponse()
            {
                AccessToken = accessToken.Token,
                ExpiresIn = accessToken.ExpiresIn,
                RefreshToken = refreshToken
            };
        }
    }
}
