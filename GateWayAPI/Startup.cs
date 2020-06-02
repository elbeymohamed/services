using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GateWayAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GateWayAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //// var audienceConfig = Configuration.GetSection("Audience");
            // var secretKey = Configuration.GetValue<string>("AppSettings:ServiceApiKey"); // get the secret key from appsettings


            // var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            // var tokenValidationParameters = new TokenValidationParameters
            // {
            //     ValidateIssuerSigningKey = true,
            //     IssuerSigningKey = signingKey,
            //     // ValidateIssuer = true,
            //     //   ValidIssuer = audienceConfig["Iss"],
            //     // ValidateAudience = true,
            //     //  ValidAudience = audienceConfig["Aud"],
            //     ValidateLifetime = true,
            //     ClockSkew = TimeSpan.Zero,
            //     RequireExpirationTime = true,
            // };

            //services.AddAuthentication()
            //        .AddJwtBearer("TestKey", x =>
            //        {
            //            x.RequireHttpsMetadata = false;
            //            x.TokenValidationParameters = tokenValidationParameters;
            //        });

            //
            var audienceConfig = Configuration.GetSection("Audience");

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfig["Secret"]));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Iss"],
                ValidateAudience = true,
                ValidAudience = audienceConfig["Aud"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = "TestKey";
            })
            .AddJwtBearer("TestKey", x =>
            {
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = tokenValidationParameters;
            });

            ////////////////////////////////////////////////////////////////

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
                });
            });


            services.AddControllers();

            services.AddDbContext<GateWayDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("GateWayDbContext")));
            services.AddOcelot(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(MyAllowSpecificOrigins);
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            await app.UseOcelot();
        }
    }
}
