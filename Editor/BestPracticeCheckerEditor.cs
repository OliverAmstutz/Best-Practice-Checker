using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using BestPracticeChecker.Editor.UI;
using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Editor
{
    public sealed class BestPracticeCheckerEditor : EditorWindow
    {
        public delegate void ExecuteBeforeShutdown();

        private const string ToolName = "Best Practice Checker";
        private static readonly Vector2 MaxUndockedToolWindowSize = new Vector2(400, 230);
        private CompoundControls _cc;

        private void OnEnable()
        {
            _cc = CompoundControlsFactory.Create();
            BestPractice.UpdateUI += Repaint;
            Undo.undoRedoPerformed += Repaint;
        }

        private void OnDisable()
        {
            BestPractice.UpdateUI -= Repaint;
            Undo.undoRedoPerformed -= Repaint;
            BeforeShutdown?.Invoke();
        }

        private void OnGUI()
        {
            _cc.SelectionButtons();
            _cc.BestPractices();
        }

        public static event ExecuteBeforeShutdown BeforeShutdown;

        [MenuItem("Tools/" + ToolName)]
        private static void OpenBestPracticeCheckerEditor()
        {
            var window = GetWindow<BestPracticeCheckerEditor>(ToolName);
            window.maxSize = MaxUndockedToolWindowSize;
        }
    }
}