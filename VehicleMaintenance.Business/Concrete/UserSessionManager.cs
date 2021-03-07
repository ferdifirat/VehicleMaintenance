using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using VehicleMaintenance.Business.Abstract;
using VehicleMaintenance.Core.Common.Constant;
using VehicleMaintenance.Core.DataAccess;

namespace VehicleMaintenance.Business.Concrete
{
    public class UserSessionManager : IUserSessionService
    {
        private HttpContext _httpContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cacheManager;

        public UserSessionManager(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMemoryCache cacheManager)
        {
            _unitOfWork = unitOfWork;
            _httpContext = httpContextAccessor.HttpContext;
            _cacheManager = cacheManager;
        }

        public string GetEmail() => GetClaimValue(Constants.TokenConstant.Email);

        public string GetRoles() => GetClaimValue(Constants.TokenConstant.Roles);

        public string GetRoleId() => GetClaimValue(Constants.TokenConstant.PrimaryRoleId);

        public string GetRoleName() => GetClaimValue(Constants.TokenConstant.PrimaryRoleName);

        public int GetUserId() => Convert.ToInt32(GetClaimValue(Constants.TokenConstant.Id));

        public string GetUserName() => GetClaimValue(Constants.TokenConstant.UserName);

        public string GetUserFullName() => GetClaimValue(Constants.TokenConstant.Name);

        public string GetIpAddress() => _httpContext.Connection?.RemoteIpAddress?.ToString();

        private string GetClaimValue(string type)
        {
            try
            {
                var token = _httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }

                var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var claim = decodedToken.Claims.FirstOrDefault(x => x.Type == type);

                if (claim == null)
                {
                    return null;
                }

                if (string.IsNullOrEmpty(claim.Value))
                    return string.Empty;

                return claim.Value;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}