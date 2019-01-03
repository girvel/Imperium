using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using Imperium.Server.Generation.Attributes;
using Imperium.Server.Generation.Exceptions;
using Province.Log;
using NetData = System.Collections.Generic.Dictionary<string, dynamic>;

namespace Imperium.Server.Generation
{
    public class ResponseServerFactory<TAccountData, TGlobalData>
    {
        private delegate NetData ExceptionResponse(Exception ex, Connection<TAccountData> connection, NetData arguments);
        
        public virtual Server<TAccountData> Generate(TGlobalData globalData, Assembly assembly)
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

            const string logDirectory = "log";
            Directory.CreateDirectory(logDirectory);

            var log = new Log(
                Console.Out,
                new StreamWriter(
                    File.OpenWrite(
                        Path.Combine(
                            logDirectory,
                            $"log {DateTime.Now:yy-MM-dd hh mm ss}.txt"))));
            
            var responses
                = containers
                    .SelectMany(c
                        => c.GetType()
                            .GetMethods()
                            .Where(m => m.GetCustomAttributes(false).Any(a => a is ResponseAttribute))
                            .Select(m => new KeyValuePair<string, ResponsePair<TAccountData>>(
                                m.Name,
                                new ResponsePair<TAccountData>(
                                    MethodToDelegate(m, c, log),
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
                log);
        }
        
        
        
        private static ResponseManager<TAccountData>.ResponseGenerator MethodToDelegate(
            MethodInfo method, IRequestContainer<TGlobalData> container, Log log)
        {
            return (connection, args) =>
            {
                var methodArgs = new object[method.GetParameters().Length];
                methodArgs[0] = connection;

                for (var i = 1; i < methodArgs.Length; i++)
                {
                    methodArgs[i] = args[method.GetParameters()[i].Name];
                }

                var argsString = args.Aggregate("", (sum, p) => $"{sum}, {p.Key}: {ConvertToString(p.Value)}");
                argsString = argsString == string.Empty ? string.Empty : argsString.Substring(2);
                
                log.Message($"{method.Name}({argsString})");

                var result = method.Invoke(container, methodArgs);
                
                log.Message($"return {ConvertToString(result)}");

                return new NetData
                {
                    ["result"] = result,
                };
            };
        }

        private static string ConvertToString(object obj)
        {
            {
                var str = obj as string;
                if (str != null) return str;
            }
            
            {
                var enumerable = obj as IEnumerable<object>;
                if (enumerable != null) 
                    return $"{{{enumerable.Aggregate("", (sum, o) => sum + ConvertToString(o) + ", ")}}}";
            }

            {
                var enumerable = obj as IEnumerable;
                if (enumerable != null) return ConvertToString(enumerable.Cast<object>());
            }

            if (obj is KeyValuePair<string, object>)
            {
                var pair = (KeyValuePair<string, object>) obj;
                return $"{pair.Key}: {ConvertToString(pair.Value)}";
            }

            return obj.ToString();
        }
    }
}