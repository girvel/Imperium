using System;
using Imperium.Core.Systems.Fight;
using Imperium.Core.Systems.Income;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Core.Systems.Production;
using Imperium.Core.Systems.Science;
using Imperium.Core.Systems.Upgrading;
using Imperium.Core.Systems.Upgrading.Conditions;
using Imperium.Core.Systems.Vision;
using Imperium.Ecs;
using Imperium.Ecs.Managers;

namespace Imperium.Game.Prototypes
{
    public class Building : PrototypeContainer
    {
        public Entity
            WoodenHouse,
            Sawmill,
            None,
            Field,
            Forest,
            Mountain;

        protected override void InitializePrototypes(EcsManager ecs)
        {
            WoodenHouse
                = new Entity("Wooden house")
                  | new Placer()
                  | new Incomer {IncomePerSecond = new Resources {Food = 3600}}
                  | new Owned()
                  | new Observer()
                  | new Destructible(20)
                  | new Productor(TimeSpan.FromSeconds(1), ecs.GetContainer<Squad>().Test);

            Sawmill
                = new Entity("Sawmill")
                  | new Placer()
                  | new Incomer {IncomePerSecond = new Resources {Wood = 3600}}
                  | new Owned()
                  | new Observer();

            None
                = new Entity("None")
                  | new Placer()
                  | new Upgradable(
                      new Upgrade(WoodenHouse));

            Field
                = new Entity("Field")
                  | new Placer()
                  | new Incomer {IncomePerSecond = new Resources {Corn = 3600}}
                  | new Owned();

            Forest
                = new Entity("Forest")
                  | new Placer();
            
            Mountain
                = new Entity("Mountain")
                | new Placer();
        }
    }
}