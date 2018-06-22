using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Imperium.Core.Systems.Owning;
using Imperium.Game;
using Imperium.Server;
using Imperium.Server.Requesting;
using Imperium.Server.Requesting.Exceptions;
using Response = Imperium.Server.ResponsePair<Imperium.Core.Systems.Owning.Player>;
using RequestDelegate = Imperium.Server.ResponseManager<Imperium.Core.Systems.Owning.Player>.ResponseGenerator;
using NetData = System.Collections.Generic.Dictionary<string, dynamic>;

namespace Imperium.Application.Server
{
    public class ServerFactory
    {
        private delegate NetData ExceptionResponse(Exception ex, Connection<Player> connection, NetData arguments);
        
        public Server<Player> Generate(GameData game)
        {
            var assembly = typeof(Program).Assembly;

            var containers
                = assembly
                    .GetTypes()
                    .Where(t => t.GetCustomAttributes(false).Any(a => a is ResponseContainerAttribute))
                    .Select(t => (IRequestContainer) t.GetConstructor(new Type[0]).Invoke(new object[0]))
                    .ToArray();

            foreach (var container in containers)
            {
                container.GameData = game;
            }

            var responses
                = containers
                    .SelectMany(c
                        => c.GetType()
                            .GetMethods()
                            .Where(m => m.GetCustomAttributes(false).Any(a => a is ResponseAttribute))
                            .Select(m => new KeyValuePair<string, Response>(
                                m.Name,
                                new Response(
                                    (RequestDelegate) m.CreateDelegate(typeof(RequestDelegate), c),
                                    m.GetCustomAttributes(false).OfType<ResponseAttribute>().First().Groups))))
                            .ToDictionary(
                                p => p.Key,
                                p => p.Value);

            var permissionResponses
                = containers.SelectMany(c
                        => c.GetType()
                            .GetMethods()
                            .Where(m => m.GetCustomAttributes(false).Any(a => a is PermissionResponseAttribute))
                            .Select(r => (RequestDelegate) r.CreateDelegate(typeof(RequestDelegate), c)))
                    .ToArray();

            if (permissionResponses.Length != 1)
            {
                throw new ResponseSearchException("Number of permission methods is not 1");
            }
            
            var exceptionResponses
                = containers.SelectMany(c
                        => c.GetType()
                            .GetMethods()
                            .Where(m => m.GetCustomAttributes(false).Any(a => a is ExceptionResponseAttribute))
                            .Select(r => (ExceptionResponse) r.CreateDelegate(typeof(ExceptionResponse), c)))
                    .ToArray();

            if (exceptionResponses.Length != 1)
            {
                throw new ResponseSearchException("Number of exception methods is not 1");
            }
            
            return new Server<Player>(
                new IPEndPoint(IPAddress.Parse("192.168.0.100"), 7999),
                new ResponseManager<Player>(
                    responses, 
                    permissionResponses[0],
                    ex => (args, c) => exceptionResponses[0](ex, args, c)), 
                new Log(Console.Out))
            {
                Accounts = {new Account<Player>("", "", new []{"user"}, new Player())}
            };
        }
    }
}