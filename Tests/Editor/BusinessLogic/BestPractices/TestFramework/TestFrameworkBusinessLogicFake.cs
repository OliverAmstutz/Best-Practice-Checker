using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.TestFramework
{
    public class TestFrameworkBusinessLogicFake : ITestFrameworkBusinessLogic
    {
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
            return true;
        }

        public TestFrameworkResultContent Result()
        {
            return new TestFrameworkResultContent();
        }

        public Status GetStatus()
        {
            return Status.NotSelected;
        }
    }
}