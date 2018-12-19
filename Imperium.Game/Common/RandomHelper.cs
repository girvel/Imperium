using System;
using Province.Vector;

namespace Imperium.Game.Common
{
    public static class RandomHelper
    {
        public static void Chance(this Random r, double chance, Action a)
        {
            if (r.NextDouble() < chance) a();
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