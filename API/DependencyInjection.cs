using API.Mappings;
using API.Middleware;
using Application.Mappings;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // Add controllers
            services.AddControllers();

            // Add DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // ✅ CONFIGURACIÓN MANUAL DE AUTOMAPPER (Sin paquete deprecado)
            services.AddSingleton<AutoMapper.IConfigurationProvider>(sp =>
            {
                return new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<ApiAutoMapperProfile>();
                    cfg.AddProfile<ApplicationAutoMapperProfile>();
                });
            });

            services.AddScoped<IMapper>(sp =>
            {
                var config = sp.GetRequiredService<AutoMapper.IConfigurationProvider>();
                return new Mapper(config);
            });

            // Add JWT Authentication
            var jwtSettings = configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            // Add Authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("Preceptor", policy => policy.RequireRole("Preceptor"));
                options.AddPolicy("Tutor", policy => policy.RequireRole("Tutor"));
                options.AddPolicy("Guard", policy => policy.RequireRole("Guard"));
                options.AddPolicy("Resident", policy => policy.RequireRole("Resident"));
            });

            // Add CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            // Add Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notification API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            // Add logging
            services.AddLogging(logging =>
            {
                logging.AddConsole();
                logging.AddDebug();
            });

            return services;
        }

        public static IApplicationBuilder UseApiLayer(this IApplicationBuilder app)
        {
            var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notification API V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseMiddleware<JwtMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}