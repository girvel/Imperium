namespace Imperium.Core.Common
{
    public struct InternalResources : IResources
    {
        private float[] _resourceArray;

        public float[] ResourcesArray
        {
            get => _resourceArray ?? (_resourceArray = new float[0]);
            set => _resourceArray = value;
        }

        public float this[int index]
        {
            get => ResourcesArray[index];
            set => ResourcesArray[index] = value;
        }

        public int ResourceTypesNumber => ResourcesArray.Length;
        
        
        
        public bool Enough(IResources price)
        {
            return this.Enough<InternalResources>((InternalResources) (price ?? new InternalResources()));
        }

        public static InternalResources operator +(InternalResources r1, InternalResources r2)
        {
            return r1.Added(r2);
        }

        public static InternalResources operator -(InternalResources r)
        {
            return r.Inverted();
        }

        public static InternalResources operator -(InternalResources r1, InternalResources r2)
        {
            return r1.Substracted(r2);
        }

        public static InternalResources operator *(InternalResources r, float n)
        {
            return r.Multiplied(n);
        }

        public static InternalResources operator *(float n, InternalResources r)
        {
            return r * n;
        }

        
        
        public object Clone() => new InternalResources {_resourceArray = (float[]) ResourcesArray.Clone(),};
    }
}