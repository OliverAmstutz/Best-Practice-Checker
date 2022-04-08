using BestPracticeChecker.Editor.UI;
using BestPracticeChecker.Tests.Editor.BusinessLogic.Persistor;
using NUnit.Framework;

namespace BestPracticeChecker.Tests.Editor.UI
{
    public sealed class CompoundControlsTest
    {
        [Test]
        public void TestObjectInstantiation()
        {
            var cc = CompoundControlsFactory.Create(new PersistorStub());
            Assert.DoesNotThrow(cc.Init);
        }
    }
}