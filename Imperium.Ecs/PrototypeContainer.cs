using System.Linq;
using System.Reflection;
using Imperium.Ecs.Attributes;
using Imperium.Ecs.Managers;

namespace Imperium.Ecs
{
    public abstract class PrototypeContainer : Manager<Entity>
    {
        public void Initialize(EcsManager ecs)
        {
            InitializePrototypes(ecs);

            foreach (var entity in GetType()
                .GetFields(BindingFlags.Public | BindingFlags.Instance)
                .Select(f => f.GetValue(this))
                .OfType<Entity>())
            {
                Register(entity);
            }
        }
        
        protected abstract void InitializePrototypes(EcsManager ecs);
    }
}