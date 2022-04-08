using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework;
using NUnit.Framework;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.TestFramework
{
    public sealed class TestFrameworkResultContentTest
    {
        [Test]
        public void TestContentOfStatusOk()
        {
            var content = new TestFrameworkResultContent();
            content.Status(Status.Ok);
            Assert.That(content.Content().Contains("You have the Unity test framework installed and its up to date!"));
        }

        [Test]
        public void TestContentOfStatusWarning()
        {
            var content = new TestFrameworkResultContent();
            content.Status(Status.Warning);
            Assert.That(content.Content()
                .Contains("You have the Unity test framework installed, but its not up to date!"));
        }

        [Test]
        public void TestContentOfStatusError()
        {
            var content = new TestFrameworkResultContent();
            content.Status(Status.Error);
            Assert.That(content.Content().Contains("You do not have the Unity test framework installed!"));
        }

        [Test]
        public void TestContentOfStatusInvalid()
        {
            var content = new TestFrameworkResultContent();
            content.Status(Status.NotCalculated);
            Assert.That(content.Content().Contains("Something went wrong in the test framework initialization!"));
        }
    }
}