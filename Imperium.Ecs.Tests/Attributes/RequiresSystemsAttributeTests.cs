using System;
using System.Collections.Generic;
using Imperium.Ecs.Attributes;
using Imperium.Ecs.Managers;
using Moq;
using Xunit;

namespace Imperium.Ecs.Tests.Attributes
{
    public class RequiresSystemsAttributeTests
    {
        [Fact]
        public void CheckRequirements_ThrowsRequirementsExceptionWhenThereAreSomeOutstandingRequirements()
        {
            // arrange
            var system = new TestSystem
            {
                Ecs = Mock.Of<EcsManager>(
                    ecs => ecs.SystemManager == Mock.Of<SystemManager>(m => m.Subjects == new List<System>()))
            };
            var exceptionThrown = false;

            // act
            try
            {
                RequirementsAttribute.CheckRequirements(system);
            }
            catch (RequirementsException)
            {
                exceptionThrown = true;
            }
            
            // assert
            Assert.True(exceptionThrown);
        }

        private class RequiredSystem : System
        {
        }

        [RequiresSystems(typeof(RequiredSystem))]
        private class TestSystem : System
        {
            
        }
    }
}