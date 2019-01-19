using System;
using System.Linq;

namespace Imperium.Core.Common
{
    public static class Mathf
    {
        public static T Max<T>(params T[] items) where T : struct, IComparable<T>
        {
            while (items.Length > 1)
            {
                items = (items[0].CompareTo(items.Last()) > 0 ? items.Take(items.Length - 1) : items.Skip(1)).ToArray();
            }
            return items[0];
        }

        public static T Min<T>(params T[] items) where T : struct, IComparable<T>
        {
            while (items.Length > 1)
            {
                items = (items[0].CompareTo(items.Last()) > 0 ? items.Skip(1) : items.Take(items.Length - 1)).ToArray();
            }
            return items[0];
        }
    }
}