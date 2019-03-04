using System;

namespace Province.Vector
{
    public static class ArrayHelper
    {
        public static T GetAt<T>(this T[,] arr, Vector position) => arr[position.X, position.Y];
        
        public static T GetAt<T>(this T[][] arr, Vector position) => arr[position.Y][position.X];
        
        public static void SetAt<T>(this T[,] arr, Vector position, T value) => arr[position.X, position.Y] = value;
        
        public static void SetAt<T>(this T[][] arr, Vector position, T value) => arr[position.Y][position.X] = value;
        
        
        
        public static Vector Size<T>(this T[,] arr) => new Vector(arr.GetLength(0), arr.GetLength(1));

        public static T[,] CreateArray<T>(this Vector v) => new T[v.X, v.Y];

	    public static T[,] CreateArray<T>(this Vector v, Func<Vector, T> selector)
	    {
	        var array = v.CreateArray<T>();
	        
	        foreach (var vector in v.Range())
	        {
	            array.SetAt(vector, selector(vector));
	        }
	        
	        return array;
	    }



        public static RoundPart<T> Round<T>(this T[,] arr, Vector center, double radius)
            => new RoundPart<T>(arr, center, radius);
        
        public static RectanglePart<T> Rectangle<T>(this T[,] arr, Vector begin, Vector end)
            => new RectanglePart<T>(arr, begin, end);
            
        
        
        public static T FindNearest<T>(this T[,] arr, Vector center, Predicate<T> match, int maxDistance = int.MaxValue)
        {
            var cx = center.X;
            var cy = center.Y;
            
            for (var d = 0; d <= maxDistance; d++)
            {
                for (var i = -d; i < d; i++)
                {
                    for (var j = 0; j < 4; j++)
                    {
                        T current;
                        switch (j)
                        {
                            case 0: current = arr[cx + i, cy + d]; break;
                            case 1: current = arr[cx - i, cy - d]; break;
                            case 2: current = arr[cx + d, cy + i]; break;
                            case 3: current = arr[cx - d, cy - i]; break;
                            default: throw new NotImplementedException();
                        }
                        
                        if (match(current))
                        {
                            return current;
                        }
                    }
                }
            }

            return default(T);
        }
    }
}
