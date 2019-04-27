using System;
using System.Text;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Core.Base
{
    internal class Token: IToken
    {
        private string _authorizationKey => "Authorization";

        private SymmetricSecurityKey _symmetricSecurityKey;

        private JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public Token()
        {
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        private string Encode(params Claim[] claims)
        {
            _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(Host.Config.Token.SecurityKey));

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = Host.Config.Token.Issuer,
                Audience = Host.Config.Token.Audience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(Host.Config.Token.Minute),
                SigningCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature),
                EncryptingCredentials = new EncryptingCredentials(_symmetricSecurityKey, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256)
            };

            SecurityToken securityToken = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);

            return _jwtSecurityTokenHandler.WriteToken(securityToken);
        }

        private T Cast<T>(Claim[] claims)
        {
            return JsonConvert.DeserializeObject<T>(claims.FirstOrDefault(x => x.Type == "___").Value);
        }

        private Claim[] Decode(string token = "")
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    token = HttpContext.Current.Request.Headers.GetValues(_authorizationKey).FirstOrDefault();
                }

                _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(Host.Config.Token.SecurityKey));

                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true,
                    RequireSignedTokens = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = Host.Config.Token.Issuer,
                    ValidAudience = Host.Config.Token.Audience,
                    IssuerSigningKey = _symmetricSecurityKey,
                    TokenDecryptionKey = _symmetricSecurityKey,
                };

                SecurityToken securityToken;
                ClaimsPrincipal claimsPrincipal = _jwtSecurityTokenHandler.ValidateToken(token.Replace("\"", "").Replace("/",""), validationParameters, out securityToken);

                if (claimsPrincipal.Identity.IsAuthenticated)
                {
                    return claimsPrincipal.Claims.ToArray();
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public string Encode<T>(T payload) where T : IPayLoad
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim("___", JsonConvert.SerializeObject(payload)));

            return Encode(claims.ToArray());
        }

        public T Decode<T>(string token = "") where T : IPayLoad
        {
            Claim[] claims = Decode(token);

            if (claims == null)
            {
                return default(T);
            }

            return Cast<T>(claims);
        }
    }
}