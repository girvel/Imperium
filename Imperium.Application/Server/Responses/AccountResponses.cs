using System.Linq;
using Imperium.CommonData;
using Imperium.Core.Common;
using Imperium.Core.Systems.Movement;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Core.Systems.Upgrading;
using Imperium.Core.Systems.Vision;
using Imperium.Ecs.Managers;
using Imperium.Game;
using Imperium.Game.Prototypes;
using Imperium.Game.Systems.Vision;
using Imperium.Server;
using Imperium.Server.Generation;
using Imperium.Server.Generation.Attributes;
using Province.Vector;
using NetData = System.Collections.Generic.Dictionary<string, dynamic>;

namespace Imperium.Application.Server.Responses
{
    [ResponseContainer]
    public class AccountResponses : IRequestContainer<EcsManager>
    {
        public EcsManager GlobalData { get; set; }



        [Response]
        public bool Login(Connection<Player> connection, string login, string password)
        {
            var account = connection.Server.Accounts.FirstOrDefault(a => a.Login == login && a.Password == password);

            if (account == null)
            {
                return false;
            }

            if (connection.Server.Connections.All(c => c.Account != account))
            {
                connection.Account = account;
                return true;
            }

            return false;
        }

        
        
        [Response(Permission.User)]
        public VisionDto GetVision(Connection<Player> connection)
        {
            return GlobalData.SystemManager.GetSystem<ClientVision>().GetCurrentVision(connection.Account.ExternalData);
        }
        
        
        
        [Response(Permission.User)]
        public bool UpgradeBuilding(Connection<Player> connection, Vector position, string name)
        {
            var component
                = GlobalData
                    .SystemManager.GetSystem<Area>()[position]
                    .Select(c => c.Parent.GetComponent<Upgradable>())
                    .FirstOrDefault(c => c != null);

            var upgrade = component?.Upgrades.FirstOrDefault(u => u.Result.Name == name);
            
            return upgrade != null && component.Upgrade(connection.Account.ExternalData, upgrade);
        }
        
        
        
        [Response(Permission.User)]
        public NetData[] GetNews(Connection<Player> connection)
        {
            return connection.Server.NewsManager
                .GetNews(connection.Account.ExternalData)
                .Select(n => 
                    new NetData
                    {
                        ["type"] = n.Type,
                        ["info"] = n.Info,
                    })
                .ToArray();
        }
        
        
        
        [Response(Permission.User)]
        public bool AddResources(Connection<Player> connection)
        {
            connection.Account.ExternalData.Resources += new Resources {Wood = 100};
            return true;
        }
        
        
        
        [Response(Permission.User)]
        public bool Move(Connection<Player> connection, Vector from, Vector to)
        {
            var squad = GlobalData.SystemManager.GetSystem<Area>().Slice<Squad>()[from];
            squad?.GetComponent<Movable>().Move(to);

            return squad != null;
        }
    }
}