using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeAnalyser
{
    public sealed class CodeAnalyserDetector : AssetPostprocessor
    {
        public delegate void ChangePluginEvent();

        private const string DllExtension = "dll";

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (ChangePlugin == null) return;
            if (IsDll(deletedAssets) || IsDll(importedAssets)) ChangePlugin();
        }

        private static bool IsDll(IReadOnlyCollection<string> assets)
        {
            return assets.Count > 0 && assets.Any(asset => Path.GetExtension(asset).Contains(DllExtension));
        }


        public static event ChangePluginEvent ChangePlugin;
    }
}