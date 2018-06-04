using System.Collections.Generic;
using Imperium.Core.Common;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Placing
{
    public class AreaSystem : ISystem
    {
        public List<PositionComponent>[,] Area { get; set; }

        public List<PositionComponent> this[Vector p] => Area[p.X, p.Y];

        

        public AreaSystem(Vector size)
        {
            Area = new List<PositionComponent>[size.X, size.Y];
            for (var x = 0; x < size.X; x++)
            {
                for (var y = 0; y < size.Y; y++)
                {
                    Area[x, y] = new List<PositionComponent>();
                }
            }
        }
        
        public void Move(PositionComponent component, Vector newPosition)
        {
            this[component.Position].Remove(component);
            component.Position = newPosition;
            this[component.Position].Add(component);
        }
        
        
        
        public void Update()
        {
            
        }
    }
}