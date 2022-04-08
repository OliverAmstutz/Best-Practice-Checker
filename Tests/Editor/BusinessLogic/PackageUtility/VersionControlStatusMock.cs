using BestPracticeChecker.Editor.BusinessLogic.PackageUtility;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.PackageUtility
{
    public class VersionControlStatusMock : IVersionControlStatus
    {
        private readonly UnityVersionControl _unityVersionControl;

        public VersionControlStatusMock(UnityVersionControl unityVersionControl)
        {
            _unityVersionControl = unityVersionControl;
        }

        public UnityVersionControl Evaluate(string mode)
        {
            return _unityVersionControl;
        }
    }
}