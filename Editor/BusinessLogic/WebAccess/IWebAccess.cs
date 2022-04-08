using BestPracticeChecker.Editor.BusinessLogic.BestPractices;

namespace BestPracticeChecker.Editor.BusinessLogic.WebAccess
{
    /// <summary>
    ///     Access web page.
    /// </summary>
    public interface IWebAccess
    {
        /// <summary>
        ///     Opens web page of best practice.
        /// </summary>
        /// <param name="bp"></param>
        void OpenDocumentation(BestPracticeName bp);
    }
}