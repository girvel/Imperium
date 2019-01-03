namespace Imperium.Ecs
{
    public abstract class RegisteredComponent<TSystem, TThis> : Component 
        where TSystem : RegistrationSystem<TThis>
        where TThis : RegisteredComponent<TSystem, TThis>
    {
        public override void Start()
        {
            base.Start();
            
            Ecs.SystemManager.GetSystem<TSystem>().Register((TThis) this);
        }
    }
}