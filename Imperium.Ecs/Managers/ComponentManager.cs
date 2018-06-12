namespace Imperium.Ecs.Managers
{
    public class ComponentManager : Manager<Component>
    {
        public Component CreateClone(Component original)
        {
            var clone = (Component) original.Clone();
            Register(clone);
            return clone;
        }
    }
}