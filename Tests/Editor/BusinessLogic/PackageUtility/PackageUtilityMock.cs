using BestPracticeChecker.Editor.BusinessLogic.PackageUtility;
using BestPracticeChecker.Editor.BusinessLogic.Persistor;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.PackageUtility
{
    public class PackageUtilityMock : IPackageUtility
    {
        private readonly bool _isUpToDate;
        private readonly bool _packageExists;
        private readonly PackageStatus _statusOfPackage;

        public PackageUtilityMock(bool packageExists, bool isUpToDate, PackageStatus statusOfPackage)
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

        public void RemovePackage(string packageName)
        {
            //no implementation.
        }
    }
}