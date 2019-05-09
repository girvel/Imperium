using System;
using System.Threading;
using Imperium.Application.Server.Synchronization;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs;
using Imperium.Ecs.Managers;
using Imperium.Game;
using Imperium.Game.Modification;
using Imperium.Game.Prototypes;
using Imperium.Server;
using Imperium.Server.Generation;

namespace Imperium.Application
{
    internal class Program
    {
        public static EcsManager Ecs;
        
        public static Server<Owner> Server;
        
        
        
        public static void Main(string[] consoleArgs)
        {
            Console.Title = "Imperium";
            Console.SetWindowSize(120, 40);
            
            Ecs = new EcsFactory().Generate();
            
            Server = new ResponseServerFactory<Owner, EcsManager>().Generate(Ecs, typeof(Program).Assembly);

            EventRegistrator.Register(Server, Ecs);

            var player = Ecs.EntityManager.Create(Ecs.GetContainer<Global>().Player).GetComponent<Owner>();
            Server.Accounts.Add(new Account<Owner>("", "", new[] {Permission.User, Permission.Admin}, player));
            Ecs.SystemManager.GetSystem<Ownership>().Register(player);
            
            new PlayerModifier().Modify(Ecs.SystemManager.GetSystem<Area>(), Ecs, player, new Random());
            
            new Thread(Server.Start).Start();
            Ecs.Start();
        }
    }
}