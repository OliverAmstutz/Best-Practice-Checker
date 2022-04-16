using BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeCoverage;
using BestPracticeChecker.Editor.BusinessLogic.PackageUtility;
using NUnit.Framework;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.CodeCoverage
{
    public class CodeCoverageResultContentTest
    {
        [Test]
        public void TestCodeCoverageOk()
        {
            var result = new CodeCoverageResultContent();
            result.Status(PackageStatus.UpToDate);
            Assert.That(result.Content().Contains("Excellent, code coverage package is installed and up-to-date!"));
        }

        [Test]
        public void TestCodeCoverageWarning()
        {
            var result = new CodeCoverageResultContent();
            result.Status(PackageStatus.Outdated);
            Assert.That(result.Content()
                .Contains(
                    "Your code coverage package is outdated. Please update manually or through the \"fix\" button."));
        }


        [Test]
        public void TestCodeCoverageError()
        {
            var result = new CodeCoverageResultContent();
            result.Status(PackageStatus.NotInstalled);
            Assert.That(result.Content()
                .Contains(
                    "You use no code coverage. It is strongly recommended to install and utilise code coverage in your project. You can install code coverage through the \"fix\" button."));
        }


        [Test]
        public void TestDefault()
        {
            var result = new CodeCoverageResultContent();
            result.Status(PackageStatus.NotInitialised);
            Assert.That(result.Content().Contains("Something went wrong in the code coverage initialization!"));
        }
    }
}