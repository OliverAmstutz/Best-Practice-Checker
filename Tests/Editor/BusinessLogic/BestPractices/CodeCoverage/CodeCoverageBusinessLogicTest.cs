using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeCoverage;
using BestPracticeChecker.Editor.BusinessLogic.PackageUtility;
using BestPracticeChecker.Tests.Editor.BusinessLogic.PackageUtility;
using NUnit.Framework;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.CodeCoverage
{
    public sealed class CodeCoverageBusinessLogicTest
    {
        [Test]
        public void TestEvaluateOk()
        {
            var bL = new CodeCoverageBusinessLogic(new PackageUtilityStub(true, true, PackageStatus.Outdated));
            bL.Evaluation();
            Assert.That(bL.GetStatus() == Status.Ok);
            Assert.IsFalse(bL.CanBeFixed());
        }

        [Test]
        public void TestEvaluateWarning()
        {
            var bL = new CodeCoverageBusinessLogic(new PackageUtilityStub(true, false, PackageStatus.Outdated));
            bL.Evaluation();
            Assert.That(bL.GetStatus() == Status.Warning);
            Assert.IsTrue(bL.CanBeFixed());
        }

        [Test]
        public void TestEvaluateError()
        {
            var bL = new CodeCoverageBusinessLogic(new PackageUtilityStub(false, true, PackageStatus.Outdated));
            bL.Evaluation();
            Assert.That(bL.GetStatus() == Status.Error);
            Assert.IsTrue(bL.CanBeFixed());
        }

        [Test]
        public void TestFix()
        {
            var bL = new CodeCoverageBusinessLogic(new PackageUtilityStub(false, true, PackageStatus.Outdated));
            bL.Evaluation();
            Assert.DoesNotThrow(bL.Fix);
        }

        [Test]
        public void TestResult()
        {
            var bL = new CodeCoverageBusinessLogic(new PackageUtilityStub(false, true, PackageStatus.Outdated));
            Assert.IsNull(bL.Result());
        }
    }
}