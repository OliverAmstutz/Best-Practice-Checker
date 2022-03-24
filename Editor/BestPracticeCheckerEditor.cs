using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.UI;
using UnityEditor;

namespace BestPracticeChecker.Editor
{
    public class BestPracticeCheckerEditor : EditorWindow
    {
        public delegate void ExecuteBeforeShutdown();

        private const string ToolName = "Best Practice Checker";
        private CompoundControls _cc;

        private void OnEnable()
        {
            _cc = CreateInstance<CompoundControls>();
            BestPractice.UpdateUI += Repaint;
            Undo.undoRedoPerformed += Repaint;
        }

        private void OnDisable()
        {
            BestPractice.UpdateUI -= Repaint;
            Undo.undoRedoPerformed -= Repaint;
            if (BeforeShutdown != null) BeforeShutdown();
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
            GetWindow<BestPracticeCheckerEditor>(ToolName);
        }
    }
}