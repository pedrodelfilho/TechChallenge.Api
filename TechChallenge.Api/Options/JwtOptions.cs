using Microsoft.IdentityModel.Tokens;

namespace TechChallenge.Api.Options;

/// <summary>
/// 
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// 
    /// </summary>
    public string Issuer { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Audience { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public SigningCredentials SigningCredentials { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int AccessTokenExpiration { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int RefreshTokenExpiration { get; set; }
}

