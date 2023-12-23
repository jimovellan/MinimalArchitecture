using Carter;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using MinimalArchitecture.Api.EndPoints;
using MinimalArchitecture.Api.Middlewares;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MinimalArchitecture.Api.configuration;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddOptions();
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);


builder.Services.AddAuthenticationCustom(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddCarter();
builder.Services.AddAuthorization();
//All configuration of architecture
//Mediator
//validations
//EntityFramework
//databases
MinimalArchitecture.Architecture.Startup.Configure(builder.Services,builder);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minimal Architecture", Version = "v1" });

    // Ruta al archivo XML de comentarios
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();





MinimalArchitecture.Architecture.Startup.AplyMigrationsAuto(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( c=>
{
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API V1");
    });
}

app.MapCarter();

app.AuthenticationAndAutenticationCustom();

#region Middlewares
app.UseMiddleware<LanguageMiddleware>();
app.UseMiddleware<CatchErrorMiddleware>();
#endregion

app.Run();
