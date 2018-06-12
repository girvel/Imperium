using System;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs;

namespace Imperium.Game
{
    public class TestWorldFactory
    {
        public void Generate(AreaSystem area)
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
                var clone = area.Ecs.EntityManager.CreateNew(position == area.Size / 2 ? house : field);
                area.Move(clone.GetComponent<PositionComponent>(), position);
            }
        }
    }
}