using System.Linq;
using Imperium.CommonData;
using Imperium.Core.Systems.Fight;
using Imperium.Core.Systems.Movement;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Core.Systems.Science;
using Imperium.Core.Systems.Upgrading;
using Imperium.Ecs.Managers;
using Imperium.Game.Prototypes;
using Imperium.Game.Systems.Vision;
using Imperium.Server;
using Imperium.Server.Generation;
using Imperium.Server.Generation.Attributes;
using Province.Vector;

namespace Imperium.Application.Server.Responses
{
    [ResponseContainer]
    public class SystemResponses : IRequestContainer<EcsManager>
    {
        public EcsManager GlobalData { get; set; }
        
        
        
        [Response(Permission.User)]
        public bool UpgradeBuilding(Connection<Owner> connection, Vector position, string name)
        {
            var component
                = GlobalData
                    .GetSystem<Area>()[position]
                    .Select(c => c.Parent.GetComponent<Upgradable>())
                    .FirstOrDefault(c => c != null);

            var upgrade = component?.Upgrades.FirstOrDefault(u => u.Result.Name == name);
            
            return upgrade != null 
                   && GlobalData.GetSystem<ClientVision>().IsVisible(connection.Account.ExternalData, position) 
                   && component.Upgrade(connection.Account.ExternalData, upgrade);
        }

        
        
        [Response(Permission.User)]
        public VisionDto GetVision(Connection<Owner> connection)
        {
            return GlobalData.SystemManager.GetSystem<ClientVision>().GetCurrentVision(connection.Account.ExternalData);
        }
        
        
        
        [Response(Permission.User)]
        public bool Move(Connection<Owner> connection, Vector from, Vector to)
        {
            var squad = GlobalData.SystemManager.GetSystem<Area>().ContainerSlice<Squad>()[from];

            return squad != null
                   && squad.GetComponent<Owned>().Owner == connection.Account.ExternalData
                   && squad.GetComponent<Movable>().Move(to);
        }
        
        
        
        [Response(Permission.User)]
        public bool BeginResearch(Connection<Owner> connection, string name)
        {
            var holder = connection.Account.ExternalData.Parent.GetComponent<ResearchHolder>();
            return holder.BeginResearch(
                holder.ResearchedTechnologies
                    .SelectMany(t => t.Children)
                    .Concat(new[] {GlobalData.GetSystem<ResearchSystem>().RootResearch})
                    .FirstOrDefault(t => t.Name == name));
        }
        
        
        
        [Response(Permission.User)]
        public bool Attack(Connection<Owner> connection, Vector from, Vector to)
        {
            var area = GlobalData.GetSystem<Area>();
            return GlobalData.GetSystem<ClientVision>().IsVisible(connection.Account.ExternalData, to) 
                   && (area.ComponentSlice<Fighter>()[from]?.Attack(area.ComponentSlice<Destructible>()[to]) ?? false);
        }
    }
}