using System;
using NRack.Adapters;
using NRack.Mock;
using NUnit.Framework;

namespace NRack.Specs
{
    [TestFixture]
    public class ShowExceptions_Specs
    {
        [Test]
        public void Should_Catch_Exceptions()
        {
            dynamic res = null;

            var req = new MockRequest(new ShowExceptions(ApplicationFactory.Create(
                env => { throw new InvalidOperationException(); })));

            Assert.DoesNotThrow(delegate { res = req.Get("/"); });

            Assert.AreEqual(500, res.Status);

            Assert.IsTrue(res.Body.ToString().Contains("InvalidOperationException"));
            Assert.IsTrue(res.Body.ToString().Contains("ShowExceptions"));
        }
    }
}