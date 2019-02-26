using System.Linq;
using Imperium.Core.Systems.Vision;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Ai
{
    public class AiSystem : RegistrationSystem<AutoAttack>
    {
        public override void Update()
        {
            base.Update();

            foreach (var entity in Subjects.Select(s => s.Parent))
            {
                var observer = entity.GetComponent<Observer>();
                
                
            }
        }
    }
}