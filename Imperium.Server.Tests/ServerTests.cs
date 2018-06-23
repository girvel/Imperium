using System.Net.Sockets;
using System.Threading;
using Xunit;

namespace Imperium.Server.Tests
{
    public class ServerTests
    {
        [Fact]
        public void Start_AcceptsConnections()
        {
            // arrange
            using (var server = new Server<object>(NetHelper.Current.GetIpEndPoint(), null, new Log()))
            using (var socket1 = new Socket(SocketType.Stream, ProtocolType.Tcp))
            using (var socket2 = new Socket(SocketType.Stream, ProtocolType.Tcp))
            {
                socket1.Bind(NetHelper.Current.GetIpEndPoint());
                socket2.Bind(NetHelper.Current.GetIpEndPoint());

                // act
                new Thread(server.Start).Start();

                socket1.Connect(server.EndPoint);
                socket2.Connect(server.EndPoint);

                // assert
                Assert.True(socket1.Connected, "socket1 can not connect to server");
                Assert.True(socket2.Connected, "socket2 can not connect to server");
            }
        }


        [Fact]
        public void Start_CreatesConnections()
        {
            // arrange
            using (var server = new Server<object>(NetHelper.Current.GetIpEndPoint(), null, new Log()))
            using (var socket = new Socket(SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Bind(NetHelper.Current.GetIpEndPoint());

                // act
                new Thread(server.Start).Start();

                // assert
                socket.Connect(server.EndPoint);
                Thread.Sleep(20);

                Assert.Equal(1, server.Connections.Count);
            }
        }
    }
}