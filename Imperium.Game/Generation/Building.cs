using Imperium.Core.Systems.Income;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Core.Systems.Upgrading;
using Imperium.Ecs;

namespace Imperium.Game.Generation
{
    public static class Building
    {
        public static readonly Entity
            WoodenHouse
                = new Entity("Wooden house")
                  | new Position()
                  | new Incomer {Income = new Resources {Food = 3600}}
                  | new Owned(),
            
            Sawmill
                = new Entity("Sawmill")
                  | new Position()
                  | new Incomer {Income = new Resources {Wood = 3600}}
                  | new Owned(),

            Field
                = new Entity("Field")
                  | new Position()
                  | new Incomer {Income = new Resources {Corn = 3600}}
                  | new Owned(),

            Plain
                = new Entity("Plain")
                  | new Position()
                  | new Upgradable(
                      new Upgrade(WoodenHouse, new Resources {Wood = 100}),
                      new Upgrade(Field, new Resources())),

            Forest
                = new Entity("Forest")
                  | new Position()
                  | new Upgradable(
                      new Upgrade(Sawmill, new Resources {Wood = 100}));
    }
}