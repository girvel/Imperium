using System.IO;
using System.Linq;
using Moq;
using Province.Log;
using Xunit;

namespace Imperium.Server.Tests
{
    public class LogTests
    {
        [Fact]
        public void Message_ShouldWriteByAllWriters()
        {
            // arrange
            var writerMocks = new[] { new Mock<TextWriter>(), new Mock<TextWriter>() };

            var log = new Log(writerMocks.Select(m => m.Object).ToArray());

            // act
            log.Message("Hello world!");

            // assert
            foreach (var m in writerMocks)
            {
                m.Verify(t => t.Write(It.IsAny<string>()), Times.Once);
            }
        }
    }
}