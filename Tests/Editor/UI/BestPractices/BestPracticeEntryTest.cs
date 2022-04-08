using System;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using BestPracticeChecker.Editor.UI.BestPractices;
using BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices;
using BestPracticeChecker.Tests.Editor.BusinessLogic.Persistor;
using BestPracticeChecker.Tests.Editor.BusinessLogic.WebAccess;
using NUnit.Framework;

namespace BestPracticeChecker.Tests.Editor.UI.BestPractices
{
    public class BestPracticeEntryTest
    {
        [Test]
        public void TestIsActiveDefaultValue()
        {
            var bpe = BestPracticeEntryFactory.Create<BestPracticeStub>(
                BestPracticeName.Placeholder, new PersistorStub(), new WebAccessDummy(), new BusinessLogicDummy(),
                new ResultEditorFactoryDummy());
            Assert.True(bpe.IsActive());
        }

        [Test]
        public void TestSwitchActive()
        {
            var bpe = BestPracticeEntryFactory.Create<BestPracticeStub>(
                BestPracticeName.Placeholder, new PersistorStub(), new WebAccessDummy(), new BusinessLogicDummy(),
                new ResultEditorFactoryDummy());
            bpe.SwitchActive();
            Assert.False(bpe.IsActive());
        }

        [Test]
        [Ignore("NullPointerException in EditorCoroutine - potential bug in package?")]
        public void TestRunBestPractice()
        {
            var bpe = BestPracticeEntryFactory.Create<BestPracticeStub>(
                BestPracticeName.Test_Framework, new PersistorStub(), new WebAccessDummy(), new BusinessLogicDummy(),
                new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bpe.RunBestPractice);
        }

        [Test]
        public void TestHasResultDefaultValues()
        {
            var bpe = BestPracticeEntryFactory.Create<BestPracticeStub>(
                BestPracticeName.Placeholder, new PersistorStub(), new WebAccessDummy(), new BusinessLogicDummy(),
                new ResultEditorFactoryDummy());
            Assert.False(bpe.HasResult());
        }

        [Test]
        public void TestIsRunningDefaultValues()
        {
            var bpe = BestPracticeEntryFactory.Create<BestPracticeStub>(
                BestPracticeName.Placeholder, new PersistorStub(), new WebAccessDummy(), new BusinessLogicDummy(),
                new ResultEditorFactoryDummy());
            Assert.False(bpe.IsRunning());
        }

        [Test]
        public void TestStop()
        {
            var bpe = BestPracticeEntryFactory.Create<BestPracticeStub>(
                BestPracticeName.Placeholder, new PersistorStub(), new WebAccessDummy(), new BusinessLogicDummy(),
                new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bpe.Stop);
        }

        [Test]
        public void TestHasFix()
        {
            var bpe = BestPracticeEntryFactory.Create<BestPracticeStub>(
                BestPracticeName.Placeholder, new PersistorStub(), new WebAccessDummy(), new BusinessLogicDummy(),
                new ResultEditorFactoryDummy());
            Assert.False(bpe.HasFix());
        }

        [Test]
        public void TestFix()
        {
            var bpe = BestPracticeEntryFactory.Create<BestPracticeStub>(
                BestPracticeName.Placeholder, new PersistorStub(), new WebAccessDummy(), new BusinessLogicDummy(),
                new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bpe.Fix);
        }

        [Test]
        public void TestBestPracticeEntryUI()
        {
            var bpe = BestPracticeEntryFactory.Create<BestPracticeStub>(
                BestPracticeName.Placeholder, new PersistorStub(), new WebAccessDummy(), new BusinessLogicDummy(),
                new ResultEditorFactoryDummy());
            Assert.Throws<NullReferenceException>(bpe.BestPracticeEntryUI);
        }
    }
}