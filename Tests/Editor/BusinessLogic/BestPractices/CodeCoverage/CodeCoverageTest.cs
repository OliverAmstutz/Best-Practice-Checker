using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeCoverage;
using BestPracticeChecker.Tests.Editor.BusinessLogic.Persistor;
using BestPracticeChecker.Tests.Editor.BusinessLogic.WebAccess;
using BestPracticeChecker.Tests.Editor.UI.BestPractices;
using NUnit.Framework;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.CodeCoverage
{
    public class CodeCoverageTest
    {
        [Test]
        public void TestFix()
        {
            var codeCoverage =
                BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeCoverage.CodeCoverage>(
                        BestPracticeName.Source_Control, new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(true, new CodeCoverageResultContent(), Status.Error),
                        new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(codeCoverage.Fix);
        }

        [Test]
        public void TestShowResult()
        {
            var codeCoverage =
                BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeCoverage.CodeCoverage>(
                        BestPracticeName.Source_Control, new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(false, new CodeCoverageResultContent(), Status.Ok),
                        new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(codeCoverage.ShowResults);
        }

        [Test]
        public void TestHasFix()
        {
            var codeCoverage =
                BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeCoverage.CodeCoverage>(
                        BestPracticeName.Source_Control, new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(true, new CodeCoverageResultContent(), Status.Warning),
                        new ResultEditorFactoryDummy());
            Assert.IsFalse(codeCoverage.HasFix());
        }

        [Test]
        public void TestGetResult()
        {
            var codeCoverage =
                BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeCoverage.CodeCoverage>(
                        BestPracticeName.Source_Control, new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(true, new CodeCoverageResultContent(), Status.NeedUpdate),
                        new ResultEditorFactoryDummy());
            var result = (CodeCoverageResultContent) codeCoverage.GetResult();
            Assert.That(result.Content().Contains("Something went wrong in the code coverage initialization!"));
        }

        [Test]
        public void TestEvaluate()
        {
            var codeCoverage =
                BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeCoverage.CodeCoverage>(
                        BestPracticeName.Source_Control, new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(true, new CodeCoverageResultContent(), Status.Ok),
                        new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(codeCoverage.Run);
        }
    }
}