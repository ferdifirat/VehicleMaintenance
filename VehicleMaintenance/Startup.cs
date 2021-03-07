using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VehicleMaintenance.Business.CustomExtensions;
using VehicleMaintenance.Core.DataAccess;
using VehicleMaintenance.Core.DataAccess.EntityFramework;
using VehicleMaintenance.Core.DataAccess.UnitOfWork;
using VehicleMaintenance.Core.Entities;
using VehicleMaintenance.DataAccess.Concrete;

namespace VehicleMaintenance.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<VehicleMaintenanceDbContext>(options => options.UseLazyLoadingProxies()
                                                                                 .UseNpgsql(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("VehicleMaintenance.DataAccess")));

            services.AddScoped<DbContext>(provider => provider.GetService<VehicleMaintenanceDbContext>());
            services.AddScoped(typeof(IEntityRepository<>), typeof(efRepositoryBase<>));
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUnitOfWork, efUnitOfWork>();

            services.AddMvc();
            services.Configure<VehicleMaintenanceOptions>(Configuration);
            var vmOptions = Configuration.Get<VehicleMaintenanceOptions>();

            var key = Encoding.ASCII.GetBytes(vmOptions.Jwt.Key);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //services.AddValidationModule(Configuration);

            services.AddBusinessModule(Configuration);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
