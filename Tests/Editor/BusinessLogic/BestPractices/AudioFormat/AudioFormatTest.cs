using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.AudioFormat;
using BestPracticeChecker.Tests.Editor.BusinessLogic.Persistor;
using BestPracticeChecker.Tests.Editor.BusinessLogic.WebAccess;
using BestPracticeChecker.Tests.Editor.UI.BestPractices;
using NUnit.Framework;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.AudioFormat
{
    public class AudioFormatTest
    {
        [Test]
        public void TestHasFix()
        {
            const BestPracticeName name = BestPracticeName.Audio_Format;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.AudioFormat.AudioFormat) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.AudioFormat.AudioFormat>(name,
                        new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(true, new AudioFormatResultContent(), Status.NotCalculated),
                        new ResultEditorFactoryDummy());
            Assert.False(bP.HasFix());
        }

        [Test]
        public void TestGetResult()
        {
            const BestPracticeName name = BestPracticeName.Audio_Format;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.AudioFormat.AudioFormat) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.AudioFormat.AudioFormat>(name,
                        new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(false, new AudioFormatResultContent(), Status.NotCalculated),
                        new ResultEditorFactoryDummy());
            Assert.NotNull(bP.GetResult());
        }

        [Test]
        public void TestEvaluation()
        {
            const BestPracticeName name = BestPracticeName.Audio_Format;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.AudioFormat.AudioFormat) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.AudioFormat.AudioFormat>(name,
                        new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(false, new AudioFormatResultContent(), Status.NotCalculated),
                        new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bP.Run);
        }

        [Test]
        public void TestFix()
        {
            const BestPracticeName name = BestPracticeName.Audio_Format;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.AudioFormat.AudioFormat) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.AudioFormat.AudioFormat>(name,
                        new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(false, new AudioFormatResultContent(), Status.NotCalculated),
                        new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bP.Fix);
        }

        [Test]
        public void TestShowResult()
        {
            const BestPracticeName name = BestPracticeName.Audio_Format;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.AudioFormat.AudioFormat) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.AudioFormat.AudioFormat>(name,
                        new PersistorStub(), new WebAccessDummy(),
                        new BusinessLogicStub(false, new AudioFormatResultContent(), Status.NotCalculated),
                        new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bP.ShowResults);
        }
    }
}