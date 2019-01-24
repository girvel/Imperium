using System.Linq;
using Imperium.Ecs;
using Province.Vector;

namespace Imperium.Core.Systems.Placing
{
    public class ComponentSlice<T> where T : Component
    {
        public Area Basis { get; }
        public ComponentSlice(Area basis)
        {
            Basis = basis;
        }

        public T this[Vector position] => Basis[position].Select(p => p.Parent.GetComponent<T>()).FirstOrDefault(c => c != null);
    }
}