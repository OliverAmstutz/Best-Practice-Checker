namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.AudioFormat
{
    /// <summary>
    ///     Utility for Unity's supported audio formats.
    /// </summary>
    public interface IAudioFormatType
    {
        /// <summary>
        ///     Evaluates Unity's audio format.
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public SupportedAudioFormat Evaluate(string mode);
    }
}