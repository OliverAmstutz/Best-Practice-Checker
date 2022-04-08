using System.IO;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio;
using NUnit.Framework;
using UnityEditor;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.TextureRatio
{
    public class TextureUpdateDetectorTest
    {
        private int _counter = 0;
        [Test]
        public void IntegrationTestOnPostprocessAllAssets()
        {
            TextureUpdateDetector.ImportTexture += ImportTextureCounter;
            const string fileName = "5x7Texture.jpg";
            const string sourcePath = "./Assets/BestPracticeChecker/Tests/TestAssets/";
            const string targetPath = "Assets/BestPracticeChecker/Tests/Editor/BusinessLogic/BestPractices/TextureRatio";
            File.Copy(sourcePath+fileName, "./"+targetPath+"/"+fileName);
            AssetDatabase.Refresh();
            AssetDatabase.Refresh();
            AssetDatabase.DeleteAsset(targetPath + "/" + fileName);
            AssetDatabase.Refresh();
            TextureUpdateDetector.ImportTexture -= ImportTextureCounter;
            Assert.That(_counter == 2);
        }

        private void ImportTextureCounter()
        {
            _counter++;
        }
    }
}