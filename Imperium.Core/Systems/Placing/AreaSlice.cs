using System.Linq;
using Imperium.Ecs;
using Province.Vector;

namespace Imperium.Core.Systems.Placing
{
    public class AreaSlice
    {
        public Entity this[Vector position]
        {
            get => Area[position].Select(p => p.Parent).FirstOrDefault(p => _buildings.Contains(p.Prototype));
            set
            {
                Area.Ecs.EntityManager.Destroy(this[position]);
                Area.Move(Area.Ecs.EntityManager.Create(value).GetComponent<Placer>(), position);
            }
        }

        public readonly Area Area;
        private readonly Entity[] _buildings;

        public AreaSlice(Area area, Entity[] buildings)
        {
            Area = area;
            _buildings = buildings;
        }
    }
}