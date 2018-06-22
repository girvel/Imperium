using System.Linq;
using Imperium.Core.Systems.Owning;
using Imperium.Game;
using Imperium.Server;
using NetData = System.Collections.Generic.Dictionary<string, dynamic>;

namespace Imperium.Application.Server.Requests
{
    [Container]
    public class AccountRequests : IRequestContainer
    {
        public GameData GameData { get; set; }



        [Request]
        public NetData Login(NetData arguments, Connection<Player> connection)
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

        
        
        [Request(Permission.User)]
        public NetData GetArea(NetData arguments, Connection<Player> connection)
        {
            var result = new string[GameData.Area.Size.X, GameData.Area.Size.Y];
            foreach (var v in GameData.Area.Size)
            {
                result[v.X, v.Y] = GameData.Area[v].First().Parent.Name;
            }
                                
            return new NetData
            {
                ["area"] = result,
            };
        }
    }
}