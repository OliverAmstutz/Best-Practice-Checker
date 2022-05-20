using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio
{
    public sealed class TextureUpdateDetector : AssetPostprocessor
    {
        public delegate void ImportTextureEvent();

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (deletedAssets.Length > 0 && ImportTexture != null) ImportTexture();
        }

        private void OnPostprocessTexture(Texture2D texture)
        {
            ImportTexture?.Invoke();
        }

        public static event ImportTextureEvent ImportTexture;
    }
}