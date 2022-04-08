using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.SourceControl;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.SourceControl
{
    public class SourceControlBusinessLogicDummy : ISourceControlBusinessLogic
    {
        public void Evaluation()
        {
            //no implementation.
        }

        public bool CanBeFixed()
        {
            return true;
        }

        public Status GetStatus()
        {
            return Status.NotSelected;
        }

        public SourceControlResultContent Result()
        {
            return new SourceControlResultContent();
        }

        public void Fix()
        {
            //no implementation.
        }
    }
}