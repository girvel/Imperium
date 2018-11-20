using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Imperium.Application.Server;
using Imperium.Core.Systems.Owning;
using Imperium.Ecs.Managers;
using Imperium.Game;
using Imperium.Server;
using Imperium.Server.Generation;

namespace Imperium.Application
{
    internal class Program
    {
        public static EcsManager Ecs;
        
        public static Server<Player> Server;
        
        
        
        public static void Main(string[] consoleArgs)
        {
            Ecs = new EcsFactory().Generate();
            
            Server = new ResponseServerFactory<Player, EcsManager>().Generate(Ecs, typeof(Program).Assembly);
            Server.Accounts.Add(new Account<Player>("", "", new[] {Permission.User, Permission.Admin}, new Player()));

            EventRegistrator.Register(Server, Ecs);

            new Thread(Server.Start).Start();
            Ecs.Start();
        }
    }
}