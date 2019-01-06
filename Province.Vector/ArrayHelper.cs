namespace Province.Vector
{
    public static class ArrayHelper
    {
        public static T At<T>(this T[,] arr, Vector position) => arr[position.X, position.Y];
    }
}