using System;

namespace Imperium.Game.Common
{
    public static class RandomHelper
    {
        public static void Chance(this Random r, double chance, Action a)
        {
            if (r.NextDouble() < chance) a();
        }
    }
}