using System.Text;

namespace CRM.Application.Common;

public class TokenConfiguration
{
    public string Secret { get; set; }
    public double ExpiresIn { get; set; }

    public static string Section => "Token";

    public byte[] GetSecretAsByteArray() => Encoding.ASCII.GetBytes(Secret);
}
