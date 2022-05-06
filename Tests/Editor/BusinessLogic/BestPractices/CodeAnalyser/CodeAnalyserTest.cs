using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeAnalyser;
using BestPracticeChecker.Tests.Editor.BusinessLogic.Persistor;
using BestPracticeChecker.Tests.Editor.BusinessLogic.WebAccess;
using BestPracticeChecker.Tests.Editor.UI.BestPractices;
using NUnit.Framework;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.CodeAnalyser
{
    public class CodeAnalyserTest
    {
        [Test]
        public void TestFix()
        {
            var codeAnalyser =
                BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeAnalyser.CodeAnalyser>(
                        BestPracticeName.Source_Control, new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(true, new CodeAnalyserResultContent(), Status.Error),
                        new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(codeAnalyser.Fix);
        }

        [Test]
        public void TestShowResult()
        {
            var codeAnalyser =
                BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeAnalyser.CodeAnalyser>(
                        BestPracticeName.Source_Control, new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(false, new CodeAnalyserResultContent(), Status.Ok),
                        new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(codeAnalyser.ShowResults);
        }

        [Test]
        public void TestHasFix()
        {
            var codeAnalyser =
                BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeAnalyser.CodeAnalyser>(
                        BestPracticeName.Source_Control, new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(true, new CodeAnalyserResultContent(), Status.Warning),
                        new ResultEditorFactoryDummy());
            Assert.IsFalse(codeAnalyser.HasFix());
        }

        [Test]
        public void TestGetResult()
        {
            var codeAnalyser =
                BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeAnalyser.CodeAnalyser>(
                        BestPracticeName.Source_Control, new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(true, new CodeAnalyserResultContent(), Status.NeedUpdate),
                        new ResultEditorFactoryDummy());
            var result = (CodeAnalyserResultContent) codeAnalyser.GetResult();
            Assert.That(result.Content().Contains("Something went wrong in the code analyser initialization!"));
        }

        [Test]
        public void TestEvaluate()
        {
            var codeAnalyser =
                BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeAnalyser.CodeAnalyser>(
                        BestPracticeName.Source_Control, new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(true, new CodeAnalyserResultContent(), Status.Ok),
                        new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(codeAnalyser.Run);
        }
    }
}