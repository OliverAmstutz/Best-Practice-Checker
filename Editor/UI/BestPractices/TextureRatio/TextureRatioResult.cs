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

        private static void DisplayFaultyTextures(List<FaultyTexture> faultyTextures)
        {
            if (faultyTextures.Count == 0) return;
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                foreach (var ft in faultyTextures)
                    using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
                    {
                        if (ft.Texture() != null && GUILayout.Button("View " + ft.Texture().name))
                            Highlighter.HighlightObject(ft.Texture());

                        GUILayout.Label("texture dim.: " + ft.Texture().width + " x " + ft.Texture().height);
                        GUILayout.Label(ft.IsUnCompressed() ? "Compression disabled" : "Compression enabled");
                    }
            }
        }
    }
}