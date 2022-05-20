using BestPracticeChecker.Editor.BusinessLogic.PackageUtility;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.PackageUtility
{
    public sealed class VersionControlStatusStub : IVersionControlStatus
    {
        private readonly UnityVersionControl _unityVersionControl;

        public VersionControlStatusStub(UnityVersionControl unityVersionControl)
        {
            _unityVersionControl = unityVersionControl;
        }

        public UnityVersionControl Evaluate(string mode)
        {
            return _unityVersionControl;
        }
    }
}