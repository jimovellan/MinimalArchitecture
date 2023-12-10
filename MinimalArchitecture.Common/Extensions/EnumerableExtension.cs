using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Common.Extensions
{
    public static class EnumerableExtension
    {
        /// <summary>
        /// Define if contains elements
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static bool HasElements<T>(this IEnumerable<T> enumerable)
        {
            return enumerable != null && enumerable.Any();
        }

        public static bool NoHasElement<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.HasElements();
        }

        /// <summary>
        /// Aggregation function async
        /// </summary>
        /// <typeparam name="TIn">Type of enter value</typeparam>
        /// <typeparam name="TAggregate">Type of outter value</typeparam>
        /// <param name="iterator">Enumerable value</param>
        /// <param name="initialValue">init value for acum</param>
        /// <param name="func">func to calculate the aggretate value</param>
        /// <returns></returns>
        public static async Task<TAggregate> AggregateAsync<TIn,TAggregate>(this IEnumerable<TIn> iterator,TAggregate initialValue,Func<TAggregate,TIn, Task<TAggregate>> func)
        {
            TAggregate acum = initialValue;
            
            foreach (var item in iterator)
            {
                initialValue = await func(acum, item);
            }

            return acum;
        }

    }
}
