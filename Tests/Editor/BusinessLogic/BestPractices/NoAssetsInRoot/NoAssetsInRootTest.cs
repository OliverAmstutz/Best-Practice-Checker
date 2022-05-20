using System;
using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.NoAssetsInRoot;
using BestPracticeChecker.Tests.Editor.BusinessLogic.Persistor;
using BestPracticeChecker.Tests.Editor.BusinessLogic.WebAccess;
using BestPracticeChecker.Tests.Editor.UI.BestPractices;
using NUnit.Framework;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.NoAssetsInRoot
{
    public sealed class NoAssetsInRootTest
    {
        [Test]
        public void TestHasFix()
        {
            const BestPracticeName name = BestPracticeName.No_Assets_In_Root;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.NoAssetsInRoot.NoAssetsInRoot) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.NoAssetsInRoot.NoAssetsInRoot>(name, new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(true, new NoAssetsInRootResultContent(), Status.NotCalculated), new ResultEditorFactoryDummy());
            Assert.IsFalse(bP.HasFix());
        }

        [Test]
        public void TestGetResult()
        {
            const BestPracticeName name = BestPracticeName.No_Assets_In_Root;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.NoAssetsInRoot.NoAssetsInRoot) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.NoAssetsInRoot.NoAssetsInRoot>(name, new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(false, new NoAssetsInRootResultContent(), Status.NotCalculated), new ResultEditorFactoryDummy());
            Assert.NotNull(bP.GetResult());
        }

        [Test]
        public void TestEvaluation()
        {
            const BestPracticeName name = BestPracticeName.No_Assets_In_Root;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.NoAssetsInRoot.NoAssetsInRoot) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.NoAssetsInRoot.NoAssetsInRoot>(name, new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(false, new NoAssetsInRootResultContent(), Status.NotCalculated), new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bP.Run);
        }

        [Test]
        public void TestFix()
        {
            const BestPracticeName name = BestPracticeName.No_Assets_In_Root;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.NoAssetsInRoot.NoAssetsInRoot) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.NoAssetsInRoot.NoAssetsInRoot>(name, new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(false, new NoAssetsInRootResultContent(), Status.NotCalculated), new ResultEditorFactoryDummy());
            Assert.Throws<NotImplementedException>(bP.Fix);
        }

        [Test]
        public void TestShowResult()
        {
            const BestPracticeName name = BestPracticeName.No_Assets_In_Root;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.NoAssetsInRoot.NoAssetsInRoot) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.NoAssetsInRoot.NoAssetsInRoot>(name, new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(false, new NoAssetsInRootResultContent(), Status.NotCalculated), new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bP.ShowResults);
        }
    }
}