using System;
using Imperium.Ecs.Managers;

namespace Imperium.Core.Common
{
    public static class Time
    {
        public const float GameTimeCoefficient = 360;
        
        public static float DeltaTime(EcsManager ecs) => (float) ecs.UpdateDelay.TotalSeconds * GameTimeCoefficient;
    }
}