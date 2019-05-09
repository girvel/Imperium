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


        public event Action<Placer> OnRemove, OnPlace;

        

        public Area(Vector size)
        {
            Size = size;
            Grid = size.CreateArray<List<Placer>>();
            foreach (var position in Size.Range())
            {
                Grid.SetAt(position, new List<Placer>());
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
            OnRemove?.Invoke(component);
        }

        public void Register(Placer component)
        {
            this[component.Position].Add(component);
            OnPlace?.Invoke(component);
        }
        
        

        public ContainerSlice ContainerSlice<T>()
            where T : PrototypeContainer
            => new ContainerSlice(
                this,
                Ecs.GetContainer<T>().Subjects.ToArray());
        
        public ComponentSlice<T> ComponentSlice<T>() where T : Component => new ComponentSlice<T>(this);
    }
}