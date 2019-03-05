using Imperium.Core.Systems.Fight;
using Imperium.Core.Systems.Income;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Core.Systems.Science;
using Imperium.Core.Systems.Upgrading;
using Imperium.Core.Systems.Upgrading.Conditions;
using Imperium.Core.Systems.Vision;
using Imperium.Ecs;

namespace Imperium.Game.Prototypes
{
    public class Building
    {
        public static readonly Entity
            WoodenHouse
                = new Entity("Wooden house")
                  | new Placer()
                  | new Incomer {Income = new Resources {Food = 3600}}
                  | new Owned()
                  | new Observer()
                  | new Destructible(20),

            Sawmill
                = new Entity("Sawmill")
                  | new Placer()
                  | new Incomer {Income = new Resources {Wood = 3600}}
                  | new Owned()
                  | new Observer(),

            None
                = new Entity("None")
                  | new Placer()
                  | new Upgradable(
                      new Upgrade(WoodenHouse)),

            Field
                = new Entity("Field")
                  | new Placer()
                  | new Incomer {Income = new Resources {Corn = 3600}}
                  | new Owned(),

            Forest
                = new Entity("Forest")
                  | new Placer(),

            Mountain
                = new Entity("Mountain")
                  | new Placer();
    }
}