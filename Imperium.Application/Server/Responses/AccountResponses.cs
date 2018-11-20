﻿using System.Linq;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Core.Systems.Upgrading;
using Imperium.Ecs.Managers;
using Imperium.Game;
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
        public string[,] GetArea(Connection<Player> connection)
        {
            var area = GlobalData.SystemManager.GetSystem<Area>();
            var result = new string[area.Size.X, area.Size.Y];
            foreach (var v in area.Size.Range())
            {
                result[v.X, v.Y] = area[v].First().Parent.Name;
            }

            return result;
        }
        
        
        
        [Response(Permission.User)]
        public bool UpgradeBuilding(Connection<Player> connection, Vector position, string name)
        {
            return GlobalData
                .SystemManager.GetSystem<Area>()[position]
                .Select(c => c.Parent.GetComponent<UpgradableComponent>())
                .FirstOrDefault(c => c != null)
                ?.Upgrade() ?? false;
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
    }
}