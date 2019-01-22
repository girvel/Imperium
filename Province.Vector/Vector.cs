using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Province.Vector
{
    public struct Vector
    {
        public static readonly Vector Zero = new Vector(0, 0), One = new Vector(1, 1);
        
        public int X, Y;

        public int SquaredMagnitude => X * X + Y * Y;

        public double Magnitude => Math.Sqrt(SquaredMagnitude);



        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public Vector Rotate90() => new Vector(-Y, X);



        public static Vector operator +(Vector v1, Vector v2) => new Vector(v1.X + v2.X, v1.Y + v2.Y);

        public static Vector operator -(Vector v) => new Vector(-v.X, -v.Y);

        public static Vector operator -(Vector v1, Vector v2) => v1 + -v2;

        public static Vector operator *(Vector v, int k) => new Vector(v.X * k, v.Y * k);

        public static Vector operator *(int k, Vector v) => v * k;

        public static Vector operator /(Vector v, int d) => v * (1 / d);



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



        public static Vector Clamp(Vector v, Vector min, Vector max)
            => v.X < min.X || v.Y < min.Y
                ? min
                : v.X > max.X || v.Y > max.Y
                    ? max
                    : v;

        public IEnumerable<Vector> Range()
        {
            var X = this.X;
            return 
                Enumerable
                    .Range(0, Y)
                    .SelectMany(y => Enumerable.Range(0, X).Select(x => new Vector(x, y)));
        }

        public bool IsSoftlyInside(Vector maximal) => X <= maximal.X && Y <= maximal.Y;
        
        public bool IsSoftlyInside(Vector minimal, Vector maximal) => IsSoftlyInside(maximal) && X >= minimal.X && Y >= minimal.Y;

        public bool IsInside(Vector maximal) => X < maximal.X && Y < maximal.Y;
        
        public bool IsInside(Vector minimal, Vector maximal) => IsSoftlyInside(maximal) && X > minimal.X && Y > minimal.Y;



        public static IEnumerable<Vector> Range(Vector from, Vector to) => (to - from).Range().Select(v => v + from);

        public static Vector Max(Vector first, Vector second) => first.IsSoftlyInside(second) ? second : first;

        public static Vector Min(Vector first, Vector second) => second.IsSoftlyInside(first) ? second : first;
        
        public static Vector PartialMax(Vector first, Vector second)
            => new Vector(
                Math.Max(first.X, second.X),
                Math.Max(first.Y, second.Y));
        
        public static Vector PartialMin(Vector first, Vector second)
            => new Vector(
                Math.Min(first.X, second.X),
                Math.Min(first.Y, second.Y));



        public static bool TryParse(string source, out Vector result)
        {
            var reg = Regex.Match(source, @"^\{([-+]?\d+)[;,]\s*([-+]?\d+)\}$");

            result 
                = reg.Success 
                    ? new Vector(int.Parse(reg.Groups[1].Value), int.Parse(reg.Groups[2].Value)) 
                    : default(Vector);

            return reg.Success;
        }
    }
}