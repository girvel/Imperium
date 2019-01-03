using Imperium.Ecs;

namespace Imperium.Core.Systems.Owning
{
    public class Owned : Component
    {
        private Player _owner;

        public Player Owner
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