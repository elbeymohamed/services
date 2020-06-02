using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProfessionnelAPI.Services
{
    public class Tools
    {
        internal static string getToken(IConfiguration configuration)
        {
                    var now = DateTime.UtcNow;

                    var secret = configuration.GetValue<string>("Audience:Secret");
                    var iss = configuration.GetValue<string>("Audience:Iss");
                    var aud = configuration.GetValue<string>("Audience:Aud");
                    var claims = new Claim[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub, "ProfessionnelAPI"),
                    new Claim(JwtRegisteredClaimNames.Sub, "ProfessionnelAPI"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
                    };

                    var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
                    var tokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingKey,
                        ValidateIssuer = true,
                        ValidIssuer = iss,
                        ValidateAudience = true,
                        ValidAudience = aud,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        RequireExpirationTime = true,

                    };

                    var jwt = new JwtSecurityToken(
                        issuer: iss,
                        audience: aud,
                        claims: claims,
                        notBefore: now,
                        expires: now.Add(TimeSpan.FromMinutes(30)),
                        signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                    );
                    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                    var responseJson = new
                    {
                        access_token = encodedJwt,
                        expires_in = (int)TimeSpan.FromMinutes(30).TotalSeconds
                    };
            return encodedJwt.ToString();

        }
    }
}
