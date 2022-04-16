using System;
using System.IO;
using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.AudioFormat;
using NUnit.Framework;
using UnityEditor;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.AudioFormat
{
    public class AudioFormatBusinessLogicTest
    {
        private static string[] OkfileNames =
        {
            "aiffAudio.aiff",
            "itAudio.it",
            "modAudio.mod",
            "s3mAudio.s3m",
            "WavAudio.wav",
            "xmAudio.xm"
        };

        private static string[] WarningfileNames =
        {
            "mp3Audio.mp3",
            "oggAudio.ogg"
        };

        [Test]
        public void TestEvaluationOkNoAssets()
        {
            var bL = new AudioFormatBusinessLogic(
                "Assets/BestPracticeChecker/Tests/Editor/BusinessLogic/BestPractices/AudioFormat");
            bL.Evaluation();
            Assert.That(bL.GetStatus() == Status.Ok);
        }

        [Test]
        public void TestEvaluationOk([ValueSource("OkfileNames")] string fileName)
        {
            const string sourcePath = "./Assets/BestPracticeChecker/Tests/TestAssets/";
            const string targetPath =
                "Assets/BestPracticeChecker/Tests/Editor/BusinessLogic/BestPractices/AudioFormat";
            File.Copy(sourcePath + fileName, "./" + targetPath + "/" + fileName);
            AssetDatabase.Refresh();
            var bL = new AudioFormatBusinessLogic(targetPath);
            bL.Evaluation();
            AssetDatabase.DeleteAsset(targetPath + "/" + fileName);
            AssetDatabase.Refresh();
            Assert.That(bL.GetStatus() == Status.Ok);
        }

        [Test]
        public void TestEvaluationWarning([ValueSource("WarningfileNames")] string fileName)
        {
            const string sourcePath = "./Assets/BestPracticeChecker/Tests/TestAssets/";
            const string targetPath =
                "Assets/BestPracticeChecker/Tests/Editor/BusinessLogic/BestPractices/AudioFormat";
            File.Copy(sourcePath + fileName, "./" + targetPath + "/" + fileName);
            AssetDatabase.Refresh();
            var bL = new AudioFormatBusinessLogic(targetPath);
            bL.Evaluation();
            AssetDatabase.DeleteAsset(targetPath + "/" + fileName);
            AssetDatabase.Refresh();
            Assert.That(bL.GetStatus() == Status.Warning);
        }

        [Test]
        public void TestCanBeFixed()
        {
            Assert.IsFalse(new AudioFormatBusinessLogic().CanBeFixed());
        }

        [Test]
        public void TestResult()
        {
            var result = new AudioFormatBusinessLogic().Result();
            Assert.That(result.Content().Contains("Something went wrong in the audio format initialization!"));
            Assert.IsEmpty(result.NotIdealAudioClips());
        }

        [Test]
        public void TestFix()
        {
            Assert.Throws<InvalidOperationException>(new AudioFormatBusinessLogic().Fix);
        }
    }
}