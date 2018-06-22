using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Imperium.Application.Server;
using Imperium.Core.Systems.Owning;
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
            var assembly = typeof(Program).Assembly;

            var containers
                = assembly
                    .GetTypes()
                    .Where(t => t.GetCustomAttributes(false).Any(a => a is ContainerAttribute))
                    .Select(t => (IRequestContainer) t.GetConstructor(new Type[0]).Invoke(new object[0]))
                    .ToArray();

            foreach (var container in containers)
            {
                container.GameData = game;
            }

            var requests
                = containers
                    .SelectMany(c
                        => c.GetType()
                            .GetMethods()
                            .Where(m => m.GetCustomAttributes(false).Any(a => a is RequestAttribute))
                            .Select(m => new KeyValuePair<string, Response>(
                                m.Name,
                                new Response(
                                    (RequestDelegate) m.CreateDelegate(typeof(RequestDelegate), c),
                                    m.GetCustomAttributes(false).OfType<RequestAttribute>().First().Groups))))
                            .ToDictionary(
                                p => p.Key,
                                p => p.Value);
            
            /*var requests = typeof(AccountRequests).GetMethods()
                .Where(m => m.GetCustomAttributes(false).Any(a => a is RequestAttribute))
                .ToDictionary(
                    m => m.Name,
                    m => new Response(
                        (RequestDelegate) m.CreateDelegate(typeof(RequestDelegate), container), 
                        m.GetCustomAttributes(false).OfType<RequestAttribute>().First().Groups));*/
            
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