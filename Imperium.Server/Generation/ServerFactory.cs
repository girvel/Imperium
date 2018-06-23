﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using Imperium.Server.Generation.Attributes;
using Imperium.Server.Generation.Exceptions;
using NetData = System.Collections.Generic.Dictionary<string, dynamic>;

namespace Imperium.Server.Generation
{
    public class ServerFactory<TAccountData, TGlobalData>
    {
        private delegate NetData ExceptionResponse(Exception ex, Connection<TAccountData> connection, NetData arguments);
        
        public Server<TAccountData> Generate(TGlobalData globalData, Assembly assembly)
        {
            var containers
                = assembly
                    .GetTypes()
                    .Where(t => t.GetCustomAttributes(false).Any(a => a is ResponseContainerAttribute))
                    .Select(t => (IRequestContainer<TGlobalData>) t.GetConstructor(new Type[0]).Invoke(new object[0]))
                    .ToArray();

            foreach (var container in containers)
            {
                container.GlobalData = globalData;
            }

            var responses
                = containers
                    .SelectMany(c
                        => c.GetType()
                            .GetMethods()
                            .Where(m => m.GetCustomAttributes(false).Any(a => a is ResponseAttribute))
                            .Select(m => new KeyValuePair<string, ResponsePair<TAccountData>>(
                                m.Name,
                                new ResponsePair<TAccountData>(
                                    (ResponseManager<TAccountData>.ResponseGenerator)
                                    m.CreateDelegate(typeof(ResponseManager<TAccountData>.ResponseGenerator), c),
                                    m.GetCustomAttributes(false).OfType<ResponseAttribute>().First().Groups))))
                    .ToDictionary(
                        p => p.Key,
                        p => p.Value);

            var permissionResponses
                = containers.SelectMany(c
                        => c.GetType()
                            .GetMethods()
                            .Where(m => m.GetCustomAttributes(false).Any(a => a is PermissionResponseAttribute))
                            .Select(r => 
                                (ResponseManager<TAccountData>.ResponseGenerator) 
                                r.CreateDelegate(typeof(ResponseManager<TAccountData>.ResponseGenerator), c)))
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

            return new Server<TAccountData>(
                new IPEndPoint(IPAddress.Parse("192.168.0.100"), 7999),
                new ResponseManager<TAccountData>(
                    responses,
                    permissionResponses[0],
                    ex => (args, c) => exceptionResponses[0](ex, args, c)),
                new Log(Console.Out));
        }
    }
}