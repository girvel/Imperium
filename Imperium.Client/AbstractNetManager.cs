using System;
using System.Net;
using NetData = System.Collections.Generic.Dictionary<string, object>;

namespace Imperium.Client
{
    public abstract class AbstractNetManager
    {
        public Connection Connection { get; } = new Connection();
        
        public RequestManager RequestManager { get; } = new RequestManager();



        public void Connect(EndPoint remoteEndPoint)
        {
            Connection.Connect(remoteEndPoint);
        }

        protected NetData Request(string type, NetData args) =>
            RequestManager.DecodeResponse(
                Connection.Request(
                    RequestManager.CreateRequest(
                        type,
                        args)));
    }
}