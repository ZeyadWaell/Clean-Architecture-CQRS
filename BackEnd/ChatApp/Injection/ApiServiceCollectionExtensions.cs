using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace ChatApp.API.Injection
{
    public static class ApiServiceCollectionExtensions
    {
        /// <summary>
        /// Adds JWT authentication (issuer/audience disabled) and configures Swagger with a Bearer scheme.
        /// </summary>
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. Configure JWT Authentication
            services.AddAuthentication(options =>
            {
                // Use JWT as default
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtSection = configuration.GetSection("Jwt");
                var key = jwtSection["Key"];

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,        // ignore "iss"
                    ValidateAudience = false,      // ignore "aud"
                    ValidateLifetime = true,       // still check "exp"
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(key))
                };
            });

            // 2. Configure Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ChatApp API",
                    Version = "v1",
                    Description = "API documentation for the ChatApp."
                });

                // Define a Bearer scheme for JWT
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.\n" +
                                  "Enter: \"Bearer YOUR_TOKEN_HERE\""
                });

                // (Optional) If you want all endpoints to appear secured in Swagger:
                // c.AddSecurityRequirement(new OpenApiSecurityRequirement
                // {
                //     {
                //         new OpenApiSecurityScheme
                //         {
                //             Reference = new OpenApiReference
                //             {
                //                 Type = ReferenceType.SecurityScheme,
                //                 Id = "Bearer"
                //             }
                //         },
                //         Array.Empty<string>()
                //     }
                // });
            });

            return services;
        }
    }
}
