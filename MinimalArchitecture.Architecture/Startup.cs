using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinimalArchitecture.Application.Features.Autorization.Login;
using MinimalArchitecture.Application.Services;
using MinimalArchitecture.Architecture.Pipelines;
using MinimalArchitecture.Architecture.Repository;
using MinimalArchitecture.Architecture.Services;
using MinimalArchitecture.Entities.Repository;
using System.Configuration;
using System.Reflection;

namespace MinimalArchitecture.Architecture
{
    public static class Startup
    {
        public static Assembly APPLICATION_ASSEMBLY =  Assembly.GetAssembly(typeof(LoginRequest)); 

        public static void Configure(IServiceCollection serviceCollection,WebApplicationBuilder builder)
        {
            ConfigureMediator(serviceCollection);
            ConfigureRepositories(serviceCollection,builder);
            ConfigureServices(serviceCollection);
        }

        /// <summary>
        /// configuration de external services
        /// </summary>
        /// <param name="serviceCollection"></param>
        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IPasswordValidation, PasswordValidatorService>();
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
