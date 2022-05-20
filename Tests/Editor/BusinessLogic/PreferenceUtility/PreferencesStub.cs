using BestPracticeChecker.Editor.BusinessLogic.PreferenceUtility;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.PreferenceUtility
{
    public sealed class PreferencesStub : IPreferences
    {
        private readonly Ide _ide;
        public PreferencesStub(Ide ide)
        {
            _ide = ide;
        }

        public Ide UsedIde()
        {
            return _ide;
        }
    }
}