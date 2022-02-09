using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snt.Romashka.Repositories.Concretes
{
    public static class TaskHelpers
    {
        public static async Task<List<T>> ToListAsync<T>(this Task<IEnumerable<T>> source)
        {
            return (await source.ConfigureAwait(false)).ToList();
        }
        
        public static async Task<T[]> ToArrayAsync<T>(this Task<IEnumerable<T>> source)
        {
            return (await source.ConfigureAwait(false)).ToArray();
        }

        public static async Task<T> SingleOrDefault<T>(this Task<IEnumerable<T>> source)
        {
            return (await source.ConfigureAwait(false)).SingleOrDefault();
        }

        public static async Task<IEnumerable<T>> DistinctAsync<T>(this Task<IEnumerable<T>> source)
        {
            return (await source.ConfigureAwait(false)).Distinct();
        }

        public static Guid ToGuid(this string str)
        {
            return Guid.Parse(str);
        }

        public static Guid? ToNullableGuid(this string str)
        {
            if (!Guid.TryParse(str, out var id))
            {
                return null;
            }

            return id;
        }
    }
}