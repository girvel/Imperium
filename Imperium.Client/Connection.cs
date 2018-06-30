using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Imperium.Client
{
    public class Connection : IDisposable
    {
        public Socket Socket { get; }
        
        public Encoding Encoding { get; set; }



        public Connection()
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Encoding = Encoding.UTF8;
        }

        public void Connect(EndPoint remoteEndPoint)
        {
            Socket.Connect(remoteEndPoint);
        }

        public string Request(string request)
        {
            Socket.Send(Encoding.GetBytes(request));
            
            var currentStringBuilder = new StringBuilder();
            var receivedData = new byte[4096];

            do
            {
                var bytes = Socket.Receive(receivedData);

                currentStringBuilder.Append(Encoding.GetString(receivedData, 0, bytes));
            }
            while (Socket.Available > 0);
            
            return currentStringBuilder.ToString();
        }
        
        

        public void Dispose()
        {
            Socket?.Close();
        }
    }
}