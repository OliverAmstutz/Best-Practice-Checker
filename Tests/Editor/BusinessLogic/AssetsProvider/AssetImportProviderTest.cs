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
            foreach (var texture in textures) Assert.NotNull(AssetImportProvider.ImporterForTexture(texture));
        }
    }
}