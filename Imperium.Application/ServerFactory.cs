using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Imperium.Application.Server;
using Imperium.Application.Server.Requests;
using Imperium.Core;
using Imperium.Core.Systems.Owning;
using Imperium.Ecs.Managers;
using Imperium.Game;
using Imperium.Server;
using Response = Imperium.Server.ResponsePair<Imperium.Core.Systems.Owning.Player>;
using NetData = System.Collections.Generic.Dictionary<string, dynamic>;
using RequestDelegate = Imperium.Server.RequestManager<Imperium.Core.Systems.Owning.Player>.ResponseGenerator;

namespace Imperium.Application
{
    public class ServerFactory
    {
        public Server<Player> Generate(GameData game)
        {
            var container = new AccountRequests {GameData = game};

            var requests = typeof(AccountRequests).GetMethods()
                .Where(m => m.GetCustomAttributes(false).Any(a => a is RequestAttribute))
                .ToDictionary(
                    m => m.Name,
                    m => new Response(
                        (RequestDelegate) m.CreateDelegate(typeof(RequestDelegate), container), 
                        m.GetCustomAttributes(false).OfType<RequestAttribute>().First().Groups));
            
            return new Server<Player>(
                new IPEndPoint(IPAddress.Parse("192.168.0.100"), 7999),
                new RequestManager<Player>(
                    requests, 
                    (args, c) => new Dictionary<string, dynamic> { ["error type"] = "permission" },
                    ex => (args, c) =>
                        new Dictionary<string, dynamic>
                        {
                            ["error type"] = "request",
                            ["exception"] = ex,
                        }), 
                new Log(Console.Out))
            {
                Accounts = {new Account<Player>("", "", new []{"user"}, new Player())}
            };
        }
    }
}