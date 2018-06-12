using System;
using Imperium.Core;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs;
using Imperium.Ecs.Managers;

namespace Imperium.Game
{
    public class AreaFactory
    {
        public void Generate(Area area, EcsManager ecs)
        {
            var field = new Entity
            {
                Name = "Field",
                Components =
                {
                    new PositionComponent(),
                }
            };

            var house = new Entity
            {
                Name = "House",
                Components =
                {
                    new PositionComponent(),
                }
            };

            foreach (var position in area.Size)
            {
                var clone = ecs.EntityManager.CreateNew(position == area.Size / 2 ? house : field);
                area[position].Add(clone.GetComponent<PositionComponent>());
            }
        }
    }
}