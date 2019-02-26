using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Province.Vector
{
    public class RoundPart<T> : IEnumerable<T>
    {
        public Vector Center { get; }
        public double Radius { get; }
        private readonly Vector Size;
        
        private readonly T[,] _internalArray;

        public RoundPart(T[,] arr, Vector center, double radius)
        {
            Center = center;
            Radius = radius;
            _internalArray = arr;

            Size = (int) radius * Vector.One;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            var minY = Math.Max(0, (Center - Size).Y);
            var maxY = Math.Min((Center + Size).Y + 1, _internalArray.GetLength(1));

            for (var y = minY; y < maxY; y++)
            {
                var q = (int) Math.Sqrt(Math.Pow(Size.X, 2) - Math.Pow(y - Center.Y, 2));
                
                var minX = Math.Max(0, Center.X - q);
                var maxX = Math.Min(Center.X + q, _internalArray.GetLength(0));
                
                for (var x = minX; x < maxX; x++) yield return _internalArray[x, y];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>) this).GetEnumerator();
        }
    }
}