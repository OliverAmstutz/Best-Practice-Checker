using UnityEditor;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.NoAssetsInRoot
{
    public sealed class AssetUpdateDetector : AssetPostprocessor
    {
        public delegate void ImportAssetEvent();

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
            string[] movedAssets, string[] movedFromAssetPaths)
        {
            ImportAsset?.Invoke();
        }

        public static event ImportAssetEvent ImportAsset;
    }
}