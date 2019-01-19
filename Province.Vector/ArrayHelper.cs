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
    }
}