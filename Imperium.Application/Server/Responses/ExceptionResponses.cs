﻿using System;
using Imperium.Core.Systems.Owning;
using Imperium.Game;
using Imperium.Server;
using Imperium.Server.Generation;
using Imperium.Server.Generation.Attributes;
using NetData = System.Collections.Generic.Dictionary<string, dynamic>;

namespace Imperium.Application.Server.Responses
{
    [ResponseContainer]
    public class ExceptionResponses : IRequestContainer<GameData>
    {
        public GameData GlobalData { get; set; }
        
        [ExceptionResponse]
        public NetData Exception(Exception ex, Connection<Player> connection, NetData arguments) => new NetData
        {
            ["error type"] = "request",
            ["exception"] = ex,
        };

        [PermissionResponse]
        public NetData PermissionException(Connection<Player> connection, NetData arguments) => new NetData
        {
            ["error type"] = "permission",
        };
    }
}