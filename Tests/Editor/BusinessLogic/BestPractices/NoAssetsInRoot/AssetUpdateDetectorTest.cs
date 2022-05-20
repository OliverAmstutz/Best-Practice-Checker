using System.IO;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.NoAssetsInRoot;
using NUnit.Framework;
using UnityEditor;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.NoAssetsInRoot
{
    public sealed class AssetUpdateDetectorTest
    {
        private int _counter;

        [Test]
        public void IntegrationTestOnPostprocessAllAssets()
        {
            AssetUpdateDetector.ImportAsset += ImportTextureCounter;
            const string fileName = "5x7Texture.jpg";
            const string sourcePath = "./Assets/BestPracticeChecker/Tests/TestAssets/";
            const string targetPath = "Assets";
            File.Copy(sourcePath + fileName, "./" + targetPath + "/" + fileName);
            AssetDatabase.Refresh();
            AssetDatabase.DeleteAsset(targetPath + "/" + fileName);
            AssetDatabase.Refresh();
            AssetUpdateDetector.ImportAsset -= ImportTextureCounter;
            Assert.That(_counter == 2);
        }

        private void ImportTextureCounter()
        {
            _counter++;
        }
    }
}