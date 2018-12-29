using Imperium.Core.Systems.Income;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Core.Systems.Upgrading;
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
                  | new Observer(),

            Sawmill
                = new Entity("Sawmill")
                  | new Placer()
                  | new Incomer {Income = new Resources {Wood = 3600}}
                  | new Owned(),

            None
                = new Entity("None")
                  | new Placer()
                  | new Upgradable(
                      new Upgrade(WoodenHouse, new Resources {Wood = 100})),

            Field
                = new Entity("Field")
                  | new Placer()
                  | new Incomer {Income = new Resources {Corn = 3600}}
                  | new Owned(),

            Forest
                = new Entity("Forest")
                  | new Placer()
                  | new Upgradable(
                      new Upgrade(Sawmill, new Resources {Wood = 100})),

            Mountain
                = new Entity("Mountain")
                  | new Placer();
    }
}