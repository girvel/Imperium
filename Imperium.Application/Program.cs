using System;
using System.Collections.Generic;
using Imperium.Core;
using Imperium.Core.Common;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs.Managers;
using Imperium.Game;

namespace Imperium.Application
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var game = EcsManager.CreateNew();
            
            var area = new Area(new Vector(5, 5));
            new TestWorldFactory().Generate(area, game);
            
            game.SystemManager.Register(new AreaSystem(area));
            game.SystemManager.Register(new PlayerSystem());
            
            game.Start();
        }
    }
}