using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MinimalArchitecture.Common.Extensions
{
    public static class ObjectExtensions
    {

        /// <summary>
        /// Convert de object to Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this Object obj)
        {
            byte[] jsonBytes = JsonSerializer.SerializeToUtf8Bytes(obj);
            return Encoding.UTF8.GetString(jsonBytes);
        }

        /// <summary>
        /// Throw an exception if the object's value is null
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ThrowExceptionIfNull(this Object obj, string name)
        {
            if (obj == null) throw new ArgumentNullException(name);
        }
    }
}
