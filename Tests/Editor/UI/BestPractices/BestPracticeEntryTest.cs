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
            var bpe = BestPracticeEntryFactory.Create<BestPracticeDummy>(
                BestPracticeName.Placeholder, new PersistorStub(), new WebAccessDummy(), new BusinessLogicStub(),
                new ResultEditorFactoryDummy());
            Assert.True(bpe.IsActive());
        }

        [Test]
        public void TestSwitchActive()
        {
            var bpe = BestPracticeEntryFactory.Create<BestPracticeDummy>(
                BestPracticeName.Placeholder, new PersistorStub(), new WebAccessDummy(), new BusinessLogicStub(),
                new ResultEditorFactoryDummy());
            bpe.SwitchActive();
            Assert.False(bpe.IsActive());
        }

        [Test]
        public void TestHasResultDefaultValues()
        {
            var bpe = BestPracticeEntryFactory.Create<BestPracticeDummy>(
                BestPracticeName.Placeholder, new PersistorStub(), new WebAccessDummy(), new BusinessLogicStub(),
                new ResultEditorFactoryDummy());
            Assert.False(bpe.HasResult());
        }

        [Test]
        public void TestIsRunningDefaultValues()
        {
            var bpe = BestPracticeEntryFactory.Create<BestPracticeDummy>(
                BestPracticeName.Placeholder, new PersistorStub(), new WebAccessDummy(), new BusinessLogicStub(),
                new ResultEditorFactoryDummy());
            Assert.False(bpe.IsRunning());
        }

        [Test]
        public void TestStop()
        {
            var bpe = BestPracticeEntryFactory.Create<BestPracticeDummy>(
                BestPracticeName.Placeholder, new PersistorStub(), new WebAccessDummy(), new BusinessLogicStub(),
                new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bpe.Stop);
        }

        [Test]
        public void TestHasFix()
        {
            var bpe = BestPracticeEntryFactory.Create<BestPracticeDummy>(
                BestPracticeName.Placeholder, new PersistorStub(), new WebAccessDummy(), new BusinessLogicStub(),
                new ResultEditorFactoryDummy());
            Assert.False(bpe.HasFix());
        }

        [Test]
        public void TestFix()
        {
            var bpe = BestPracticeEntryFactory.Create<BestPracticeDummy>(
                BestPracticeName.Placeholder, new PersistorStub(), new WebAccessDummy(), new BusinessLogicStub(),
                new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bpe.Fix);
        }

        [Test]
        public void TestBestPracticeEntryUI()
        {
            var bpe = BestPracticeEntryFactory.Create<BestPracticeDummy>(
                BestPracticeName.Placeholder, new PersistorStub(), new WebAccessDummy(), new BusinessLogicStub(),
                new ResultEditorFactoryDummy());
            Assert.Throws<NullReferenceException>(bpe.BestPracticeEntryUI);
        }
    }
}