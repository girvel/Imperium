using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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