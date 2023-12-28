using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Common.Extensions
{
    public static class TypeExtension
    {

        public static List<PropiedadInfo> GetStructureProperties(this Type type)
        {
            var estructura = new List<PropiedadInfo>();

            PropertyInfo[] propiedades = type.GetProperties();

            foreach (var propiedad in propiedades)
            {
                var nombre = propiedad.Name;
                var tipoPropiedad = ConvertirTipoJs(propiedad.PropertyType.Name);

                estructura.Add(new PropiedadInfo { Nombre = nombre, Tipo = tipoPropiedad });
            }

            return estructura;
        }

        // Función para convertir los tipos de C# a tipos entendibles por JavaScript
        static string ConvertirTipoJs(string tipoCSharp)
        {
            switch (tipoCSharp)
            {
                case "String":
                    return "string";
                case "Int32":
                    return "number";
                case "Boolean":
                    return "boolean";
                case "Double":
                    return "number";
                case "DateTime":
                    return "Date";
               
                default:
                    return tipoCSharp;
            }
        }

        public class PropiedadInfo
        {
            public string Nombre { get; set; }
            public string Tipo { get; set; }
        }
    }
}
