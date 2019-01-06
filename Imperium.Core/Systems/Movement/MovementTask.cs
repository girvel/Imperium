using Imperium.Core.Systems.Placing;
using Province.Vector;

namespace Imperium.Core.Systems.Movement
{
    public class MovementTask
    {
        public Movable Movable { get; set; }
        
        public Vector To { get; set; }

        public Placer Placer => Movable.Parent.GetComponent<Placer>();
    }
}