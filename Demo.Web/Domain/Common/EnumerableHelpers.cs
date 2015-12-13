using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Web.Domain.Common
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Determines whether a sequence is null or contains no elements
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>
        /// True if the source sequence is null or contains no elements; otherwise false
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }

        /// <summary>
        /// Returns the specified number of elements starting at the beginning of the specified page
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="page">The 1-based number of the page to skip to</param>
        /// <param name="pageSize">The number of elements in a page</param>
        /// <returns></returns>
        public static IEnumerable<T> TakePage<T>(this IEnumerable<T> source, int page, int pageSize)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Determines whether a sequence contains any elements
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns>Returns true if the source sequence contains any elements; returns false if otherwise or if the source is null</returns>
        public static bool NullSafeAny<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null)
            {
                return false;
            }

            return source.Any(predicate);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns>An IEnumerable{T} that contains elements from the input sequence that satisfy the condition; returns an empty IEnumerable{T} if the source is null</returns>
        public static IEnumerable<T> NullSafeWhere<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null)
            {
                return Enumerable.Empty<T>();
            }

            return source.Where(predicate);
        }

        /// <summary>
        /// Projects each element of a sequence into a new form
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns>An IEnumerable{TResult} whose elements are the result of invoking the transform fucntion on each element of {TSource}; returns an empty IEnumerable{TResult} if the source is null</returns>
        public static IEnumerable<TResult> NullSafeSelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source == null)
            {
                return Enumerable.Empty<TResult>();
            }

            return source.Select(selector);
        }

        /// <summary>
        /// Returns the number of elements in a sequence
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>The number of elements in the input sequence; returns 0 if the source is null</returns>
        public static int NullSafeCount<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                return 0;
            }

            return source.Count();
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order according to a key
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns>An IEnumerable{TSource} whose elements are sorted according to a key or an empty IEnumerable{TSource} if the source is null</returns>
        public static IEnumerable<TSource> NullSafeOrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
            {
                return Enumerable.Empty<TSource>();
            }

            return source.OrderBy(keySelector);
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a key
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns>An IEnumerable{TSource} whose elements are sorted according to a key or an empty IEnumerable{TSource} if the source is null</returns>
        public static IEnumerable<TSource> NullSafeOrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
            {
                return Enumerable.Empty<TSource>();
            }

            return source.OrderByDescending(keySelector);
        }

        /// <summary>
        /// Performs the specified action on each element of the given sequence or does nothing the sequence is null
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        public static void NullSafeForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            if (source == null)
            {
                return;
            }

            foreach (TSource s in source)
            {
                action(s);
            }
        }

        /// <summary>
        /// Concatenates the members of the given sequence using the specified separator between each member
        /// </summary>
        /// <param name="source"></param>
        /// <param name="separator"></param>
        /// <returns>A string that consists of the members of the sequence delimited by the separator. Returns null if the sequence is null.</returns>
        public static string NullSafeJoin(this IEnumerable<string> source, string separator)
        {
            if (source == null)
            {
                return null;
            }

            return string.Join(separator, source);
        }

        public static void BatchForEach<TSource>(this IEnumerable<TSource> source, int batchSize, Action<IEnumerable<TSource>> action)
        {
            if (source.IsNullOrEmpty())
            {
                return;
            }

            int skip = 0;
            IEnumerable<TSource> batch = source.Skip(skip).Take(batchSize);
            while (batch.Any())
            {
                action(batch);
                skip += batchSize;
                batch = source.Skip(skip).Take(batchSize);
            }
        }
    }
}
