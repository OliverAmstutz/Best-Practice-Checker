using System.IO;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeAnalyser;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.NoAssetsInRoot;
using NUnit.Framework;
using UnityEditor;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.CodeAnalyser
{
    public sealed class CodeAnalyserDetectorTest
    {
        private int _counter;

        [Test]
        public void IntegrationTestOnPostprocessAllAssets()
        {
            CodeAnalyserDetector.ChangePlugin += ChangePluginCounter;
            const string fileName = "BestPracticeChecker.dll";
            const string sourcePath = "./Assets/BestPracticeChecker/Editor/UI/Resources/";
            const string targetPath = "Assets";
            File.Copy(sourcePath + fileName, "./" + targetPath + "/" + fileName);
            AssetDatabase.Refresh();
            AssetDatabase.DeleteAsset(targetPath + "/" + fileName);
            AssetDatabase.Refresh();
            AssetUpdateDetector.ImportAsset -= ChangePluginCounter;
            Assert.That(_counter == 2);
        }

        private void ChangePluginCounter()
        {
            _counter++;
        }
    }
}