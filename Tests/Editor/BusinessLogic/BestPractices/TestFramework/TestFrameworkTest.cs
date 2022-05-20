using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using BestPracticeChecker.Tests.Editor.BusinessLogic.Persistor;
using BestPracticeChecker.Tests.Editor.BusinessLogic.WebAccess;
using BestPracticeChecker.Tests.Editor.UI.BestPractices;
using NUnit.Framework;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.TestFramework
{
    public sealed class TestFrameworkTest
    {
        [Test]
        public void TestHasFix()
        {
            const BestPracticeName name = BestPracticeName.Test_Framework;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework.TestFramework) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework.TestFramework>(name, new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(), new ResultEditorFactoryDummy());
            Assert.False(bP.HasFix());
        }

        [Test]
        public void TestGetResult()
        {
            const BestPracticeName name = BestPracticeName.Test_Framework;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework.TestFramework) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework.TestFramework>(name, new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(), new ResultEditorFactoryDummy());
            Assert.NotNull(bP.GetResult());
        }

        [Test]
        public void TestFix()
        {
            const BestPracticeName name = BestPracticeName.Test_Framework;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework.TestFramework) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework.TestFramework>(name, new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(), new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bP.Fix);
        }

        [Test]
        public void TestEvaluation()
        {
            const BestPracticeName name = BestPracticeName.Test_Framework;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework.TestFramework) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework.TestFramework>(name, new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(), new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bP.Run);
        }


        [Test]
        public void TestShowResults()
        {
            const BestPracticeName name = BestPracticeName.Test_Framework;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework.TestFramework) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework.TestFramework>(name, new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(), new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bP.ShowResults);
        }
    }
}