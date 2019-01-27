using FluentAssertions;
using Mithril.Invoices.Domain.Core;
using System;
using Xunit;

namespace Mithril.Invoices.Domain.Tests.Core
{
    public class ValueObjectTests
    {
        class FakeValueObject : ValueObject<FakeValueObject>
        {
            public int A { get; set; }
            public string B { get; set; }

            public override bool EqualsCore(FakeValueObject obj)
            {
                return A == obj.A && B == obj.B;
            }

            protected override int GetHashCodeCore()
            {
                return HashCode.Combine(A, B);
            }
        }

        [Fact]
        public void SimilarValueObjectsShouldEquals()
        {
            // Arrange
            var valueObject1 = new FakeValueObject() { A = 5, B = "Hello" };
            var valueObject2 = new FakeValueObject() { A = 5, B = "Hello" };

            // Act
            var areEquals = valueObject1 == valueObject2;

            // Assert
            areEquals.Should().BeTrue();
        }

        [Fact]
        public void NotSimilarValueObjectsShouldNotEquals()
        {
            // Arrange
            var valueObject1 = new FakeValueObject() { A = 5, B = "Hello" };
            var valueObject2 = new FakeValueObject() { A = 6, B = "World" };

            // Act
            var areEquals = valueObject1 == valueObject2;

            // Assert
            areEquals.Should().BeFalse();
        }

        [Fact]
        public void SimilarValueObjectsShouldHaveSameHashCode()
        {
            // Arrange
            var valueObject1 = new FakeValueObject() { A = 5, B = "Hello" };
            var valueObject2 = new FakeValueObject() { A = 5, B = "Hello" };

            // Act
            var sameHashCode = valueObject1.GetHashCode() == valueObject2.GetHashCode();

            // Assert
            sameHashCode.Should().BeTrue();
        }

        [Fact]
        public void SimilarValueObjectsShouldNotHaveSameHashCode()
        {
            // Arrange
            var valueObject1 = new FakeValueObject() { A = 5, B = "Hello" };
            var valueObject2 = new FakeValueObject() { A = 6, B = "World" };

            // Act
            var sameHashCode = valueObject1.GetHashCode() == valueObject2.GetHashCode();

            // Assert
            sameHashCode.Should().BeFalse();
        }

    }
}
