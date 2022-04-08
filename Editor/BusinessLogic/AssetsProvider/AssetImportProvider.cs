using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.AssetsProvider
{
    public static class AssetImportProvider
    {
        public static TextureImporter ImporterForTexture(Texture texture)
        {
            var path = AssetDatabase.GetAssetPath(texture);
            try
            {
                return (TextureImporter) AssetImporter.GetAtPath(path);
            }
            catch
            {
                return null;
            }
        }
    }
}