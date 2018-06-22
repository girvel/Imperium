using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Imperium.Server.Common;

namespace Imperium.Server
{
    public class Server<T> : IDisposable
    {
        public EndPoint EndPoint { get; }

        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public List<Connection<T>> Connections { get; set; } = new List<Connection<T>>();

        public Log Log { get; set; }

        public ResponseManager<T> ResponseManager { get; set; }

        public List<Account<T>> Accounts { get; set; } = new List<Account<T>>();

        public NewsManager<T> NewsManager { get; set; } = new NewsManager<T>();



        private readonly Socket _socket;



        /// <summary>
        /// Testing ctor
        /// </summary>
        public Server() { }



        public Server(EndPoint endPoint, ResponseManager<T> responseManager, Log log)
        {
            Log = log;
            ResponseManager = responseManager;

            _socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            try
            {
                _socket.Bind(EndPoint = endPoint);
            }
            catch (SocketException)
            {
                Log.Message("Server can not be launched");
                throw;
            }
        }



        /// <summary>
        /// Starts Server
        /// </summary>
        public void Start()
        {
            Log.Message("Server started");
            _socket.Listen(10);

            while (true)
            {
                Socket newSocket;
                var connection = new Connection<T>(newSocket = _socket.Accept(), this);

                connection.StartThread();
                Connections.Add(connection);
                Log.Message($"Accepted connection from {newSocket.RemoteEndPoint.ToLogString()}");
            }
        }



        public void Dispose()
        {
            _socket.Close();
        }
    }
}