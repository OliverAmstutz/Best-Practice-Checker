using System.IO;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.AudioFormat;
using NUnit.Framework;
using UnityEditor;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.AudioFormat
{
    public sealed class AudioUpdateDetectorTest
    {
        private int _counter;

        [Test]
        public void IntegrationTestOnPostprocessAllAssets()
        {
            AudioUpdateDetector.ImportAudio += ImportAudioCounter;
            const string fileName = "WavAudio.wav";
            const string sourcePath = "./Assets/BestPracticeChecker/Tests/TestAssets/";
            const string targetPath = "Assets/BestPracticeChecker/Tests/Editor/BusinessLogic/BestPractices/AudioFormat";
            File.Copy(sourcePath + fileName, "./" + targetPath + "/" + fileName);
            AssetDatabase.Refresh();
            AssetDatabase.Refresh();
            AssetDatabase.DeleteAsset(targetPath + "/" + fileName);
            AssetDatabase.Refresh();
            AudioUpdateDetector.ImportAudio -= ImportAudioCounter;
            Assert.That(_counter == 2); //Copy and Delete count each as one
        }

        private void ImportAudioCounter()
        {
            _counter++;
        }
    }
}