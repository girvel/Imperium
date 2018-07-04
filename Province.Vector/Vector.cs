﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Province.Vector
{
    public struct Vector : IEnumerable<Vector>
    {
        public readonly int X, Y;

        public int SquaredMagnitude => X * X + Y * Y;

        public double Magnitude => Math.Sqrt(SquaredMagnitude);



        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }



        public static Vector operator +(Vector v1, Vector v2) => new Vector(v1.X + v2.X, v1.Y + v2.Y);

        public static Vector operator -(Vector v) => new Vector(-v.X, -v.Y);

        public static Vector operator -(Vector v1, Vector v2) => v1 + -v2;

        public static Vector operator *(Vector v, float k) => new Vector((int) (v.X * k), (int) (v.Y * k));

        public static Vector operator *(float k, Vector v) => v * k;

        public static Vector operator /(Vector v, float d) => v * (1 / d);



        public static bool operator ==(Vector v1, Vector v2)
        {
            return v1.Equals(v2);
        }

        public static bool operator !=(Vector v1, Vector v2)
        {
            return !(v1 == v2);
        }



        public override bool Equals(object obj)
        {
            if (obj is Vector)
            {
                var v = (Vector) obj;
                return v.X == X && v.Y == Y;
            }

            return base.Equals(obj);
        }

        public bool Equals(Vector other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public override string ToString() => $"{{{X}; {Y}}}";

        
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Vector> GetEnumerator()
        {
            var X = this.X;
            return 
                Enumerable
                    .Range(0, Y)
                    .SelectMany(y => Enumerable.Range(0, X).Select(x => new Vector(x, y)))
                    .GetEnumerator();
        }
    }
}