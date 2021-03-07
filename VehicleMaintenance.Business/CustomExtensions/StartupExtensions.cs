using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Business.Abstract;
using VehicleMaintenance.Business.Concrete;
using VehicleMaintenance.DataAccess.Abstract;
using VehicleMaintenance.DataAccess.Concrete;

namespace VehicleMaintenance.Business.CustomExtensions
{
    public static class StartupExtensions
    {

        public static void AddBusinessModule(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ITokenService, TokenManager>();

            services.AddScoped<IUserSessionService, UserSessionManager>();

            services.AddTransient<TokenAttribute>();

            services.AddScoped<IActionTypeService, ActionTypeManager>();
            services.AddScoped<IActionTypeDal, efActionTypeDal>();

            services.AddScoped<IMaintenanceHistoryService, MaintenanceHistoryManager>();
            services.AddScoped<IMaintenanceHistoryDal, efMaintenanceHistoryDal>();

            services.AddScoped<IMaintenanceService, MaintenanceManager>();
            services.AddScoped<IMaintenanceDal, efMaintenanceDal>();

            services.AddScoped<IPictureGroupService, PictureGroupManager>();
            services.AddScoped<IPictureGroupDal, efPictureGroupDal>();

            services.AddScoped<IVehicleTypeService, VehicleTypeManager>();
            services.AddScoped<IVehicleTypeDal, efVehicleTypeDal>();


            services.AddScoped<IStatusService, StatusManager>();
            services.AddScoped<IStatusDal, efStatusDal>();

            services.AddScoped<IUserService,UserManager>();
            services.AddScoped<IUserDal, efUserDal>();


            services.AddScoped<IVehicleService, VehicleManager>();
            services.AddScoped<IVehicleDal, efVehicleDal>();


        }
    }
}
