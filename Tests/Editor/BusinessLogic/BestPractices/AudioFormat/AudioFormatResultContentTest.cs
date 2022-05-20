using System.Linq;
using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.AudioFormat;
using NUnit.Framework;
using UnityEngine;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.AudioFormat
{
    public sealed class AudioFormatResultContentTest
    {
        [Test]
        public void TestContentStatusOk()
        {
            var content = new AudioFormatResultContent();
            content.Status(Status.Ok);
            Assert.That(content.Content().Contains("All audio clips have an appropriate lossless audio source."));
        }

        [Test]
        public void TestContentStatusWarning()
        {
            var content = new AudioFormatResultContent();
            content.Status(Status.Warning);
            Assert.That(content.Content().Contains("You have audio clips which source is not in the recommended (lossless) file format."));
        }

        [Test]
        public void TestContentStatusError()
        {
            var content = new AudioFormatResultContent();
            content.Status(Status.Error);
            Assert.That(content.Content().Contains("You have at least one unsupported audio source format!"));
        }

        [Test]
        public void TestContentStatusInvalid()
        {
            var content = new AudioFormatResultContent();
            content.Status(Status.NotCalculated);
            Assert.That(content.Content().Contains("Something went wrong in the audio format initialization"));
        }

        [Test]
        public void TestAddNotIdealAudioClip()
        {
            var content = new AudioFormatResultContent();
            var audios = new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider().FindAllAssetsOfType<AudioClip>("Assets");
            foreach (var audioClip in audios)
            {
                content.AddNotIdealAudioClip(audioClip.ToString());
                Assert.True(content.NotIdealAudioClips().ToList().Contains(audioClip.ToString()));
            }
        }
    }
}