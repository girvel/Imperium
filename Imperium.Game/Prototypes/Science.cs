using Imperium.Core.Systems.Science;
using Imperium.Ecs;
using Imperium.Ecs.Managers;

namespace Imperium.Game.Prototypes
{
    public class Science : PrototypeContainer
    {
        public Research Test;
        
        protected override void InitializePrototypes(EcsManager ecs)
        {
            Test = new Research("test", 30, new Research[0]);
        }
    }
}