using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using MinimalArchitecture.Api.configuration;
using MinimalArchitecture.Api.Extensions;
using MinimalArchitecture.Api.Middlewares;
using MinimalArchitecture.Architecture;
using MinimalArchitecture.Architecture.Cache;
using MinimalArchitecture.Entities.Authorization.Models;
using MinimalArchitecture.Entities.Posts.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOptions();
builder.Services.AddDistributedMemoryCache();
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);


builder.Services.AddAuthenticationCustom(builder.Configuration);
builder.Services.AddHttpContextAccessor();


//builder.Services.AddCarter();
builder.Services.AddAuthorization();

builder.Services.AddCacheDistributedSQLSERVER(builder.Configuration.GetConnectionString("app"),3);

//All configuration of architecture
//Mediator
//validations
//EntityFramework
//databases
MinimalArchitecture.Architecture.Startup.Configure(builder.Services,builder);



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

builder.Services.AddControllers().AddOData(opt =>
            opt.Select()
            .Filter().Expand().OrderBy().SetMaxTop(null).Count().AddRouteComponents("odata", GetEdmModel()));

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

app.AuthenticationAndAutenticationCustom();

#region Middlewares
app.UseMiddleware<LanguageMiddleware>();
app.UseMiddleware<CatchErrorMiddleware>();
#endregion

app.SeedDataNecesary();

app.MapControllers().RequireAuthorization();

app.Run();

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();

    //Add all odata endpoints

    modelBuilder.SetEntitiesFromAssempbly(Assembly.GetAssembly(typeof(Program)));
     
    return modelBuilder.GetEdmModel();
}