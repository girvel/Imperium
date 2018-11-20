using System.Reflection;

namespace Imperium.Core
{
    // TODO Spy class
    
    public delegate void SpyDelegate(PropertyInfo changedPropety, object oldValue, object newValue);
    
    public class Spy<T>
    {
        public static event SpyDelegate ValueChanged;
        
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                //ValueChanged();
                _value = value;
            }
        }
    }
}