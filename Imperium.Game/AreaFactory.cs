using Imperium.Core.Systems.Income;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Core.Systems.Upgrading;
using Imperium.Ecs;
using Imperium.Ecs.Managers;

namespace Imperium.Game
{
    public class AreaFactory
    {
        public void Generate(Area area, EcsManager ecs)
        {
            var house = new Entity
            {
                Name = "Wooden house",
                Components =
                {
                    new PositionComponent(),
                    new Incomer{Income = new Resources{Food=3600}},
                    new OwnedComponent(),
                }
            };
            
            var field = new Entity
            {
                Name = "Field",
                Components =
                {
                    new PositionComponent(),
                    new UpgradableComponent(house),
                }
            };

            var forest = new Entity
            {
                Name = "Forest",
                Components =
                {
                    new PositionComponent(), 
                },
            };
            
            foreach (var position in area.Size.Range())
            {
                var clone = ecs.EntityManager.Create(position == area.Size / 2 ? house : field);
                area.Move(clone.GetComponent<PositionComponent>(), position);
            }
        }
    }
}