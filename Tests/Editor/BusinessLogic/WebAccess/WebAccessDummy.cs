using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using BestPracticeChecker.Editor.BusinessLogic.WebAccess;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.WebAccess
{
    public sealed class WebAccessDummy : IWebAccess
    {
        public void OpenDocumentation(BestPracticeName bp)
        {
            //No action necessary
        }
    }
}