using System;
using System.Collections.Generic;
using System.Linq;

namespace QWMSServer.Tests.Extensions
{
    public static class CollectionExtensions
    {
        private static ICollection<TOut> CreateResultCollection<TIn, TOut>(IEnumerable<TIn> c, int size = 0)
        {
            Type collectionType;
            Boolean useSize = false;
            switch (c)
            {
                case SortedSet<TIn> _:
                    collectionType = typeof(SortedSet<TOut>);
                    break;
                case ISet<TIn> _:
                    collectionType = typeof(HashSet<TOut>);
                    break;
                default:
                    useSize = true;
                    collectionType = typeof(List<TOut>);
                    break;
            }

            if (useSize && size > 0)
            {
                return (ICollection<TOut>)Activator.CreateInstance(collectionType, new Object[] { size });
            }

            return (ICollection<TOut>)Activator.CreateInstance(collectionType);
        }

        /// <summary>
        /// Return those items of sequence for which function(item) is true.
        /// </summary>
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> collection, Func<T, Boolean> func)
        {
            ICollection<T> cRes = CreateResultCollection<T, T>(collection);
            foreach (T e in collection)
            {
                if (func(e) == true)
                {
                    cRes.Add(e);
                }
            }

            return cRes;
        }

        /// <summary>
        /// Return a list of the results of applying the function to the items of
        /// the argument sequence(s).
        /// </summary>
        public static IEnumerable<TOut> Map<TInput, TOut>(this IEnumerable<TInput> collection, Func<TInput, TOut> func)
        {
            ICollection<TOut> cRes = CreateResultCollection<TInput, TOut>(collection, collection.Count());
            foreach (TInput e in collection)
            {
                cRes.Add(func(e));
            }

            return cRes;
        }

        /// <summary>
        /// Return the first item which function(item) is true.
        /// </summary>
        public static T FindFirst<T>(this IEnumerable<T> collection, Func<T, Boolean> func)
        {
            ICollection<T> cRes = CreateResultCollection<T, T>(collection);
            foreach (T e in collection)
            {
                if (func(e) == true)
                {
                    return e;
                }
            }

            return default(T);
        }
    }
}
