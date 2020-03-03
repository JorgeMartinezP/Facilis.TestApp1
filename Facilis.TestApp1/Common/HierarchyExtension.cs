using System;
using System.Collections.Generic;
using System.Linq;

namespace Facilis.TestApp1
{
    public static class HierarchyExtension
    {
        public static IEnumerable<T> SetHierarchy<T, S>(this IEnumerable<T> items,
            Func<T, S> getItemId,
            Func<T, S> getParentId,
            Action<T, T> assignParent,
            Action<T, T> assignChildToParent)
        {
            // This requires a dictionary where the key is the id (since the property name 'id' is unknown to this method)
            var dict = items.ToDictionary(getItemId);

            foreach (var item in dict.Values)
            {
                if (!dict.TryGetValue(getParentId(item), out var parent))
                {
                    yield return item;
                    continue;
                }

                assignParent(item, parent);
                assignChildToParent(item, parent);
            }
        }
    }
}
