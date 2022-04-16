namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.AudioFormat
{
    /// <summary>
    ///     Utility for Unity's version control status.
    /// </summary>
    public interface IAudioFormatType
    {
        /// <summary>
        ///     Evaluates Unity's version control status.
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public SupportedAudioFormat Evaluate(string mode);
    }
}