namespace BestPracticeChecker.Editor.BusinessLogic.PackageUtility
{
    /// <summary>
    ///     Utility for Unity's version control status.
    /// </summary>
    public interface IVersionControlStatus
    {
        /// <summary>
        ///     Evaluates Unity's version control status.
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public UnityVersionControl Evaluate(string mode);
    }
}