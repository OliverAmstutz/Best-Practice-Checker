using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio;
using BestPracticeChecker.Tests.Editor.BusinessLogic.Persistor;
using BestPracticeChecker.Tests.Editor.BusinessLogic.WebAccess;
using BestPracticeChecker.Tests.Editor.UI.BestPractices;
using NUnit.Framework;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.TextureRatio
{
    public class TextureRatioTest
    {
        [Test]
        public void TestHasFix()
        {
            const BestPracticeName name = BestPracticeName.Texture_Ratio;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio.TextureRatio) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio.TextureRatio>(name,
                        new PersistorStub(), new WebAccessDummy(),
                        new TextureRatioBusinessLogicFake(false, Status.NotCalculated, new TextureRatioResultContent()),
                        new ResultEditorFactoryDummy());
            Assert.False(bP.HasFix());
        }

        [Test]
        public void TestGetResult()
        {
            const BestPracticeName name = BestPracticeName.Texture_Ratio;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio.TextureRatio) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio.TextureRatio>(name,
                        new PersistorStub(), new WebAccessDummy(),
                        new TextureRatioBusinessLogicFake(false, Status.NotCalculated, new TextureRatioResultContent()),
                        new ResultEditorFactoryDummy());
            Assert.NotNull(bP.GetResult());
        }

        [Test]
        public void TestEvaluation()
        {
            const BestPracticeName name = BestPracticeName.Texture_Ratio;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio.TextureRatio) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio.TextureRatio>(name,
                        new PersistorStub(), new WebAccessDummy(),
                        new TextureRatioBusinessLogicFake(false, Status.NotCalculated, new TextureRatioResultContent()),
                        new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bP.Run);
        }

        [Test]
        public void TestFix()
        {
            const BestPracticeName name = BestPracticeName.Texture_Ratio;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio.TextureRatio) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio.TextureRatio>(name,
                        new PersistorStub(), new WebAccessDummy(),
                        new TextureRatioBusinessLogicFake(false, Status.NotCalculated, new TextureRatioResultContent()),
                        new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bP.Fix);
        }

        [Test]
        public void TestShowResult()
        {
            const BestPracticeName name = BestPracticeName.Texture_Ratio;
            var bP =
                (BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio.TextureRatio) BestPracticeFactory
                    .Create<BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio.TextureRatio>(name,
                        new PersistorStub(), new WebAccessDummy(),
                        new TextureRatioBusinessLogicFake(false, Status.NotCalculated, new TextureRatioResultContent()),
                        new ResultEditorFactoryDummy());
            Assert.DoesNotThrow(bP.ShowResults);
        }
    }
}