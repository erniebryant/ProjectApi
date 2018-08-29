using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProjectApi.Data;
using ProjectApi.Data.Core;
using ProjectApi.Filters;
using ProjectApi.Helpers;
using ProjectApi.Mappings;
using ProjectApi.Service;
using Swashbuckle.AspNetCore.Swagger;

namespace ProjectApi
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;
        private readonly IHostingEnvironment _environment;
        private readonly IConfiguration _configuration;

        public Startup(ILogger<Startup> logger, IConfiguration configuration, IHostingEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
            _configuration = configuration;
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<UrlsConfig>(_configuration.GetSection("urls"));
            services.AddMvc(options =>
            {
                options.Filters.Add(new HttpResponseCodeFilter());
                options.Filters.Add(new ValidatorActionFilter());
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // EF Context Registration
            services.AddDbContext<APIContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(IISDefaults.AuthenticationScheme);
            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdmin", policy => policy.RequireRole(_configuration.GetSection("AdminGroup").Value));
            });

            // Define CORS Policy
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    _builder =>
                    _builder.AllowAnyOrigin()
                    //.WithOrigins("my://explicitly-trusted.url")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddAutoMapper(Assembly.GetAssembly(typeof(DomainToDTOMappingProfile)));
            services.AddAutoMapper(Assembly.GetAssembly(typeof(DomainToViewModelMappingProfile)));

            services.AddScoped<IMediator, Mediator>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDbContext, APIContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ServiceFactory>(sp => t => sp.GetService(t));
            services.AddMediatorHandlers(typeof(Startup).GetTypeInfo().Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "APUS SDU API", Version = "v2" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            // Ensure EF Db is created
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<APIContext>();
                context.Database.EnsureCreated();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "APUS SDU API V2");
            });

            // Apply CORS Policy
            app.UseCors("CorsPolicy");

        }
    }
}
