namespace BestPracticeChecker.Editor.BusinessLogic.PreferenceUtility
{
    /// <summary>
    /// Unity preferences evaluation.
    /// </summary>
    public interface IPreferences
    {
        /// <summary>
        /// Returns the used IDE enum.
        /// </summary>
        /// <returns></returns>
        public Ide UsedIde();
    }
}