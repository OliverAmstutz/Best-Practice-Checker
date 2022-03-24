namespace BestPracticeChecker.Editor.BusinessLogic
{
    /// <summary>
    ///     Interface to best practice result.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        ///     Returns the content of the result.
        /// </summary>
        /// <returns>String.</returns>
        string Content();
    }
}