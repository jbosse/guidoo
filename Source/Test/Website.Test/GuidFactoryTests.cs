using System.Collections.Generic;
using NUnit.Framework;
using Website.App;

namespace Website.Test
{
    [TestFixture]
    public class GuidFactoryTests
    {
        [Test]
        public void it_should_create_a_guid()
        {
            var sut = new GuidFactory();
            var result = sut.Create();
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void it_should_not_repeat_a_guid()
        {
            var created = new List<string>();
            var sut = new GuidFactory();
            for (var i = 0; i < 1000; i++)
            {
                var guid = sut.Create().ToString();
                Assert.That(created, Is.Not.Contains(guid));
                created.Add(guid);
            }
        }
    }
}