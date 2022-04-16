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
            Assert.NotNull(AssetImportProvider.ImporterForTexture(textures[8]));
            Assert.NotNull(AssetImportProvider.ImporterForTexture(textures[9]));
            Assert.NotNull(AssetImportProvider.ImporterForTexture(textures[10]));
            Assert.Null(AssetImportProvider.ImporterForTexture(textures[11]));
        }
    }
}