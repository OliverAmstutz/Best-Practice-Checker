using Unity.CodeEditor;

namespace BestPracticeChecker.Editor.BusinessLogic.PreferenceUtility
{
    public sealed class Preferences : IPreferences
    {        
        private const string Rider = "Packages.Rider.Editor.RiderScriptEditor";
        private const string VisualStudio = "Microsoft.Unity.VisualStudio.Editor.VisualStudioEditor";
        
        public Ide UsedIde()
        {
            var editor = CodeEditor.CurrentEditor;
            return editor.ToString() switch
            {
                Rider => Ide.Rider,
                VisualStudio => Ide.VisualStudio,
                _ => Ide.Unknown
            };
        }
    }
}