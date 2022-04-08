using BestPracticeChecker.Editor.BusinessLogic.AssetsProvider;
using NUnit.Framework;
using UnityEngine;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.AssetsProvider
{
    public class AssetImportProviderTest
    {
        [Test]
        public void TestImporterForTexture()
        {
            var textures = new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider()
                .FindAllAssetsOfType<Texture>("Assets");
            Assert.That(textures.Count == 4);
            Assert.NotNull(AssetImportProvider.ImporterForTexture(textures[0]));
            Assert.NotNull(AssetImportProvider.ImporterForTexture(textures[1]));
            Assert.NotNull(AssetImportProvider.ImporterForTexture(textures[2]));
            Assert.Null(AssetImportProvider.ImporterForTexture(textures[3]));
        }
    }
}