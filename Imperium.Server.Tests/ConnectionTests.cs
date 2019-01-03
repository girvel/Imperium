using System.Net.Sockets;
using System.Threading;
using Moq;
using Newtonsoft.Json.Linq;
using Province.Log;
using Xunit;

namespace Imperium.Server.Tests
{
    public class ConnectionTests
    {
        [Fact]
        public void Start_ReceivesAndSendsRequests()
        {
            // TODO use NetHelper.GetIpEndPoint

            // arrange
            Socket clientSocket, connectionSocket;
            NetHelper.Current.GetNewSocketPair(out clientSocket, out connectionSocket);

            using (clientSocket)
            using (connectionSocket)
            {
                var requestManagerMock = new Mock<ResponseManager<object>>();
                requestManagerMock.Setup(
                    m => m.GetResponse(
                        It.IsAny<string>(),
                        It.IsAny<Connection<object>>()))
                    .Returns(new JObject { ["response"] = "nothing" }.ToString());

                var server = new Server<object>(
                    connectionSocket.LocalEndPoint,
                    requestManagerMock.Object,
                    new Log());

                var connection = new Connection<object>(connectionSocket, server);

                // act
                connection.StartThread();
                clientSocket.Send(server.Encoding.GetBytes(new JObject().ToString()));

                var response = new byte[256];
                clientSocket.Receive(response);
                connection.Close();

                // assert
                Assert.Equal(
                    new JObject { ["response"] = "nothing" }.ToString(),
                    server.Encoding.GetString(response).Replace("\0", ""));
            }
        }
    }
}