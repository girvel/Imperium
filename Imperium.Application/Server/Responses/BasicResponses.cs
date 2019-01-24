using System.Linq;
using Imperium.Core.Systems.Owning;
using Imperium.Ecs.Managers;
using Imperium.Server;
using Imperium.Server.Generation;
using Imperium.Server.Generation.Attributes;
using NetData = System.Collections.Generic.Dictionary<string, dynamic>;

namespace Imperium.Application.Server.Responses
{
    [ResponseContainer]
    public class BasicResponses : IRequestContainer<EcsManager>
    {
        public EcsManager GlobalData { get; set; }



        [Response]
        public bool Login(Connection<Owner> connection, string login, string password)
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
        public NetData[] GetNews(Connection<Owner> connection)
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