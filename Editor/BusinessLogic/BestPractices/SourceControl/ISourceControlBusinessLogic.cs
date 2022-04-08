namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.SourceControl
{
    /// <summary>
    ///     Business logic for source control best practice.
    /// </summary>
    public interface ISourceControlBusinessLogic : IBusinessLogic
    {
        /// <summary>
        ///     Evaluates the best practice.
        /// </summary>
        public void Evaluation();

        /// <summary>
        ///     Returns a boolean if the best practice is fixable.
        /// </summary>
        /// <returns></returns>
        public bool CanBeFixed();

        /// <summary>
        ///     Returns the best practice Status.
        /// </summary>
        /// <returns></returns>
        public Status GetStatus();

        /// <summary>
        ///     Returns the result.
        /// </summary>
        /// <returns></returns>
        public SourceControlResultContent Result();

        /// <summary>
        ///     Fixes the best practice.
        /// </summary>
        public void Fix();
    }
}