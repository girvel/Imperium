using System.Collections.Generic;
using Province.Vector;

namespace Imperium.Core.Systems.Placing
{
    public class Area : Ecs.System
    {
        public Vector Size { get; set; }
        
        public List<PositionComponent>[,] Grid { get; set; }

        public List<PositionComponent> this[Vector p] => Grid[p.X, p.Y];

        

        public Area(Vector size)
        {
            Size = size;
            Grid = new List<PositionComponent>[size.X, size.Y];
            for (var x = 0; x < size.X; x++)
            {
                for (var y = 0; y < size.Y; y++)
                {
                    Grid[x, y] = new List<PositionComponent>();
                }
            }
        }
        
        public void Move(PositionComponent component, Vector newPosition)
        {
            Remove(component);
            component.Position = newPosition;
            Register(component);
        }

        public void Remove(PositionComponent component)
        {
            this[component.Position].Remove(component);
        }

        public void Register(PositionComponent component)
        {
            this[component.Position].Add(component);
        }
    }
}