using Carter;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using MinimalArchitecture.Api.EndPoints;
using MinimalArchitecture.Api.Middlewares;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();



builder.Services.AddCarter();

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




if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( c=>
{
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API V1");
    });
}

app.MapCarter();

#region Middlewares
app.UseMiddleware<LanguageMiddleware>();
app.UseMiddleware<CatchErrorMiddleware>();
#endregion






app.Run();