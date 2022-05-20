using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio;
using NUnit.Framework;
using UnityEditor;
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
            Assert.That(content.Content().Contains("Excellent, all your texture's width and height are based on two with compression enabled."));
        }

        [Test]
        public void TestContentStatusWarning()
        {
            var content = new TextureRatioResultContent();
            content.Status(Status.Warning);
            Assert.That(content.Content().Contains("You have either textures which dimension are not based on two, or textures with compression turned off."));
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
            var textures = new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider().FindAllAssetsOfType<Texture>("Assets");
            foreach (var texture in textures) content.AddFaultyTexture(new FaultyTexture(AssetDatabase.GetAssetPath(texture), true));
            foreach (var tex in textures) Assert.True(content.FaultyTextures().Contains(new FaultyTexture(AssetDatabase.GetAssetPath(tex), true)));
        }
    }
}