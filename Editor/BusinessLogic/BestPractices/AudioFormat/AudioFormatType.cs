namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.AudioFormat
{
    public sealed class AudioFormatType : IAudioFormatType
    {
        public SupportedAudioFormat Evaluate(string mode)
        {
            return mode switch
            {
                ".aif" => SupportedAudioFormat.Aif,
                ".aiff" => SupportedAudioFormat.Aif,
                ".it" => SupportedAudioFormat.It,
                ".mod" => SupportedAudioFormat.Mod,
                ".mp3" => SupportedAudioFormat.Mp3,
                ".ogg" => SupportedAudioFormat.Ogg,
                ".wav" => SupportedAudioFormat.Wav,
                ".xm" => SupportedAudioFormat.Xm,
                ".s3m" => SupportedAudioFormat.S3M,
                _ => SupportedAudioFormat.UnknownAudioFormat
            };
        }
    }
}