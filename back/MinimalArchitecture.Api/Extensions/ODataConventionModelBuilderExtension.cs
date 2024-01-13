using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using MinimalArchitecture.Api.Controllers;
using System.Reflection;
using System.Data.Entity.Core.Metadata.Edm;

namespace MinimalArchitecture.Api.Extensions
{
    public static class ODataConventionModelBuilderExtension
    {

        /// <summary>
        /// Load all oData from controllers that inherit QueryableController
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="assembly"></param>
        public static void  SetEntitiesFromAssempbly(this ODataConventionModelBuilder modelBuilder, Assembly assembly)
        {
            Type genericType = typeof(QueryableController<>); // Reemplaza con el tipo genérico que estás buscando

            

            foreach (Type type in assembly.GetTypesInheritingFromGeneric(genericType))
            {
                modelBuilder.AddEntitySet(type, type.Name);  
            }


            
        }

        public static void AddEntitySet(this ODataConventionModelBuilder modelBuilder, Type entityType, string name)
        {
            var entitySetMethodInfo = typeof(ODataConventionModelBuilder).GetMethod("EntitySet", new[] { typeof(string) });

            if (entitySetMethodInfo != null)
            {
                var genericEntitySetMethod = entitySetMethodInfo.MakeGenericMethod(entityType);
                var entitySetConfiguration = genericEntitySetMethod.Invoke(modelBuilder, new object[] { name });

                // Puedes agregar más configuraciones según sea necesario
            }

        }

        private static IEnumerable<Type> GetTypesInheritingFromGeneric(this Assembly assembly, Type genericType)
        {
            var types =  assembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.BaseType != null
                               && type.BaseType.IsGenericType
                               && type.BaseType.GetGenericTypeDefinition() == genericType)
                .ToArray();

            foreach (var type in types)
            {
                yield return type.BaseType.GetGenericArguments().FirstOrDefault();
            }
            
        }
    }
}
