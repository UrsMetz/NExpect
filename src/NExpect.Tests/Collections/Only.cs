﻿using NExpect.Exceptions;
using NUnit.Framework;
using static PeanutButter.RandomGenerators.RandomValueGen;
using PeanutButter.RandomGenerators;
using static NExpect.Expectations;
// ReSharper disable InconsistentNaming

namespace NExpect.Tests.Collections
{
    [TestFixture]
    public class Only_N
    {
        [Test]
        public void OperatingOnCollectionOfStrings_WhenExpecting1Item_AndFinding1_ShouldNotThrow()
        {
            // Arrange
            var search = GetRandomString(3);
            var collection = new[]
            {
                search
            };

            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Contain.Only(1).Equal.To(search);
                },
                Throws.Nothing);

            // Assert
        }

        [Test]
        public void OperatingOnCollectionOfStrings_WhenExpecting2Items_AndFinding2_ShouldNotThrow()
        {
            // Arrange
            var search = GetRandomString(3);
            var collection = new[]
            {
                search,
                search
            };

            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Contain.Only(2).Equal.To(search);
                },
                Throws.Nothing);

            // Assert
        }

        [Test]
        public void OperatingOnCollectionOfStrings_WhenContainsCorrectNumber_AndNotEqual_ShouldThrow()
        {
            // Arrange
            var search = GetRandomString(3);
            var collection = new[]
            {
                GetAnother(search)
            };

            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Contain.Only(1).Equal.To(search);
                },
                Throws.Exception.TypeOf<UnmetExpectationException>()
                    .With.Message.Contains($"Expected to find only 1 occurrence of \"{search}\" but found 0"));

            // Assert
        }

        [Test]
        public void OperatingOnCollectionOfStrings_WhenExpecting1Item_AndFinding0_ShouldThrow()
        {
            // Arrange
            var collection = new string[] { };
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Contain.Only(1).Items();
                },
                Throws.Exception.TypeOf<UnmetExpectationException>()
                    .With.Message.Contains("Expected to find only 1 occurrence of any string in collection but found a total of 0")
            );

            // Assert
        }
        [Test]
        public void OperatingOnCollectionOfStrings_WhenExpecting1Item_AndFinding0_ShouldThrowWithCustomMessage()
        {
            // Arrange
            var collection = new string[] { };
            var expected = GetRandomString(10);
            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Contain.Only(1).Items(expected);
                },
                Throws.Exception.TypeOf<UnmetExpectationException>()
                    .With.Message.Contains(expected)
            );

            Assert.That(() =>
                {
                    Expect(collection).To.Contain.Only(1).Items(() => expected);
                },
                Throws.Exception.TypeOf<UnmetExpectationException>()
                    .With.Message.Contains(expected)
            );

            // Assert
        }

        [Test]
        public void ShouldBreakOnCountMisMatch()
        {
            // Arrange
            var items = new[] { 1, 2, 3 };
            // Act
            Assert.That(() =>
            {
                Expect(items)
                    .To.Contain.Only(1)
                    .Matched.By(i => i == 1);
            }, Throws.Exception.InstanceOf<UnmetExpectationException>());
            Assert.That(() =>
            {
                Expect(items)
                    .Not.To.Contain.Only(1)
                    .Matched.By(i => i == 1);
            }, Throws.Nothing);
            // Assert
        }

        [Test]
        public void AnyShouldPassForOneMatch()
        {
            // Arrange
            var items = new[] { 1, 2, 3 };
            // Act
            Assert.That(() =>
            {
                Expect(items)
                    .To.Contain.Any
                    .Matched.By(i => i == 1);
            }, Throws.Nothing);
            // Assert
        }

        [Test]
        public void OperatingOnCollectionOfStrings_WhenExpecting1Item_AndFinding3_ShouldThrow()
        {
            // Arrange
            var search = GetRandomString(3);
            var other1 = GetAnother(search);
            var other2 = GetAnother<string>(new[] {search, other1});
            var other3 = GetAnother<string>(new[] {search, other1, other2});
            var collection = new[]
            {
                other1,
                other2,
                other3
            }.Randomize();

            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Contain.Only(1).Equal.To(search);
                },
                Throws.Exception.TypeOf<UnmetExpectationException>()
                    .With.Message.Contains($"Expected to find only 1 occurrence of {search} in collection but found a total of 3 items")
            );

            // Assert
        }

        [Test]
        public void OperatingOnCollectionOfStrings_WhenExpecting2Items_AndFinding3_ShouldThrow()
        {
            // Arrange
            var search = GetRandomString(3);
            var other1 = GetAnother(search);
            var other2 = GetAnother<string>(new[] {search, other1});
            var other3 = GetAnother<string>(new[] {search, other1, other2});
            var collection = new[]
            {
                other1,
                other2,
                other3
            }.Randomize();

            // Pre-Assert
            // Act
            Assert.That(() =>
                {
                    Expect(collection).To.Contain.Only(2).Equal.To(search);
                },
                Throws.Exception.TypeOf<UnmetExpectationException>()
                    .With.Message.Contains($"Expected to find only 2 occurrences of {search} in collection but found a total of 3 items")
            );

            // Assert
        }
    }
}