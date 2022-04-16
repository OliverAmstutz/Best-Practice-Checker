using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Editor.UI.BestPractices
{
    public class ResultEditorFactory : IResultEditorFactory
    {
        private static readonly Vector2 MaxUndockedResultWindowSize = new Vector2(800f, 3000f);

        public ResultEditor InitialiseResultWindow<T>(BestPractice bP) where T : ResultEditor
        {
            var window = (ResultEditor) EditorWindow.GetWindow(typeof(T),
                true, bP.GetName() + " Result", true);
            window.Init();
            window.GetBestPracticeData(bP);
            window.maxSize = MaxUndockedResultWindowSize;
            window.ShowAuxWindow();
            return window;
        }
    }
}