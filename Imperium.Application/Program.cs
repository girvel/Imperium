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
        public static EcsManager Ecs;
        
        public static void Main(string[] args)
        {
            Ecs = new EcsFactory().Generate();

            Ecs.Start();
        }
    }
}