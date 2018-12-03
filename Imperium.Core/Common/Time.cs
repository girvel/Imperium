using Imperium.Ecs.Managers;

namespace Imperium.Core.Common
{
    public static class Time
    {
        public const float GameTimeCoefficient = 1.0f / 360;

        public static float GameSecondToUpdateCoefficient(EcsManager ecs) => (float) ecs.UpdateDelay.TotalSeconds * GameTimeCoefficient;
    }
}