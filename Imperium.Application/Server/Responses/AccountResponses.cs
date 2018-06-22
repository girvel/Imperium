using System.Linq;
using Imperium.Core.Systems.Owning;
using Imperium.Game;
using Imperium.Server;
using Imperium.Server.Requesting;
using NetData = System.Collections.Generic.Dictionary<string, dynamic>;

namespace Imperium.Application.Server.Responses
{
    [ResponseContainer]
    public class AccountResponses : IRequestContainer
    {
        public GameData GameData { get; set; }



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