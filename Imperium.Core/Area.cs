using System.Collections.Generic;
using Imperium.CommonData;
using Imperium.Core.Systems.Placing;
using Province.Vector;

namespace Imperium.Core
{
    public class Area
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
    }
}