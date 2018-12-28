using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Imperium.Ecs;
using Province.Vector;

namespace Imperium.Core.Systems.Placing
{
    public class Area : Ecs.System
    {
        public Vector Size { get; set; }
        
        public List<Placer>[,] Grid { get; set; }

        public List<Placer> this[Vector p] => Grid[p.X, p.Y];

        

        public Area(Vector size)
        {
            Size = size;
            Grid = new List<Placer>[size.X, size.Y];
            for (var x = 0; x < size.X; x++)
            {
                for (var y = 0; y < size.Y; y++)
                {
                    Grid[x, y] = new List<Placer>();
                }
            }
        }

        public const float MinimalTemperature = -15, MaximalTemperature = 20;
        public float GetTemperature(Vector position)
        {
            return
                (MinimalTemperature - MaximalTemperature) *
                Math.Abs(2 * (position.Y + position.X) / (float) (Size.X + Size.Y - 2) - 1) + MaximalTemperature;
        }
        
        public void Move(Placer component, Vector newPosition)
        {
            Remove(component);
            component.Position = newPosition;
            Register(component);
        }

        public void Remove(Placer component)
        {
            this[component.Position].Remove(component);
        }

        public void Register(Placer component)
        {
            this[component.Position].Add(component);
        }
        
        

        public AreaSlice Slice<T>()
            => new AreaSlice(
                this,
                typeof(T)
                    .GetFields(BindingFlags.Static | BindingFlags.Public)
                    .Select(f => f.GetValue(null))
                    .Cast<Entity>()
                    .ToArray());
    }
}