using Imperium.Ecs.Managers;
using Xunit;

namespace Imperium.Ecs.Tests.Managers
{
    public class ManagerTests
    {
        [Fact]
        public void Register_ShouldAddNewSubjectToList()
        {
            // arrange
            var objectManager = new Manager<object>();
            var newObject = new object();
            
            // act
            objectManager.Register(newObject);
            
            // assert
            Assert.True(objectManager.Subjects.Contains(newObject));
        }
    }
}