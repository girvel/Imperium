using System;
using System.Collections;
using System.Collections.Generic;
using static System.Diagnostics.Debug;

namespace Province.Vector
{
    public class RectanglePart<T> : IEnumerable<T>
    {
        private readonly T[,] _internalArray;

        public Vector Begin { get; }

        public Vector End { get; }



        public RectanglePart(T[,] internalArray, Vector begin, Vector end)
        {
            _internalArray = internalArray;
            Begin = begin;
            End = end; 
        }



        public T FindNearest(
            Predicate<T> match,
            Func<int, int, double> calculateDistance = null)
        {
            if (calculateDistance == null)
            {
                calculateDistance = (dx, dy) => Math.Max(dx, dy);
            }

            var size = End - Begin;

            Assert(
                size.X != size.Y || size.X % 1 == 0,
                "Area should be square and its size should be odd");

            var maxDistance = (size.X + 1) / 2;
            
            for (var d = 0; d <= maxDistance; d++)
            {
                
            }

            return default(T);
        }



        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            for (var y = Math.Max(0, Begin.Y); y < Math.Min(End.Y + 1, _internalArray.GetLength(1)); y++)
            for (var x = Math.Max(0, Begin.X); x < Math.Min(End.X + 1, _internalArray.GetLength(0)); x++)
            {
                yield return _internalArray[x, y];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>) this).GetEnumerator();
        }
    }
}
