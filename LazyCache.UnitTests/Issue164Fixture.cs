namespace LazyCache.UnitTests
{
    using System.Collections.Generic;
    using AutoFixture;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class Issue164Fixture
    {
        public class MyClass
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        [Test]
        public void Issue164()
        {
            var sut = new LazyCache.CachingService();

            var fixture = new AutoFixture.Fixture();

            var values = fixture.Create<List<MyClass>>();
            sut.Add("MyKey", values);


            if (sut.TryGetValue<List<MyClass>>("MyKey", out var cachedValues))
            {
                cachedValues.Should().BeEquivalentTo(values);
            }
            else
            {
                Assert.Fail("Not Found");
            }
        }

        [Test]
        public void Issue164_NotInCache()
        {
            var sut = new LazyCache.CachingService();

            var fixture = new AutoFixture.Fixture();

            var values = fixture.Create<List<MyClass>>();
            sut.Add("MyKey1", values);


            if (sut.TryGetValue<List<MyClass>>("MyKey2", out var cachedValues))
            {
                Assert.Fail("Found??");
            }
            else
            {
                cachedValues.Should().BeNull();
            }
        }
    }
}