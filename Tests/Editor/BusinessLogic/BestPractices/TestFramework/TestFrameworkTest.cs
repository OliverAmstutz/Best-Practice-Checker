using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using BestPracticeChecker.Tests.Editor.BusinessLogic.Persistor;
using BestPracticeChecker.Tests.Editor.BusinessLogic.WebAccess;
using BestPracticeChecker.Tests.Editor.UI.BestPractices;
using NUnit.Framework;
using UnityEngine;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.TestFramework
{
    public class TestFrameworkTest
    {
        [Test]
        public void TestHasFix()
        {
            const BestPracticeName name = BestPracticeName.Test_Framework;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework.TestFramework) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework.TestFramework>(name,
                        new PersistorStub(), new WebAccessDummy(), new TestFrameworkBusinessLogicFake(),
                        new ResultEditorFactoryDummy());
            Assert.False(bP.HasFix());
        }

        [Test]
        public void TestGetResult()
        {
            const BestPracticeName name = BestPracticeName.Test_Framework;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework.TestFramework) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework.TestFramework>(name,
                        new PersistorStub(), new WebAccessDummy(), new TestFrameworkBusinessLogicFake(),
                        new ResultEditorFactoryDummy());
            Assert.NotNull(bP.GetResult());
        }

        [Test]
        public void TestFix()
        {
            const BestPracticeName name = BestPracticeName.Test_Framework;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework.TestFramework) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework.TestFramework>(name,
                        new PersistorStub(), new WebAccessDummy(), new TestFrameworkBusinessLogicFake(),
                        new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bP.Fix);
        }

        [Test]
        public void TestEvaluation()
        {
            const BestPracticeName name = BestPracticeName.Test_Framework;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework.TestFramework) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework.TestFramework>(name,
                        new PersistorStub(), new WebAccessDummy(), new TestFrameworkBusinessLogicFake(),
                        new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bP.Run);
        }


        [Test]
        public void TestShowResults()
        {
            const BestPracticeName name = BestPracticeName.Test_Framework;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework.TestFramework) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework.TestFramework>(name,
                        new PersistorStub(), new WebAccessDummy(), new TestFrameworkBusinessLogicFake(),
                        new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bP.ShowResults);
        }


        [Test]
        [Ignore("Integration test does not work as intended.")]
        public void IntegrationTestIsDirtyUpdate()
        {
            //for some reason the package event is not triggered when installing/deinstalling through script...
            const BestPracticeName name = BestPracticeName.Test_Framework;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework.TestFramework) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework.TestFramework>(name,
                        new PersistorStub(), new WebAccessDummy(), new TestFrameworkBusinessLogicFake(),
                        new ResultEditorFactoryDummy());
            var pu = new BestPracticeChecker.Editor.BusinessLogic.PackageUtility.PackageUtility();
            var packageName = "com.unity.2d.sprite";
            pu.InstallLatestPackage(packageName);
            var newPu = new BestPracticeChecker.Editor.BusinessLogic.PackageUtility.PackageUtility();
            newPu.RemovePackage(packageName);
            Debug.Log(bP.GetCalculationStatus());
            Assert.That(bP.GetCalculationStatus() == Status.NeedUpdate);
        }
    }
}