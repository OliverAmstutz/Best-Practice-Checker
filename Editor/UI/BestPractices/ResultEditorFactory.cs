using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Editor.UI.BestPractices
{
    public sealed class ResultEditorFactory : IResultEditorFactory
    {
        private static readonly Vector2 MaxUndockedResultWindowSize = new Vector2(800f, 3000f);
        private static readonly Vector2 MinUndockedResultWindowSize = new Vector2(200f, 200f);

        public ResultEditor InitialiseResultWindow<T>(BestPractice bP) where T : ResultEditor
        {
            var window = (ResultEditor) EditorWindow.GetWindow(typeof(T), true, bP.GetName() + " Result", true);
            window.Init();
            window.GetBestPracticeData(bP);
            window.maxSize = MaxUndockedResultWindowSize;
            window.minSize = MinUndockedResultWindowSize;
            return window;
        }
    }
}