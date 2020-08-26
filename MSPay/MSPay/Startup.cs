using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MSPay.Services;
using Steeltoe.Discovery.Client;

namespace MSPay
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

            //services.AddDiscoveryClient(Configuration);

            services.Configure<ConsulConfig>(Configuration.GetSection("consulConfig"));

            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consultConfig => {
                var address = Configuration["consulConfig:address"];
                consultConfig.Address = new Uri(address);
            }));

            //services.AddSingleton<IUnitOfWork>(option => new UnitOfWork(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<IUnitOfWork>(option => new UnitOfWork(Configuration["connection_string"]));

            //var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["token:key"]));
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,

            //            ValidIssuer = Configuration["token:issuer"],
            //            ValidAudience = Configuration["token:audience"],
            //            IssuerSigningKey = signingKey
            //        };
            //        options.Events = new JwtBearerEvents
            //        {
            //            OnAuthenticationFailed = context =>
            //            {
            //                Console.WriteLine("OnAuthenticationFailed" + context.Exception.Message);
            //                return Task.CompletedTask;
            //            },
            //            OnTokenValidated = context =>
            //            {
            //                Console.WriteLine("OnTokenValidated" + context.SecurityToken);
            //                return Task.CompletedTask;
            //            },
            //        };
            //    });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();

            app.registerConsul(lifetime);

            //app.UseDiscoveryClient();
        }
    }
}
