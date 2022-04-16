using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices
{
    public class BusinessLogicStub : IBusinessLogic<IResult>
    {
        private readonly bool _canBeFixed;
        private readonly IResult _result;
        private readonly Status _status;

        public BusinessLogicStub(bool canBeFixed, IResult result, Status status)
        {
            _canBeFixed = canBeFixed;
            _result = result;
            _status = status;
        }

        public BusinessLogicStub()
        {
        }

        public void Evaluation()
        {
            //no logic implemented.
        }

        public void Fix()
        {
            //no logic implemented.
        }

        public bool CanBeFixed()
        {
            return _canBeFixed;
        }

        public IResult Result()
        {
            return _result;
        }

        public Status GetStatus()
        {
            return _status;
        }
    }
}