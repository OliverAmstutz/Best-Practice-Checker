using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.SourceControl;
using BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.SourceControl;
using BestPracticeChecker.Tests.Editor.BusinessLogic.Persistor;
using BestPracticeChecker.Tests.Editor.BusinessLogic.WebAccess;
using BestPracticeChecker.Tests.Editor.UI.BestPractices;
using NUnit.Framework;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.SourceControl
{
    public class SourceControlTest
    {
        [Test]
        public void TestFix()
        {
            var sourceControl =
                BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.SourceControl.SourceControl>(
                        BestPracticeName.Source_Control, new PersistorStub(), new WebAccessDummy(),
                        new SourceControlBusinessLogicDummy(), new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(sourceControl.Fix);
        }

        [Test]
        public void TestShowResult()
        {
            var sourceControl =
                BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.SourceControl.SourceControl>(
                        BestPracticeName.Source_Control, new PersistorStub(), new WebAccessDummy(),
                        new SourceControlBusinessLogicDummy(), new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(sourceControl.ShowResults);
        }

        [Test]
        public void TestHasFix()
        {
            var sourceControl =
                BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.SourceControl.SourceControl>(
                        BestPracticeName.Source_Control, new PersistorStub(), new WebAccessDummy(),
                        new SourceControlBusinessLogicDummy(), new ResultEditorFactoryDummy());
            Assert.IsFalse(sourceControl.HasFix());
        }

        [Test]
        public void TestGetResult()
        {
            var sourceControl =
                BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.SourceControl.SourceControl>(
                        BestPracticeName.Source_Control, new PersistorStub(), new WebAccessDummy(),
                        new SourceControlBusinessLogicDummy(), new ResultEditorFactoryDummy());
            SourceControlResultContent result = (SourceControlResultContent) sourceControl.GetResult();
            Assert.That(result.Status() == SourceControlStatus.NotInitialised);
        }

        [Test]
        public void TestEvaluate()
        {
            var sourceControl =
                BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.SourceControl.SourceControl>(
                        BestPracticeName.Source_Control, new PersistorStub(), new WebAccessDummy(),
                        new SourceControlBusinessLogicDummy(), new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(sourceControl.Run);
        }
    }
}