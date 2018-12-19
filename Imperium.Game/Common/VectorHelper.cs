using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using Vector = Province.Vector.Vector;

namespace Imperium.Game.Common
{
    public static class VectorHelper
    {
        public static Matrix<float> ToMatrix(this Vector v) => Matrix.Build.DenseOfArray(new float[,] {{v.X}, {v.Y}});
        
        public static Matrix<float> TransitionMatrix(Vector a, Vector b) => a.ToMatrix().Append(b.ToMatrix()).Inverse();

        public static Matrix<float> TransitionMatrix(this Vector a) => TransitionMatrix(a, a.Rotate90());
    }
}