using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Editor.UI.BestPractices
{
    public abstract class ResultEditor : EditorWindow
    {
        private const string FixButtonText = "Fix";
        private const string CloseButtonText = "Close";
        private Vector2 _scrollPosition;
        protected bool HasFix;

        private void OnDisable()
        {
            CleanUp();
        }

        private void OnGUI()
        {
            GUI.skin.label.wordWrap = true;
            GUI.skin.button.wordWrap = true;
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                using (new GUILayout.VerticalScope())
                {
                    using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
                    {
                        using (new EditorGUI.DisabledScope(!HasFix))
                        {
                            FixButton();
                        }

                        CloseButton();
                    }

                    DisplayResult();
                }
            }

            EditorGUILayout.EndScrollView();
        }

        private void CloseButton()
        {
            if (GUILayout.Button(new GUIContent(CloseButtonText, "Closes this result editor window"))) Close();
        }

        private void FixButton()
        {
            if (GUILayout.Button(new GUIContent(FixButtonText, "Fixes this best practice"))) Fix();
        }

        public virtual void Init()
        {
        }

        protected virtual void CleanUp()
        {
        }


        protected abstract void DisplayResult();


        public abstract void GetBestPracticeData(IBestPractice bP);

        protected abstract void Fix();
    }
}