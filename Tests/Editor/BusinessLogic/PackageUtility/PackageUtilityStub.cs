using BestPracticeChecker.Editor.BusinessLogic.PackageUtility;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.PackageUtility
{
    public sealed class PackageUtilityStub : IPackageUtility
    {
        private readonly bool _isUpToDate;
        private readonly bool _packageExists;
        private readonly PackageStatus _statusOfPackage;

        public PackageUtilityStub(bool packageExists, bool isUpToDate, PackageStatus statusOfPackage)
        {
            _packageExists = packageExists;
            _isUpToDate = isUpToDate;
            _statusOfPackage = statusOfPackage;
        }

        public PackageStatus StatusOfPackage(string packageName)
        {
            return _statusOfPackage;
        }

        public bool PackageExists(string packageName)
        {
            return _packageExists;
        }

        public bool IsUpToDate(string packageName)
        {
            return _isUpToDate;
        }

        public void InstallLatestPackage(string packageName)
        {
            //no implementation.
        }
    }
}