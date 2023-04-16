using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllInOneStockMarket
{
    public class Startup
    {
        public static String sqlConnectionString = "";
        public static string Jwtkey = "";
        public static string JwtIssuer = "";
        public static string JwtAudience = "";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            sqlConnectionString = Configuration.GetConnectionString("DB");
            Jwtkey = Configuration["Jwt:Key"];
            JwtIssuer = Configuration["Jwt:Issuer"];
            JwtAudience = Configuration["Jwt:Audience"];
            services.AddControllersWithViews().AddRazorOptions(option =>
            {// Add custom location to view search location
                option.ViewLocationFormats.Add("~/Views/{0}.cshtml");
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowCors", policy =>
                 {
                     policy.WithOrigins("localhost:44303").AllowAnyMethod().AllowAnyHeader();
                 }); 
            }); 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AllInOneStockMarket", Version = "v1" });
                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                //{
                //     Name = "Authorization",
                //     Type = SecuritySchemeType.ApiKey,
                //     Scheme = "Bearer",
                //     BearerFormat = "JWT",
                //     In = ParameterLocation.Header,
                //     Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`"
                //});
                //c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                //{
                //    new OpenApiSecurityScheme
                //    {
                //        Name = "Bearer",
                //        In = ParameterLocation.Header,
                //        Reference = new OpenApiReference
                //        {
                //            Type = ReferenceType.SecurityScheme,
                //            Id = "Bearer"
                //        }
                //    },
                //    new string[] {}
                //    }
                //});
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    //In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },

        },
        new List<string>()
    }
});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AllInOneStockMarket v1"));
            }

            // app.UseHttpsRedirection();
            app.UseCors("AllowCors");
            app.UseRouting();
            app.UseStatusCodePages();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
