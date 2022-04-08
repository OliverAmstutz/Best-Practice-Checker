using System;
using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework;
using BestPracticeChecker.Editor.BusinessLogic.PackageUtility;
using BestPracticeChecker.Tests.Editor.BusinessLogic.PackageUtility;
using NUnit.Framework;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.TestFramework
{
    public class TestFrameworkBusinessLogicTest
    {
        [Test]
        public void TestEvaluationOk()
        {
            var bL = new TestFrameworkBusinessLogic(new PackageUtilityMock(true, true, PackageStatus.UpToDate));
            bL.Evaluation();
            Assert.That(bL.Result().Content().Contains("You have the Unity test framework installed and its up to date"));
            Assert.IsFalse(bL.CanBeFixed());
            Assert.That(bL.GetStatus() == Status.Ok);
        }
        
        [Test]
        public void TestEvaluationWarning()
        {
            var bL = new TestFrameworkBusinessLogic(new PackageUtilityMock(true, false, PackageStatus.Outdated));
            bL.Evaluation();
            Assert.That(bL.Result().Content().Contains("You have the Unity test framework installed, but its not up to date!"));
            Assert.IsTrue(bL.CanBeFixed());
            Assert.That(bL.GetStatus() == Status.Warning);
        }
        
        [Test]
        public void TestEvaluationError()
        {
            var bL = new TestFrameworkBusinessLogic(new PackageUtilityMock(false, false, PackageStatus.NotInstalled));
            bL.Evaluation();
            Assert.That(bL.Result().Content().Contains("You do not have the Unity test framework installed!"));
            Assert.IsTrue(bL.CanBeFixed());
            Assert.That(bL.GetStatus() == Status.Error);
        }
        
        [Test]
        public void TestEvaluationPackageUtilityNull()
        {
            var bL = new TestFrameworkBusinessLogic(null);
            Assert.Throws<NullReferenceException>(bL.Evaluation);
            
        }
        
        [Test]
        public void TestFix()
        {
            Assert.DoesNotThrow(new TestFrameworkBusinessLogic(new PackageUtilityMock(true, true, PackageStatus.UpToDate)).Fix);
        }
    }
}