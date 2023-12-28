using SharedDomain;

namespace CRM.Application.Common;

public interface ITokenService
{
    AccessToken GenerateToken(UserRequest user);
    Task<UserRequest> GetDatabaseUser(AuthRequest request, string ip);
    UserRequest GetUserFromToken(string token);
    string GenerateRefreshToken(UserRequest user);
    bool ValidateRefreshToken(string refreshToken, UserRequest user);
    bool IsRefreshTokenExpired(string refreshToken);
}
