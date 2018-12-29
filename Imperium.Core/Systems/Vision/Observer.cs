using Imperium.Ecs;

namespace Imperium.Core.Systems.Vision
{
    public class Observer : Component
    {
        public int VisionRange { get; set; } = 5;
    }
}