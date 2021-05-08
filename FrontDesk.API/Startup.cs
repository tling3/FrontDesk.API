using AutoMapper;
using FrontDesk.API.Data.Context;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Reflection;

namespace FrontDesk.API
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
            services.AddDbContext<FrontDeskContext>(option => option.UseSqlServer
                (Configuration.GetConnectionString("FrontDeskConnection")));

            services.AddCors(options => {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IMemberRepo, SqlMemberRepo>();
            services.AddScoped<ISessionRepo, SqlSessionRepo>();
            services.AddScoped<IAttendanceRepo, SqlAttendanceRepo>();
            services.AddScoped<IWeekdayRepo, SqlWeekdayRepo>();
            services.AddScoped<IMembershipTypeRepo, SqlMembershipTypeRepo>();

            var contact = new OpenApiContact()
            {
                Name = "Example Name",
                Email = "example@example.com",
                Url = new Uri("https://swagger.io/")
            };

            var license = new OpenApiLicense()
            {
                Name = "Example License",
                Url = new Uri("https://swagger.io/")
            };

            var info = new OpenApiInfo()
            {
                Version = "v1",
                Title = "Front Desk API",
                Description = "API endpoints for Front Desk Application",
                TermsOfService = new Uri("https://swagger.io/"),
                Contact = contact,
                License = license
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", info);

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                // Enables duplicate class names with different namespaces
                c.CustomSchemaIds(x => x.FullName);

                c.DocumentFilter<JsonPatchDocumentFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger API V1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
