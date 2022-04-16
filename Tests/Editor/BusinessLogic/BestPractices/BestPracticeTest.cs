using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using BestPracticeChecker.Tests.Editor.BusinessLogic.Persistor;
using BestPracticeChecker.Tests.Editor.BusinessLogic.WebAccess;
using BestPracticeChecker.Tests.Editor.UI.BestPractices;
using NUnit.Framework;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices
{
    public sealed class BestPracticeTest
    {
        [Test]
        public void TestGetName()
        {
            const BestPracticeName name = BestPracticeName.Placeholder;
            var bP = BestPracticeFactory.Create<BestPracticeDummy>(name, new PersistorStub(),
                new WebAccessDummy(), new BusinessLogicStub(), new ResultEditorFactoryDummy());
            Assert.That(bP.GetName().Equals(name));
        }

        [Test]
        public void TestRun()
        {
            var bP = BestPracticeFactory.Create<BestPracticeDummy>(BestPracticeName.Placeholder,
                new PersistorStub(), new WebAccessDummy(), new BusinessLogicStub(), new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bP.Run);
        }

        [Test]
        public void TestShowDocumentation()
        {
            var bP = BestPracticeFactory.Create<BestPracticeDummy>(BestPracticeName.Placeholder,
                new PersistorStub(), new WebAccessDummy(), new BusinessLogicStub(), new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bP.ShowDocumentation);
        }

        [Test]
        public void TestShowResults()
        {
            var bP = BestPracticeFactory.Create<BestPracticeDummy>(BestPracticeName.Placeholder,
                new PersistorStub(), new WebAccessDummy(), new BusinessLogicStub(), new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bP.ShowResults);
        }

        [Test]
        public void TestGetCalculationStatusNotInitialised()
        {
            var bP = BestPracticeFactory.Create<BestPracticeDummy>(BestPracticeName.Placeholder,
                new PersistorStub(), new WebAccessDummy(), new BusinessLogicStub(), new ResultEditorFactoryDummy());
            Assert.That(bP.GetCalculationStatus().Equals(Status.NotCalculated));
        }

        [Test]
        public void TestFix()
        {
            var bP = BestPracticeFactory.Create<BestPracticeDummy>(BestPracticeName.Placeholder,
                new PersistorStub(), new WebAccessDummy(), new BusinessLogicStub(), new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bP.Fix);
        }

        [Test]
        public void TestHasFix()
        {
            var bP = BestPracticeFactory.Create<BestPracticeDummy>(BestPracticeName.Placeholder,
                new PersistorStub(), new WebAccessDummy(), new BusinessLogicStub(), new ResultEditorFactoryDummy());
            Assert.False(bP.HasFix());
        }

        [Test]
        public void TestStopRun()
        {
            var bP = BestPracticeFactory.Create<BestPracticeDummy>(BestPracticeName.Placeholder,
                new PersistorStub(), new WebAccessDummy(), new BusinessLogicStub(), new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bP.StopRun);
        }
    }
}