using BestPracticeChecker.Editor.BusinessLogic.PreferenceUtility;
using NUnit.Framework;
using Unity.CodeEditor;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.PreferenceUtility
{
    public sealed class PreferencesTest
    {
        private const string Rider = "Packages.Rider.Editor.RiderScriptEditor";
        private const string VisualStudio = "Microsoft.Unity.VisualStudio.Editor.VisualStudioEditor";
        
        [Test]
        public void IntegrationTestIde()
        {
            IPreferences preferences = new Preferences();
            var expectedIde = AssignIde(CodeEditor.CurrentEditor.ToString());
            Assert.That(preferences.UsedIde().Equals(expectedIde));
        }

        private Ide AssignIde(string ideString)
        {
            return ideString switch
            {
                Rider => Ide.Rider,
                VisualStudio => Ide.VisualStudio,
                _ => Ide.Unknown
            };
        }
    }
}