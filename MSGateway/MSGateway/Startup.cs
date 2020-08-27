using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jaeger;
using Jaeger.Samplers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using OpenTracing;
using OpenTracing.Contrib.NetCore.CoreFx;
using OpenTracing.Util;

namespace MSGateway
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
            services.AddCors(o => o.AddPolicy("MyPolicyCore", builder =>
            {
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
            }));

            services.Configure<MvcOptions>(options => 
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("MyPolicyCore"));
            });

            services.AddOcelot(Configuration);

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["token:key"]));
            services.AddAuthentication()
                .AddJwtBearer("SECURITY-TOKEN", options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,

                         ValidIssuer = Configuration["token:issuer"],
                         ValidAudience = Configuration["token:audience"],
                         IssuerSigningKey = signingKey
                     };
                     options.Events = new JwtBearerEvents
                     {
                         OnAuthenticationFailed = context =>
                         {
                             Console.WriteLine("OnAuthenticationFailed" + context.Exception.Message);
                             return Task.CompletedTask;
                         },
                         OnTokenValidated = context =>
                         {
                             Console.WriteLine("OnTokenValidated" + context.SecurityToken);
                             return Task.CompletedTask;
                         },
                     };
                 });

            services.AddSingleton<ITracer>(serviceProvider => {

                string serviceName = "SERVICE-GATEWAY";

                ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                ISampler sampler = new ConstSampler(sample: true);

                ITracer tracer = new Tracer.Builder(serviceName)
                    .WithLoggerFactory(loggerFactory)
                    .WithSampler(sampler)
                    .Build();

                GlobalTracer.Register(tracer);

                return tracer;

            });

            services.AddOpenTracing();

            services.Configure<HttpHandlerDiagnosticOptions>(options =>
            {
                options.IgnorePatterns.Add(x => !x.RequestUri.IsLoopback);
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseCors("MyPolicyCore");

            await app.UseOcelot();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
