using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Imperium.Server.Tests
{
    public class ResponseManagerTests
    {
        [Fact]
        public void GetResponse_ReturnsCorrectResponse()
        {
            // arrange
            var manager = new ResponseManager<object>(
                new Dictionary<string, ResponsePair<object>>
                {
                    ["refresh"] = new ResponsePair<object>((args, c) => new Dictionary<string, dynamic> { ["response"] = "nothing" }),
                },
                null, null);

            // act
            var response = manager.GetResponse(
                new JObject { ["type"] = "refresh", ["args"] = new JObject() }.ToString(), 
#pragma warning disable 618
                new Connection<object>());
#pragma warning restore 618

            // assert
            Assert.Equal(JToken.Parse(response)["response"].ToString(), "nothing");
        }


        [Fact]
        public void GetResponse_UsesErrorResponseWhenRequestIsIncorrect()
        {
            // arrange
            var manager = new ResponseManager<object>(
                new Dictionary<string, ResponsePair<object>>(),
                null,
                ex => (c, args) => new Dictionary<string, dynamic>{["ex"] = true});

            // act
            var response = manager.GetResponse(
                new JObject { ["type"] = "refresh", ["args"] = new JObject() }.ToString(),
                null);
            
            // assert
            Assert.True(JObject.Parse(response)["ex"].ToObject<bool>());
        }

        [Fact]
        public void GetResponse_ShouldConsiderUserGroup()
        {
            // arrange
            var manager = new ResponseManager<object>(
                new Dictionary<string, ResponsePair<object>>
                {
                    ["login"] = new ResponsePair<object>(
                        (c, args) =>
                        {
                            c.Account = new Account<object>("", "", new []{"b"}, null);
                            return null;
                        }),
                    ["b"] = new ResponsePair<object>(
                        (c, args) => new Dictionary<string, dynamic>{["a"] = true}, "b")
                }, 
                (args, c) => new Dictionary<string, dynamic> {["error type"] = "permission"},
                null);

#pragma warning disable 618
            var connection = new Connection<object>();
#pragma warning restore 618

            var request = new JObject {["type"] = "b", ["args"] = null}.ToString();

            // act
            var beforeLogin = JObject.Parse(manager.GetResponse(request, connection));
            manager.GetResponse(new JObject { ["type"] = "login", ["args"] = null }.ToString(), connection);
            var afterLogin = JObject.Parse(manager.GetResponse(request, connection));

            // assert
            Assert.True(beforeLogin["error type"].ToString() == "permission");
            Assert.True(afterLogin["a"].ToObject<bool>());
        }
    }
}