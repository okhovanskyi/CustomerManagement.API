using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CustomerManagement.API.Utilities
{
    /// <summary>
    /// ONLY FOR TESTING!
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class JwtTokenUtility
    {
        private const string BearerString = "Bearer";
        public static RsaSecurityKey KeyPair;

        static JwtTokenUtility()
        {
            KeyPair = new RsaSecurityKey(RSA.Create());
        }

        public static string GenerateCustomJwtToken(Guid customerUid)
        {            
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var claims = new ClaimsIdentity(new[] { new Claim("customerUid", customerUid.ToString()) });

            var jwtToken = new JwtSecurityToken(
                issuer: "https://localhost:44344",
                audience: "https://localhost:44344",
                claims: claims.Claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(KeyPair, SecurityAlgorithms.RsaSha256));
            
            return $"{BearerString} {jwtSecurityTokenHandler.WriteToken(jwtToken)}";
        }
    }
}
