using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MinimalArchitecture.Api.configuration
{
    public static class AuthConfig
    {
        public static AuthenticationBuilder AddAuthenticationCustom(this IServiceCollection services, ConfigurationManager configuration)
        {
            return services.AddAuthentication(options =>
             {
                 options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                 options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
             })
            .AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["jwt:Issuer"],
                    ValidAudience = configuration["jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration["jwt:Secret"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
        }

        public static void AuthenticationAndAutenticationCustom(this WebApplication app)
        {
            Boolean.TryParse(app.Configuration["auth:active"], out bool active);

            if (active)
            {
                app.UseAuthentication();
                app.UseAuthorization();
            }
        }
    }
}
