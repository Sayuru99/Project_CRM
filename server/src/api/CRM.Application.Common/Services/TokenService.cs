using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using SharedDomain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace CRM.Application.Common;

public class TokenService : ITokenService
{
    private readonly TokenConfiguration _tokenConfiguration;

    private readonly string _connectionString;

    public TokenService(IConfiguration configuration, IOptions<TokenConfiguration> tokenConfiguration)
    {
        _tokenConfiguration = tokenConfiguration.Value;
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(_connectionString));
    }

    public AccessToken GenerateToken(UserRequest user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim("Name", user.Name),
                new Claim("Email", user.Email),
                new Claim("IpAddress", user.IpAddress)
            }),
            Expires = DateTime.UtcNow.AddHours(_tokenConfiguration.ExpiresIn),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_tokenConfiguration.GetSecretAsByteArray()),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        DateTimeOffset expiresIn = tokenDescriptor.Expires.Value;

        return new AccessToken
        {
            Token = tokenHandler.WriteToken(token),
            ExpiresIn = int.Parse(expiresIn.ToUnixTimeSeconds().ToString())
        };
    }

    public UserRequest GetUserFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(_tokenConfiguration.GetSecretAsByteArray()),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false
        };

        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken == null ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return new UserRequest(
            new Guid(principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value),
            principal.Claims.FirstOrDefault(x => x.Type == "Name")?.Value,
            principal.Claims.FirstOrDefault(x => x.Type == "Email")?.Value,
            principal.Claims.FirstOrDefault(x => x.Type == "IpAddress")?.Value
        );
    }

    public string GenerateRefreshToken(UserRequest user)
    {
        var refreshInfo = new RefreshTokenInfo
        {
            ID = user.ID,
            Name = user.Name,
            Email = user.Email,
            IP = Environment.MachineName,
            CreationDate = DateTime.UtcNow
        };

        return TripleDes.Encrypt(_tokenConfiguration.GetSecretAsByteArray(), JsonSerializer.Serialize(refreshInfo));
    }

    public bool ValidateRefreshToken(string refreshToken, UserRequest user)
    {
        var refreshInfo = JsonSerializer.Deserialize<RefreshTokenInfo>(
            TripleDes.Decrypt(_tokenConfiguration.GetSecretAsByteArray(), refreshToken)
        );

        return refreshInfo.ID.ToString().ToUpper().Equals(user.ID.ToString().ToUpper()) &&
               refreshInfo.Name.ToUpper().Equals(user.Name.ToUpper()) &&
               refreshInfo.IP.ToUpper().Equals(Environment.MachineName.ToUpper());
    }

    public bool IsRefreshTokenExpired(string refreshToken)
    {
        var refreshInfo = JsonSerializer.Deserialize<RefreshTokenInfo>(
            TripleDes.Decrypt(_tokenConfiguration.GetSecretAsByteArray(), refreshToken)
        );

        return refreshInfo.CreationDate.AddHours(_tokenConfiguration.ExpiresIn * 2) < DateTime.UtcNow;
    }

    public async Task<UserRequest> GetDatabaseUser(AuthRequest request, string ip)
    {
        var sql = "SELECT \"Id\", \"Name\", \"Email\", \"PasswordHash\" FROM public.\"users\" WHERE \"Email\" = @email AND \"IsActive\" = true";

        var ID = Guid.Empty;
        var Name = string.Empty;
        var Email = string.Empty;
        var PasswordHash = string.Empty;

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@email", request.Email.ToLower());
            {
                var reader = await command.ExecuteReaderAsync();

                if (reader.Read())
                {
                    ID = reader.GetGuid(0);
                    Name = reader.GetString(1);
                    Email = reader.GetString(2);
                    PasswordHash = reader.GetString(3);
                }
            }
        }

        if (string.IsNullOrEmpty(Email) || !BCrypt.Net.BCrypt.Verify(request.Password, PasswordHash))
        {
            return new UserRequest();
        }

        return new UserRequest(ID, Name, Email, ip);
    }
}


