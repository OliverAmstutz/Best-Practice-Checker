using System.Collections.Generic;
using System.Linq;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.NoAssetsInRoot;
using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Editor.UI.BestPractices.NoAssetsInRoot
{
    public sealed class NoAssetsInRootResult : ResultEditor
    {
        private IBestPractice _bestPractice;
        private NoAssetsInRootResultContent _result;

        protected override void DisplayResult()
        {
            if (_result == null && _bestPractice != null)
                _result = (NoAssetsInRootResultContent) _bestPractice.GetResult();


            if (_result != null)
            {
                GUILayout.Label(_result.Content());
                DisplayMisplacedAssets(_result.MisplacedAssetsPaths());
            }
            else
            {
                Debug.LogWarning("_result is null, _bestPractice not yet initialized!");
            }
        }

        public override void GetBestPracticeData(IBestPractice bP)
        {
            _bestPractice = bP;
            HasFix = bP.HasFix();
        }

        protected override void Fix()
        {
            if (_bestPractice != null)
                _bestPractice.Fix();
            else
                Debug.LogWarning("_bestPractice is null!");
        }

        private static void DisplayMisplacedAssets(List<string> misplacedObjectPaths)
        {
            if (misplacedObjectPaths.Count == 0) return;
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                foreach (var obj in misplacedObjectPaths.Select(AssetDatabase.LoadMainAssetAtPath)
                             .Where(obj => obj != null))
                    using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
                    {
                        if (GUILayout.Button("View " + obj.name)) Highlighter.HighlightObject(obj);
                        GUILayout.Label("Type: " + obj.GetType());
                    }
            }
        }
    }
}