using BestPracticeChecker.Editor.BusinessLogic.AssetsProvider;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.AssetsProvider
{
    public sealed class AssetImportProviderTest
    {
        [Test]
        public void TestImporterForTexture()
        {
            var textures = new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider().FindAllAssetsOfType<Texture>("Assets");
            Assert.NotNull(AssetImportProvider.ImporterForTexture(textures[8]));
            Assert.NotNull(AssetImportProvider.ImporterForTexture(textures[9]));
            Assert.NotNull(AssetImportProvider.ImporterForTexture(textures[10]));
            Assert.Null(AssetImportProvider.ImporterForTexture(textures[11]));
        }

        [Test]
        public void TestImporterForPlugin()
        {
            var plugin = (DefaultAsset) new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider().FindAssetOfNameAndFileExtension("BestPracticeChecker", "dll");
            Assert.NotNull(AssetImportProvider.ImporterForPlugin(plugin));
        }

        [Test]
        public void TestImporterForNullPlugin()
        {
            Assert.Null(AssetImportProvider.ImporterForPlugin(null));
        }
    }
}