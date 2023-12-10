using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
