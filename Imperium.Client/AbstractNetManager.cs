using System;
using System.Net;
using Newtonsoft.Json;
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

        protected T Request<T>(string type, NetData args)
        {
            var firstTime = true;
            while (true)
            {
                try
                {
                    return RequestManager.DecodeResponse<T>(
                        Connection.Request(
                            firstTime
                                ? RequestManager.CreateRequest(type, args)
                                : "@resend"));
                }
                catch (JsonReaderException)
                {
                }
                catch (NullReferenceException)
                {
                }

                firstTime = false;
            }
        }
    }
}