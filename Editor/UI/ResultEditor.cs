using BestPracticeChecker.Editor.BusinessLogic;
using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Editor.UI
{
    public abstract class ResultEditor : EditorWindow
    {
        private const string FixButtonText = "Fix";
        private const string CloseButtonText = "Close";
        private Vector2 _scrollPosition;
        protected bool HasFix;

        private void OnGUI()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                using (new GUILayout.VerticalScope())
                {
                    using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
                    {
                        using (new EditorGUI.DisabledScope(!HasFix))
                        {
                            if (GUILayout.Button(FixButtonText))
                                Fix();
                        }

                        if (GUILayout.Button(CloseButtonText))
                            Close();
                    }

                    DisplayResult();
                }
            }

            EditorGUILayout.EndScrollView();
        }


        protected abstract void DisplayResult();


        public abstract void GetBestPracticeData(IBestPractice bP);

        protected abstract void Fix();
    }
}