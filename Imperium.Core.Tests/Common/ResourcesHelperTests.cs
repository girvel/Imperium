using System;
using System.Linq;
using Imperium.Core.Common;
using Moq;
using Xunit;

namespace Imperium.Core.Tests.Common
{
    public class ResourcesHelperTests
    {
        [Fact]
        public void Added_ReturnsSummandOfTwoElements()
        {
            // arrange
            var r1 = new InternalResources(1, 2, 3);
            var r2 = new InternalResources(2, 3, 4);
            
            // act
            var result = r1.Added(r2);
            
            // assert
            Assert.True(result.ResourcesArray.SequenceEqual(new float[] {3, 5, 7}));
            Assert.True(r1.ResourcesArray.SequenceEqual(new float[] {1, 2, 3}));
            Assert.True(r2.ResourcesArray.SequenceEqual(new float[] {2, 3, 4}));
        }

        [Fact]
        public void Added_ExpandsArray()
        {
            // arrange
            var r1 = new InternalResources(1, 2);
            var r2 = new InternalResources(0, 0, 3);
            
            // act
            var result = r1.Added(r2);
            
            // assert
            Assert.True(result.ResourcesArray.SequenceEqual(new float[] {1, 2, 3}));
        }

        [Fact]
        public void Multiplied_MultipliesEachResource()
        {
            // arrange
            var r = new InternalResources(1, 2, 3);
            
            // act
            var result = r.Multiplied(2);
            
            // assert
            Assert.True(result.ResourcesArray.SequenceEqual(new float[] {2, 4, 6}));
            Assert.True(r.ResourcesArray.SequenceEqual(new float[] {1, 2, 3}));
        }

        [Fact]
        public void Inverted_InvertsArray()
        {
            // arrange
            var r = new InternalResources(8, 1, 0);
            
            // act
            var result = r.Inverted();
            
            // assert
            Assert.True(result.ResourcesArray.SequenceEqual(new float[] {-8, -1, 0}));
            Assert.True(r.ResourcesArray.SequenceEqual(new float[] {8, 1, 0}));
        }
        
        [Fact]
        public void Substracted_SubstractsArrays()
        {
            // arrange
            var r1 = new InternalResources(1, 2, 3);
            var r2 = new InternalResources(2, 3, 4);
            
            // act
            var result = r1.Substracted(r2);
            
            // assert
            Assert.True(result.ResourcesArray.SequenceEqual(new float[] {-1, -1, -1}));
            Assert.True(r1.ResourcesArray.SequenceEqual(new float[] {1, 2, 3}));
            Assert.True(r2.ResourcesArray.SequenceEqual(new float[] {2, 3, 4}));
        }

        [Fact]
        public void Equals_ChecksEquality()
        {
            // arrange
            var r1 = new InternalResources(1, 2, 3);
            var r2 = new InternalResources(1, 2, 4);
            
            // act
            var result = r1.Equals(r2);
            
            // assert
            Assert.False(result);
            Assert.True(r1.ResourcesArray.SequenceEqual(new float[] {1, 2, 3}));
            Assert.True(r2.ResourcesArray.SequenceEqual(new float[] {1, 2, 4}));
        }
    }
}