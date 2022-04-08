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
            Assert.That(textures[0].name.Equals("4x4Texture"));
            Assert.That(textures[1].name.Equals("5x7Texture"));
        }

        [Test]
        public void IntegrationTestFindAllAssetsOfTypeAudioClip()
        {
            var audios = new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider()
                .FindAllAssetsOfType<AudioClip>("Assets");
            Assert.That(audios[0].name.Equals("WavAudio"));
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

        private sealed class TestFindAssetTest : Object
        {
        }
    }
}