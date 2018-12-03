using Imperium.Core.Common;
using ResourceType = Imperium.CommonData.ResourceType;

namespace Imperium.Game
{
    public struct Resources : IResources
    {
        public static Resources Zero => new Resources();


        private float[] _resourceArray;

        public float[] ResourcesArray
        {
            get => _resourceArray ?? (_resourceArray = new float[ResourceTypesNumber]);
            private set => _resourceArray = value;
        }

        public float this[int index]
        {
            get => ResourcesArray[index];
            set => ResourcesArray[index] = value;
        }

        public int ResourceTypesNumber => typeof(ResourceType).GetEnumNames().Length;



        #region Resources properties

        public float Wood
        {
            get => ResourcesArray[(int) ResourceType.Wood];
            set => ResourcesArray[(int) ResourceType.Wood] = value;
        }

        public float Food
        {
            get => ResourcesArray[(int)ResourceType.Food];
            set => ResourcesArray[(int)ResourceType.Food] = value;
        }

        public float RawFood
        {
            get => ResourcesArray[(int)ResourceType.RawFood];
            set => ResourcesArray[(int)ResourceType.RawFood] = value;
        }

        public float Instruments
        {
            get => ResourcesArray[(int)ResourceType.Instruments];
            set => ResourcesArray[(int)ResourceType.Instruments] = value;
        }

        public float Corn
        {
            get => ResourcesArray[(int)ResourceType.Corn];
            set => ResourcesArray[(int)ResourceType.Corn] = value;
        }

        public float Coal
        {
            get => ResourcesArray[(int)ResourceType.Coal];
            set => ResourcesArray[(int)ResourceType.Coal] = value;
        }

        #endregion



        public bool Enough(IResources price)
        {
            return this.Enough<Resources>((Resources) (price ?? new Resources()));
        }

        public static Resources operator +(Resources r1, Resources r2)
        {
            return r1.Added(r2);
        }

        public static Resources operator -(Resources r)
        {
            return r.Inverted();
        }

        public static Resources operator -(Resources r1, Resources r2)
        {
            return r1.Substracted(r2);
        }

        public static Resources operator *(Resources r, float n)
        {
            return r.Multiplied(n);
        }

        public static Resources operator *(float n, Resources r)
        {
            return r * n;
        }
        
        

        public static bool operator ==(Resources r1, Resources r2)
        {
            return r1.Equals(r2);
        }

        public static bool operator !=(Resources r1, Resources r2)
        {
            return !(r1 == r2);
        }

        

        public override bool Equals(object obj)
        {
            if (!(obj is Resources)) return base.Equals(obj);

            var r = (Resources)obj;

            return this.Equals<Resources>(r);
        }

        public override int GetHashCode()
        {
            return _resourceArray?.GetHashCode() ?? 0;
        }

        public object Clone() => new Resources {_resourceArray = (float[]) ResourcesArray.Clone(),};



        public static Resources operator +(IResources r1, Resources r2) => (Resources) r1 + r2;

        public static Resources operator +(Resources r1, IResources r2) => r2 + r1;

        public static Resources operator -(IResources r1, Resources r2) => (Resources) r1 - r2;

        public static Resources operator -(Resources r1, IResources r2) => r2 - r1;
        
        

        public static bool operator ==(IResources r1, Resources r2) => (Resources) r1 == r2;

        public static bool operator ==(Resources r1, IResources r2) => r2 == r1;

        public static bool operator !=(IResources r1, Resources r2) => (Resources) r1 != r2;

        public static bool operator !=(Resources r1, IResources r2) => r2 != r1;



        public static implicit operator InternalResources(Resources r) 
            => new InternalResources{ResourcesArray = (float[]) r.ResourcesArray.Clone()};

        public static implicit operator Resources(InternalResources r) 
            => new Resources{ResourcesArray = (float[]) r.ResourcesArray.Clone()};
    }
}