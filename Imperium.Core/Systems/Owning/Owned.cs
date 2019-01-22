using Imperium.Ecs;

namespace Imperium.Core.Systems.Owning
{
    public class Owned : Component
    {
        private Owner _owner;

        public Owner Owner
        {
            get => _owner;
            set
            {
                _owner?.RemoveOwned(this);

                _owner = value;

                _owner?.AddOwned(this);
            }
        }
    }
}