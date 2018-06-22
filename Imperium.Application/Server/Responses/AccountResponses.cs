using System.Linq;
using Imperium.Core.Systems.Owning;
using Imperium.Game;
using Imperium.Server;
using Imperium.Server.Generation;
using Imperium.Server.Generation.Attributes;
using NetData = System.Collections.Generic.Dictionary<string, dynamic>;

namespace Imperium.Application.Server.Responses
{
    [ResponseContainer]
    public class AccountResponses : IRequestContainer<GameData>
    {
        public GameData GlobalData { get; set; }



        [Response]
        public NetData Login(Connection<Player> connection, NetData arguments)
        {
            var account = connection.Server.Accounts.FirstOrDefault(a =>
                a.Login == arguments["login"] && a.Password == arguments["password"]);

            if (connection.Server.Connections.All(c => c.Account != account))
            {
                connection.Account = account;
            }

            return new NetData
            {
                ["success"] = account != null,
            };
        }

        
        
        [Response(Permission.User)]
        public NetData GetArea(Connection<Player> connection, NetData arguments)
        {
            var result = new string[GlobalData.Area.Size.X, GlobalData.Area.Size.Y];
            foreach (var v in GlobalData.Area.Size)
            {
                result[v.X, v.Y] = GlobalData.Area[v].First().Parent.Name;
            }
                                
            return new NetData
            {
                ["area"] = result,
            };
        }
    }
}