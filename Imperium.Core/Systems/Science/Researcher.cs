using Imperium.Core.Systems.Owning;
using Imperium.Ecs;
using Imperium.Ecs.Attributes;

namespace Imperium.Core.Systems.Science
{
    public class Researcher : Component
    {
        public double SciencePointsPerSecond { get; }
        
        public Researcher(double sciencePointsPerSecond)
        {
            SciencePointsPerSecond = sciencePointsPerSecond;
        }
    }
}