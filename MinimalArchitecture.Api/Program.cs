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
using Microsoft.AspNetCore.Mvc.Authorization;
using MinimalArchitecture.Architecture;

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
builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new OpenApiInfo { Title = "Minimal Architecture", Version = "v1" });

    // Ruta al archivo XML de comentarios
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    setup.IncludeXmlComments(xmlPath);

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
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

app.SeedDataNecesary();

app.Run();
