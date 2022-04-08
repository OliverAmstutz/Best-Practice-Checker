using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.TextureRatio
{
    public class TextureRatioBusinessLogicFake : ITextureRatioBusinessLogic
    {
        private readonly bool _canBeFixed;
        private readonly TextureRatioResultContent _result;
        private readonly Status _status;

        public TextureRatioBusinessLogicFake(bool canBeFixed, Status status, TextureRatioResultContent result)
        {
            _canBeFixed = canBeFixed;
            _status = status;
            _result = result;
        }

        public void Evaluation()
        {
            //no implementation.
        }

        public void Fix()
        {
            //no implementation.
        }

        public bool CanBeFixed()
        {
            return _canBeFixed;
        }

        public TextureRatioResultContent Result()
        {
            return _result;
        }

        public Status GetStatus()
        {
            return _status;
        }
    }
}