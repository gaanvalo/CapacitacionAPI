using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PersonasDAL.DALPersona;
using PersonasDTO.LogXNet;

namespace PersonasAPI
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
            //Inicializa Health Check
            services.AddHealthChecks();


            /*Inicializa Cadena conexion en el contructor del controlador*/
            services.AddControllers();

            services.AddTransient<IPersonaRepository>(x => new PersonaRepository(Configuration.GetConnectionString("Conexion"),
                                                           Convert.ToBoolean(Configuration.GetSection("ApiLog").GetSection("FlagLogInfo").Value)
                                                           )
                                                            );

            services.AddTransient<ILogAPI>(x => new Logger(Convert.ToBoolean(Configuration.GetSection("ApiLog").GetSection("FlagLogInfo").Value)));

            /*Inicializa Swagger*/
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PersonasAPI", Version = "v1" });
            });

            /*Inicializa JWT*/
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = Configuration["ApiAuth:Issuer"],
                      ValidAudience = Configuration["ApiAuth:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["ApiAuth:Key"]))
                  };
              });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            /*JWT Token*/
            app.UseAuthentication();

            /*Swagger*/
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "PersonasAPI");
            });

            //healthCheckSimple
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
            });
            // ******HEALTH CHECK WITH DB
            app.Use(async (context, func) =>
            {
                if (context.Request.Path.Value == "/hc")
                {
                    try
                    {
                        using (var db = new SqlConnection(Configuration.GetConnectionString("Conexion")))
                        {
                            await db.OpenAsync();
                            db.Close();
                        }

                        context.Response.StatusCode = 200;
                        context.Response.ContentLength = 7;
                        await context.Response.WriteAsync("HEALTHY");
                    }
                    catch (SqlException)
                    {
                        context.Response.StatusCode = 200;
                        context.Response.ContentLength = 20;
                        await context.Response.WriteAsync("SQL Connection Error");
                    }
                }
                else
                {
                    await func.Invoke();
                }
            });
        }
    }
}
