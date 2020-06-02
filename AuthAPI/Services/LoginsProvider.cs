using AuthAPI.Data;
using AuthAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Services
{
    public class LoginsProvider
    {
        // https://www.c-sharpcorner.com/article/building-api-gateway-using-ocelot-in-asp-net-core-part-two/
        //private static IConfiguration configuration;
        public static async Task<JsonResult> GetToken(AuthAPIContext _context, IConfiguration configuration,
            string email, string password)
        {
            JsonResult response = new JsonResult("");
             var login = await _context.Login.FindAsync(email);  
 

           if (login != null && password.Equals(login.Password))
            {
                var now = DateTime.UtcNow;
                
                var secret = configuration.GetValue<string>("Audience:Secret");
                var iss = configuration.GetValue<string>("Audience:Iss");
                var aud = configuration.GetValue<string>("Audience:Aud");
                var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, login.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, login.Role),
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
                  //  RoleClaimType = login.Role  //// role client

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
                var personne =  await _context.Personnes.FindAsync(email);
                var responseJson = new
                {
                    access_token = encodedJwt,
                    personne = personne,
                    expires_in = (int)TimeSpan.FromMinutes(30).TotalSeconds
                };

                //////////////////
                response.Value =responseJson;
                response.StatusCode = 200;

            }
            else
            {
                response.Value = "UnAuthorized";
                response.StatusCode = 400;
                
            }
            return response;
        }


    }

    public class Audience
    {
        public string Secret { get; set; }
        public string Iss { get; set; }
        public string Aud { get; set; }
    }
}
