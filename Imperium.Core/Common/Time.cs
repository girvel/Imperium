using System;
using Imperium.Ecs.Managers;

namespace Imperium.Core.Common
{
    public static class Time
    {
        public const float GameTimeCoefficient = 1.0f / 360;

        public static float UpdateGameTimeCoefficient(EcsManager ecs) => (float) ecs.UpdateDelay.TotalSeconds * GameTimeCoefficient;
    }
}