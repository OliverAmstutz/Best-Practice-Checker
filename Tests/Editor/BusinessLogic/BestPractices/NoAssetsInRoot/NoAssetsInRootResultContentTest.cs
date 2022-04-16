using System.Linq;
using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.NoAssetsInRoot;
using NUnit.Framework;
using UnityEditor;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.NoAssetsInRoot
{
    public sealed class NoAssetsInRootResultContentTest
    {
        [Test]
        public void TestContentStatusOk()
        {
            var content = new NoAssetsInRootResultContent();
            content.Status(Status.Ok);
            Assert.That(content.Content().Contains("Excellent, your root hierarchy is clean and organised."));
        }

        [Test]
        public void TestContentStatusWarning()
        {
            var content = new NoAssetsInRootResultContent();
            content.Status(Status.Warning);
            Assert.That(content.Content().Contains("You have assets in your root hierarchy!"));
        }

        [Test]
        public void TestContentStatusInvalid()
        {
            var content = new NoAssetsInRootResultContent();
            content.Status(Status.NotCalculated);
            Assert.That(content.Content().Contains("Something went wrong in the no assets in root initialization"));
        }

        [Test]
        public void TestAddMisplacedAssetsEmptyList()
        {
            var content = new NoAssetsInRootResultContent();
            Assert.IsEmpty(content.MisplacedAssetsPaths());
        }


        [Test]
        public void TestAddMisplacedAssets()
        {
            var content = new NoAssetsInRootResultContent();
            var assetsProvider = new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider();
            var assets = assetsProvider.FindAllAssetsInFolder("Assets/BestPracticeChecker/Tests/TestAssets");
            assets.ToList().ForEach(asset => content.AddMisplacedAssetsPath(AssetDatabase.GetAssetPath(asset)));

            foreach (var asset in assets)
                Assert.True(content.MisplacedAssetsPaths().Contains(AssetDatabase.GetAssetPath(asset)));
        }
    }
}