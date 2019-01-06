namespace Imperium.Ecs
{
    public abstract class RegisteredComponent<TSystem, TThis> : Component 
        where TSystem : RegistrationSystem<TThis>
        where TThis : RegisteredComponent<TSystem, TThis>
    {
        public TSystem System { get; private set; }
        
        public override void Start()
        {
            base.Start();

            System = Ecs.SystemManager.GetSystem<TSystem>();
            System.Register((TThis) this);
        }
    }
}