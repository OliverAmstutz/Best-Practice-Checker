namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices
{
    /// <summary>
    ///     Best practice business logic for result type T.
    /// </summary>
    public interface IBusinessLogic<out T> where T : IResult
    {
        /// <summary>
        ///     Evaluates the best practice.
        /// </summary>
        public void Evaluation();

        /// <summary>
        ///     Fixes the best practice.
        /// </summary>
        public void Fix();

        /// <summary>
        ///     Returns a boolean if the best practice is fixable.
        /// </summary>
        /// <returns></returns>
        public bool CanBeFixed();

        /// <summary>
        ///     Returns the result of type T.
        /// </summary>
        /// <returns></returns>
        public T Result();

        /// <summary>
        ///     Returns the best practice Status.
        /// </summary>
        /// <returns></returns>
        public Status GetStatus();
    }
}