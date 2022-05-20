using System;
using System.Collections.Generic;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.AudioFormat
{
    [Serializable]
    public sealed class AudioFormatResultContent : IResult
    {
        [SerializeField] private Status status = BusinessLogic.Status.NotCalculated;
        [SerializeField] private List<string> notIdealAudioClips = new List<string>();

        public string Content()
        {
            return status switch
            {
                BusinessLogic.Status.Ok =>
                    "All audio clips have an appropriate lossless audio source.",
                BusinessLogic.Status.Warning =>
                    "You have audio clips which source is not in the recommended (lossless) file format.\n" +
                    "It is highly recommended to upgrade the listed audio clips below in order to improve the audio quality. " +
                    "Visit this best practice documentation for links to resources.",
                BusinessLogic.Status.Error =>
                    "You have at least one unsupported audio source format! \n" +
                    "Visit this best practice documentation for links to resources.",
                _ => "Something went wrong in the audio format initialization!"
            };
        }

        public void Status(Status packageStatus)
        {
            status = packageStatus;
        }

        public void AddNotIdealAudioClip(string audioClipPath)
        {
            notIdealAudioClips.Add(audioClipPath);
        }

        public IReadOnlyList<string> NotIdealAudioClips()
        {
            return notIdealAudioClips.AsReadOnly();
        }
    }
}