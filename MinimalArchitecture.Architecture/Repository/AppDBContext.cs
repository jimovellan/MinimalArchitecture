using Microsoft.EntityFrameworkCore;
using MinimalArchitecture.Entities.Authorization.Models;
using MinimalArchitecture.Entities.Posts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Architecture.Repository
{
    public class AppDBContext:DbContext
    {
        public DbSet<Rol> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag>  Tags { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public AppDBContext(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            FindAllConfigurationFiles(modelBuilder);

            Seed(modelBuilder);

        }

        /// <summary>
        /// Seed all data necesary to start application
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rol>().HasData(
            new Rol { Id = 1, Description = "Administrador", RolType = Entities.Authorization.Enums.RolType.All }

        // Agrega más datos según sea necesario...
        ); 
        }

        /// <summary>
        /// looking for all files of configuration into the actual Assembly
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void FindAllConfigurationFiles(ModelBuilder modelBuilder)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type.GetInterfaces().Any(interfaceType =>
                interfaceType.IsGenericType &&
                interfaceType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            // Crear instancias y aplicar configuraciones
            foreach (var type in types)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}
