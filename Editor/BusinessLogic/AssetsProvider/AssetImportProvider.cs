using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.AssetsProvider
{
    public static class AssetImportProvider
    {
        public static TextureImporter ImporterForTexture(Texture texture)
        {
            var path = AssetDatabase.GetAssetPath(texture);
            return (TextureImporter) AssetImporter.GetAtPath(path);
        }
    }
}