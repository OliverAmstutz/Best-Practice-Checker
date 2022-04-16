using System.IO;
using NUnit.Framework;
using UnityEngine;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.AssetsProvider
{
    public sealed class AssetsProviderTest
    {
        [Test]
        public void IntegrationTestFindAllAssetsOfTypeTexture()
        {
            var textures = new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider()
                .FindAllAssetsOfType<Texture>("Assets");
            Assert.That(textures[8].name.Equals("4x4Texture"));
            Assert.That(textures[9].name.Equals("5x7Texture"));
        }

        [Test]
        public void IntegrationTestFindAllAssetsOfTypeAudioClip()
        {
            var audios = new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider()
                .FindAllAssetsOfType<AudioClip>("Assets");
            Assert.That(audios[0].name.Equals("aiffAudio"));
            Assert.That(audios[1].name.Equals("itAudio"));
            Assert.That(audios[2].name.Equals("modAudio"));
            Assert.That(audios[3].name.Equals("mp3Audio"));
            Assert.That(audios[4].name.Equals("oggAudio"));
            Assert.That(audios[5].name.Equals("s3mAudio"));
            Assert.That(audios[6].name.Equals("WavAudio"));
            Assert.That(audios[7].name.Equals("xmAudio"));
        }

        [Test]
        public void TestFindAllAssetsOfTypeEmptyList()
        {
            Assert.IsEmpty(new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider()
                .FindAllAssetsOfType<TestFindAssetTest>("Assets"));
        }

        [Test]
        public void TestFindFileAssetFolderTrue()
        {
            Assert.True(
                new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider().FindFileAssetFolder(
                    "Scripts.meta"));
        }

        [Test]
        public void TestFindFolderFromStartPathTrue()
        {
            Assert.True(
                new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider().FindFolderFromStartPath(
                    "Scripts", "./Assets"));
        }

        [Test]
        public void TestFindFolderFromStartPathGit()
        {
            Assert.True(
                new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider().FindFolderFromStartPath(
                    ".git",
                    "./Assets"));
        }


        [Test]
        public void TestFindFolderFromStartPathNonexistent()
        {
            Assert.False(
                new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider().FindFolderFromStartPath(
                    "NonsenseFolderWhichDoesntExist", "./Assets"));
        }


        [Test]
        public void TestFindFolderFromStartPathNonsensePath()
        {
            Assert.False(
                new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider().FindFolderFromStartPath(
                    ".git",
                    "NonsensePathWhichDoesntExist"));
        }

        [Test]
        public void TestFileExtensionOfAsset()
        {
            var assetProvider = new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider();
            var audios = assetProvider.FindAllAssetsOfType<AudioClip>("Assets");
            Assert.That(assetProvider.FileExtensionOfAsset(audios[0]).Equals(".aiff"));
            Assert.That(assetProvider.FileExtensionOfAsset(audios[1]).Equals(".it"));
            Assert.That(assetProvider.FileExtensionOfAsset(audios[2]).Equals(".mod"));
            Assert.That(assetProvider.FileExtensionOfAsset(audios[3]).Equals(".mp3"));
            Assert.That(assetProvider.FileExtensionOfAsset(audios[4]).Equals(".ogg"));
            Assert.That(assetProvider.FileExtensionOfAsset(audios[5]).Equals(".s3m"));
            Assert.That(assetProvider.FileExtensionOfAsset(audios[6]).Equals(".wav"));
            Assert.That(assetProvider.FileExtensionOfAsset(audios[7]).Equals(".xm"));
        }

        [Test]
        public void TestFileExtensionOfAssetWithoutExtension()
        {
            Assert.That(new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider()
                .FileExtensionOfAsset(new TestFindAssetTest()).Equals(""));
        }

        [Test]
        public void TestFileExtensionOfAssetNull()
        {
            Assert.That(new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider()
                .FileExtensionOfAsset(new TestFindAssetTest()).Equals(""));
        }

        [Test]
        public void TestFindAllAssetsInFolderEmpty()
        {
            var assetProvider = new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider();
            Assert.IsEmpty(assetProvider.FindAllAssetsInFolder("Assets"));
        }

        [Test]
        public void TestFindAllAssetsInFolder()
        {
            var assetProvider = new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider();
            var assets = assetProvider.FindAllAssetsInFolder("Assets/BestPracticeChecker/Tests/TestAssets");
            Assert.That(assets.Count == 12);
        }


        [Test]
        public void TestFindAllAssetsInFolderInvalidFolder()
        {
            var assetProvider = new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider();
            try
            {
                assetProvider.FindAllAssetsInFolder("ThisIsANonExistingFolder");
                Assert.True(false);
            }
            catch (DirectoryNotFoundException e)
            {
                Assert.NotNull(e);
            }
        }

        private sealed class TestFindAssetTest : Object
        {
        }
    }
}