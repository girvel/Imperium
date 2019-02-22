namespace Imperium.Ecs.Managers
{
    public class ComponentManager : Manager<Component>
    {
        public override void Register(Component subject)
        {
            base.Register(subject);

            subject.Start();
        }

        public override void Unregister(Component subject)
        {
            base.Unregister(subject);

            subject.Destroy();
        }
    }
}