using System;
using System.Collections.Generic;
using System.Reflection;

namespace Furlong
{
    /// <summary>
    /// Uses reflection to unfold a Chain (of Responsibility).
    /// </summary>
    public static class ChainInspector
    {
        /// <summary>
        /// Unfold a Chain (of Responsibility) into its individual links.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="link"></param>
        /// <returns></returns>
        public static IEnumerable<T> UnfoldChain<T>(T link) where T : class
        {
            T GetSuccessor(T value)
            {
                Type type = value.GetType();
                FieldInfo field = null;

                while (type != null && field == null)
                {
                    field = type.GetField("_successor", BindingFlags.Instance | BindingFlags.NonPublic);

                    if (field == null)
                    {
                        type = type.BaseType;
                    }
                }

                return field?.GetValue(value) as T;
            }

            var result = new List<T> { link };
            T next = GetSuccessor(link);

            while (next != null)
            {
                result.Add(next);
                next = GetSuccessor(next);
            }

            return result;
        }
    }
}