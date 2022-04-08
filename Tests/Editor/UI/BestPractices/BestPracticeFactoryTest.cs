using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework;
using NUnit.Framework;

namespace BestPracticeChecker.Tests.Editor.UI.BestPractices
{
    public class BestPracticeFactoryTest
    {
        [Test]
        public void IntegrationTestInstantiateBestPractice()
        {
            var bP = BestPracticeFactory.Create<TestFramework>(BestPracticeName.Test_Framework);
            Assert.NotNull(bP);
        }
    }
}