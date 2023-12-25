using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinimalArchitecture.Application.Features.Autorization.Login;
using MinimalArchitecture.Application.Services;
using MinimalArchitecture.Architecture.Config;
using MinimalArchitecture.Architecture.Pipelines;
using MinimalArchitecture.Architecture.Repository;
using MinimalArchitecture.Architecture.Services;
using MinimalArchitecture.Entities.Repository;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using MinimalArchitecture.Application.Features.Autorization.SeedMinimalData;
using Quartz;
using Microsoft.Extensions.Hosting;
using MinimalArchitecture.Architecture.Jobs;
using MinimalArchitecture.Architecture.Jobs.Common;

namespace MinimalArchitecture.Architecture
{
    public static class Startup
    {
        public static Assembly APPLICATION_ASSEMBLY =  Assembly.GetAssembly(typeof(LoginRequest)); 

        public static void Configure(IServiceCollection serviceCollection,WebApplicationBuilder builder)
        {
            
            LoadOptions(serviceCollection, builder.Configuration);
            ConfigureMediator(serviceCollection);
            ConfigureRepositories(serviceCollection,builder);
            ConfigureServices(serviceCollection);
            LoadScheeduleJobs(serviceCollection).Wait();
            
        }

        public static void LoadOptions(IServiceCollection serviceCollection, ConfigurationManager configuration)
        {
            
            serviceCollection.Configure<JWTSettings>(configuration.GetSection("jwt"));
        }

        public static void SeedDataNecesary(this WebApplication builder)
        {

            using (var scope = builder.Services.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetService<IMediator>();
                var result = mediator.Send(new SeedMinimalDataRequest()).Result;
            }
            

            //var result = mediator.Send(new SeedMinimalDataRequest()).Result;

            //if (result.IsFailed)
            //{
            //    throw new Exception("Error al crear los datos minimos");
            //}

            //var x = mediator.Send(new LoginRequest() { Password = "dd", User = "dd" }).Result;


        }


        public static async Task LoadScheeduleJobs(IServiceCollection services)
        {
           
                 services.AddQuartz(q =>
                 {
                     q.UseMicrosoftDependencyInjectionJobFactory();


                     //find all class IJobApplication from this assembly
                     var assembly = Assembly.GetAssembly(typeof(JobConfigurationAttribute));

                     var classes = assembly.GetTypes()
                                    .Where(type =>
                                        typeof(IJob).IsAssignableFrom(type) &&
                                        type.GetCustomAttributes(typeof(JobConfigurationAttribute), true).Length > 0
                                    );


                     foreach (var item in classes)
                     {
                         var jobkey = new JobKey(item.Name);
                         var attribute = (JobConfigurationAttribute)item.GetCustomAttributes(typeof(JobConfigurationAttribute), true).FirstOrDefault();
                        
                         q.AddJob(item, jobkey,opts => opts.WithIdentity(jobkey));
                         q.AddTrigger(opts => opts
                        .ForJob(jobkey) // link to the HelloWorldJob
                        .WithIdentity($"{item.Name}-trigger") // give the trigger a unique name
                        .WithCronSchedule(attribute.CronSchedule)); // run every 5 seconds
                     }




                     
                 });
                 services.AddQuartzHostedService(opt =>
                 {
                     opt.WaitForJobsToComplete = true;
                 });
             

            // will block until the last running job completes
            





        }

        /// <summary>
        /// Aply all migrations pending
        /// </summary>
        /// <param name="app"></param>
        public static void AplyMigrationsAuto(this WebApplication app)
        {
            using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDBContext>();
                dbContext.Database.Migrate();
            }
        }

       

        /// <summary>
        /// configuration de external services
        /// </summary>
        /// <param name="serviceCollection"></param>
        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IPasswordValidation, PasswordValidatorService>();
            serviceCollection.AddSingleton<IPasswordValidation, PasswordValidatorService>();
            serviceCollection.AddSingleton<ITokenService, JWTTokenService>();
        }

        /// <summary>
        /// configuration of repositories and Unit of works
        /// </summary>
        /// <param name="serviceCollection"></param>
        private static void ConfigureRepositories(IServiceCollection serviceCollection,WebApplicationBuilder builder)
        {
            
            serviceCollection.AddDbContext<AppDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("app"));
               
            },ServiceLifetime.Scoped);
            
            serviceCollection.AddScoped<ISpecificationResolver, SpecificationResolver>();
            serviceCollection.AddScoped(typeof(IRepositoryBase<>), typeof(SqlServerRepository<>));

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        /// <summary>
        /// configure mediator pattern
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureMediator(IServiceCollection services)
        {
           //register all handlers of MediaTR
            services.AddMediatR(config => config.RegisterServicesFromAssembly(APPLICATION_ASSEMBLY!));
            //Add pipeline mediatr's call
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatingPipelineBehavior<,>));
            //validations before call to handler of mediatr
            services.AddFluentValidation(config=> {
                config.RegisterValidatorsFromAssembly( APPLICATION_ASSEMBLY);
                });

        }

    }
}
