using System;
using Province.Vector;

namespace Imperium.Game.Common
{
    public static class RandomHelper
    {
        public static bool Chance(this Random r, double chance)
        {
            return r.NextDouble() < chance;
        }

        public static Vector NextPosition(this Random r, Vector size)
        {
            return new Vector(r.Next(size.X), r.Next(size.Y));
        }

        public static Vector NextPosition(this Random r, Vector minimal, Vector size)
        {
            return new Vector(r.Next(minimal.X, size.X), r.Next(minimal.Y, size.Y));
        }
    }
}