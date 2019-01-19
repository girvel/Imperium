using System;
using System.Collections.Generic;
using Imperium.Ecs.Attributes;
using Imperium.Ecs.Managers;
using Moq;
using Xunit;

namespace Imperium.Ecs.Tests
{
    public class SystemTests
    {
        [Fact]
        public void Start_DoesNothingWhenEcsContainsAllRequiredSystems()
        {
            // arrange
            var system = new TestSystem {Ecs = Mock.Of<EcsManager>()};
            
            system.Ecs.SystemManager = Mock.Of<SystemManager>();
            system.Ecs.SystemManager.Subjects = new List<System> {new RequiredSystem1(), new RequiredSystem2()};
            
            // act
            system.Start();
            
            // assert
            Assert.True(true);
        }
        
        
        
        [Fact]
        public void Start_ThrowsAnExceptionWhenThereAreOutstandingRequirements()
        {
            // arrange
            var system = new TestSystem {Ecs = Mock.Of<EcsManager>()};
            
            system.Ecs.SystemManager = Mock.Of<SystemManager>();
            system.Ecs.SystemManager.Subjects = new List<System> {new RequiredSystem1()};
            
            // act
            var thrown = false;
            try
            {
                system.Start();
            }
            catch (RequirementsException)
            {
                thrown = true;
            }
            
            // assert
            Assert.True(thrown);
        }



        [RequiresSystems(typeof(RequiredSystem1), typeof(RequiredSystem2))]
        private class TestSystem : System
        {
            
        }

        private class RequiredSystem1 : System
        {
            
        }

        private class RequiredSystem2 : System
        {
            
        }
    }
}