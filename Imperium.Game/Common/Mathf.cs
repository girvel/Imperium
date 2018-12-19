using System;
using System.Linq;

namespace Imperium.Game.Common
{
    public static class Mathf
    {
        public static T Max<T>(params T[] items) where T : struct, IComparable<T>
        {
            while (true)
            {
                if (items.Length == 1) return items[0];
                items = (items[0].CompareTo(items.Last()) > 0 ? items.Take(items.Length - 1) : items.Skip(1)).ToArray();
            }
        }
    }
}