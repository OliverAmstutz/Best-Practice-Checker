namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio
{
    /// <summary>
    ///     Business logic for texture ratio best practice.
    /// </summary>
    public interface ITextureRatioBusinessLogic : IBusinessLogic
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
        ///     Returns the result.
        /// </summary>
        /// <returns></returns>
        public TextureRatioResultContent Result();

        /// <summary>
        ///     Returns the best practice Status.
        /// </summary>
        /// <returns></returns>
        public Status GetStatus();
    }
}