using System.IO;
using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.AssetsProvider;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.TextureRatio
{
    public class TextureRatioBusinessLogicTest
    {
        [Test]
        public void TestEvaluationOk()
        {
            var bL = new TextureRatioBusinessLogic(
                "Assets/BestPracticeChecker/Tests/Editor/BusinessLogic/BestPractices/TextureRatio");
            bL.Evaluation();
            Assert.That(bL.GetStatus() == Status.Ok);
            Assert.IsFalse(bL.CanBeFixed());
        }

        [Test]
        public void TestEvaluationNotPowerOfTwoWarning()
        {
            const string fileName = "5x7Texture.jpg";
            const string sourcePath = "./Assets/BestPracticeChecker/Tests/TestAssets/";
            const string targetPath =
                "Assets/BestPracticeChecker/Tests/Editor/BusinessLogic/BestPractices/TextureRatio";
            File.Copy(sourcePath + fileName, "./" + targetPath + "/" + fileName);
            AssetDatabase.Refresh();
            var textures = new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider()
                .FindAllAssetsOfType<Texture>(targetPath);
            var textureImporter = AssetImportProvider.ImporterForTexture(textures[0]);
            textureImporter.npotScale = TextureImporterNPOTScale.None;
            textureImporter.SaveAndReimport();

            var bL = new TextureRatioBusinessLogic(targetPath);
            bL.Evaluation();

            AssetDatabase.Refresh();
            AssetDatabase.DeleteAsset(targetPath + "/" + fileName);
            AssetDatabase.Refresh();

            Assert.That(bL.GetStatus() == Status.Warning);
            Assert.IsFalse(bL.CanBeFixed());
        }

        [Test]
        public void TestEvaluationNotPowerOfTwoError()
        {
            const string fileName = "5x7Texture.jpg";
            const string sourcePath = "./Assets/BestPracticeChecker/Tests/TestAssets/";
            const string targetPath =
                "Assets/BestPracticeChecker/Tests/Editor/BusinessLogic/BestPractices/TextureRatio";
            File.Copy(sourcePath + fileName, "./" + targetPath + "/" + fileName);
            AssetDatabase.Refresh();
            var textures = new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider()
                .FindAllAssetsOfType<Texture>(targetPath);
            var textureImporter = AssetImportProvider.ImporterForTexture(textures[0]);
            textureImporter.npotScale = TextureImporterNPOTScale.None;
            textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
            textureImporter.SaveAndReimport();

            var bL = new TextureRatioBusinessLogic(targetPath);
            bL.Evaluation();

            AssetDatabase.Refresh();
            AssetDatabase.DeleteAsset(targetPath + "/" + fileName);
            AssetDatabase.Refresh();

            Assert.That(bL.GetStatus() == Status.Warning);
            Assert.IsTrue(bL.CanBeFixed());
        }

        [Test]
        public void TestEvaluationError()
        {
            const string fileName = "6x6 Texture.png";
            const string sourcePath = "./Assets/BestPracticeChecker/Tests/TestAssets/";
            const string targetPath =
                "Assets/BestPracticeChecker/Tests/Editor/BusinessLogic/BestPractices/TextureRatio";
            File.Copy(sourcePath + fileName, "./" + targetPath + "/" + fileName);
            AssetDatabase.Refresh();
            var textures = new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider()
                .FindAllAssetsOfType<Texture>(targetPath);
            var textureImporter = AssetImportProvider.ImporterForTexture(textures[0]);
            textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
            textureImporter.SaveAndReimport();
            var bL = new TextureRatioBusinessLogic(targetPath);

            bL.Evaluation();

            AssetDatabase.Refresh();
            AssetDatabase.DeleteAsset(targetPath + "/" + fileName);
            AssetDatabase.Refresh();

            Assert.That(bL.GetStatus() == Status.Warning);
            Assert.IsTrue(bL.CanBeFixed());
        }

        [Test]
        public void TestFix()
        {
            const string fileName = "6x6 Texture.png";
            const string sourcePath = "./Assets/BestPracticeChecker/Tests/TestAssets/";
            const string targetPath =
                "Assets/BestPracticeChecker/Tests/Editor/BusinessLogic/BestPractices/TextureRatio";
            File.Copy(sourcePath + fileName, "./" + targetPath + "/" + fileName);
            AssetDatabase.Refresh();
            var textures = new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider()
                .FindAllAssetsOfType<Texture>(targetPath);
            var textureImporter = AssetImportProvider.ImporterForTexture(textures[0]);
            textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
            textureImporter.SaveAndReimport();
            var bL = new TextureRatioBusinessLogic(targetPath);

            bL.Evaluation();
            bL.Fix();
            bL.Evaluation();

            AssetDatabase.Refresh();
            AssetDatabase.DeleteAsset(targetPath + "/" + fileName);
            AssetDatabase.Refresh();

            Assert.That(bL.GetStatus() == Status.Ok);
            Assert.IsFalse(bL.CanBeFixed());
        }

        [Test]
        public void TestResult()
        {
            var bL = new TextureRatioBusinessLogic();
            Assert.IsEmpty(bL.Result().FaultyTextures());
            Assert.That(bL.Result().Content().Contains("Something went wrong in the texture ratio initialization!"));
        }
    }
}