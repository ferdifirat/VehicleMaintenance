using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VehicleMaintenance.Business.Abstract;
using VehicleMaintenance.Core.Common.Constant;
using VehicleMaintenance.Core.Entities;
using VehicleMaintenance.Entity.Concrete;
using VehicleMaintenance.Entity.DTOs;

namespace VehicleMaintenance.Business.Concrete
{
    public class TokenManager : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly VehicleMaintenanceOptions _options;

        public TokenManager(IConfiguration configuration,
            IOptions<VehicleMaintenanceOptions> options
        )
        {
            _options = options.Value;
            _configuration = configuration;
        }

        public TokenDto CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_options.Jwt.Key);

            var expireDate = DateTime.UtcNow.AddHours(_options.Jwt.ExpireHours).AddMinutes(_options.Jwt.ExpireMinutes).AddSeconds(_options.Jwt.ExpireSeconds);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(Constants.TokenConstant.Email, user.Email),
                    new Claim(Constants.TokenConstant.Name, user.FirstName),
                    new Claim(Constants.TokenConstant.Id, user.ID.ToString()),
                    new Claim(ClaimTypes.Expired, DateTime.Now.ToLongDateString())

                }),
                Expires = expireDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
                    , SecurityAlgorithms.HmacSha512Signature)
            };



            var token = tokenHandler.CreateToken(tokenDescriptor);
            var userToken = new TokenDto()
            {
                ExpireDate = expireDate,
                Token = tokenHandler.WriteToken(token)
            };
            return userToken;
        }

        public bool ValidToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_options.Jwt.Key);
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                SecurityToken validatedToken;
                var tokenParameters = TokenValidationParameters.DefaultAuthenticationType;
                var user = tokenHandler.ValidateToken(token, parameters, out validatedToken);
                if (user != null)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {

                return false;
            }

        }
    }
}
