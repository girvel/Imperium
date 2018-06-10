using Imperium.Ecs;

namespace Imperium.Core.Systems.Owning
{
    public class OwnedComponent : Component
    {
        private Player _owner;

        public Player Owner
        {
            get => _owner;
            set
            {
                _owner?.OwnedSubjects.Remove(this);

                _owner = value;

                _owner?.OwnedSubjects.Add(this);
            }
        }
    }
}