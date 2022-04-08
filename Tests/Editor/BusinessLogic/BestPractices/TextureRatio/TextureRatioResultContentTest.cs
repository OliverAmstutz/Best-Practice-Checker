using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio;
using NUnit.Framework;
using UnityEngine;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.TextureRatio
{
    public sealed class TextureRatioResultContentTest
    {
        [Test]
        public void TestContentStatusOk()
        {
            var content = new TextureRatioResultContent();
            content.Status(Status.Ok);
            Assert.That(content.Content().Contains("All your texture's width and height are based on two"));
        }

        [Test]
        public void TestContentStatusWarning()
        {
            var content = new TextureRatioResultContent();
            content.Status(Status.Warning);
            Assert.That(content.Content()
                .Contains(
                    "You have textures which are not based on two - no texture compression is possible for these textures"));
        }

        [Test]
        public void TestContentStatusInvalid()
        {
            var content = new TextureRatioResultContent();
            content.Status(Status.NotCalculated);
            Assert.That(content.Content().Contains("Something went wrong in the texture ratio initialization!"));
        }

        [Test]
        public void TestFaultyTexturesEmptyList()
        {
            var content = new TextureRatioResultContent();
            Assert.IsEmpty(content.FaultyTextures());
        }


        [Test]
        public void TestAddFaultyTextures()
        {
            var content = new TextureRatioResultContent();
            var textures =
                new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider()
                    .FindAllAssetsOfType<Texture>(
                        "Assets");
            foreach (var texture in textures) content.AddFaultyTexture(new FaultyTexture(texture, true));
            foreach (var tex in textures) Assert.True(content.FaultyTextures().Contains(new FaultyTexture(tex, true)));
        }
    }
}