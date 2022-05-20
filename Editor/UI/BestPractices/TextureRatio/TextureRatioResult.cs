using System.Collections.Generic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio;
using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Editor.UI.BestPractices.TextureRatio
{
    public sealed class TextureRatioResult : ResultEditor
    {
        private IBestPractice _bestPractice;
        private TextureRatioResultContent _result;

        public override void Init()
        {
            base.Init();
            BestPractice.UpdateUI += AfterFix;
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            BestPractice.UpdateUI -= AfterFix;
        }

        private void AfterFix()
        {
            _result = (TextureRatioResultContent) _bestPractice.GetResult();
            HasFix = _bestPractice.HasFix();
            Repaint();
        }

        protected override void DisplayResult()
        {
            if (_result == null && _bestPractice != null)
                _result = (TextureRatioResultContent) _bestPractice.GetResult();

            if (_result != null)
            {
                GUILayout.Label(_result.Content());
                DisplayFaultyTextures(_result.FaultyTextures());
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

        private void DisplayFaultyTextures(List<FaultyTexture> faultyTextures)
        {
            if (faultyTextures.Count == 0) return;
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                foreach (var ft in faultyTextures) Display(ft);
            }
        }

        private void Display(FaultyTexture ft)
        {
            if (ft.IsDirty()) return;
            var texture = AssetDatabase.LoadAssetAtPath<Texture>(ft.TexturePath());
            if (texture == null) return;
            using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
            {
                DisplayTexture(ft, texture);
            }
        }

        private static void DisplayTexture(FaultyTexture ft, Texture texture)
        {
            if (GUILayout.Button(new GUIContent("View " + texture.name, "Shows asset in project hierarchy"))) Highlighter.HighlightObject(texture);

            if (ft.IsUnCompressed())
            {
                if (GUILayout.Button(new GUIContent("Fix", "Fixes this texture")))
                {
                    TextureRatioBusinessLogic.FixTextureCompressionSetting(ft);
                    ft.SetDirty();
                }

                GUILayout.Label("Compression disabled");
            }

            if (TextureRatioBusinessLogic.IsNotPowerOfTwo(texture)) GUILayout.Label("texture dim.: " + texture.width + " x " + texture.height);
        }
    }
}