using System;
using BestPracticeChecker.Editor.BusinessLogic.PackageUtility;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework
{
    public sealed class TestFrameworkBusinessLogic : IBusinessLogic<TestFrameworkResultContent>
    {
        private const string TestFrameWorkPackageName = "com.unity.test-framework";
        private readonly IPackageUtility _pu;
        private bool _canBeFixed;
        private TestFrameworkResultContent _result;
        private Status _status = Status.NotCalculated;

        public TestFrameworkBusinessLogic() : this(new PackageUtility.PackageUtility())
        {
        }


        public TestFrameworkBusinessLogic(IPackageUtility pu)
        {
            _pu = pu;
        }


        public void Evaluation()
        {
            _result = new TestFrameworkResultContent();
            if (_pu == null) throw new NullReferenceException("PackageUtility not instantiated");

            if (_pu.PackageExists(TestFrameWorkPackageName))
            {
                if (_pu.IsUpToDate(TestFrameWorkPackageName))
                    SetStatus(false, Status.Ok, PackageStatus.UpToDate);
                else
                    SetStatus(true, Status.Warning, PackageStatus.Outdated);
            }
            else
            {
                SetStatus(true, Status.Error, PackageStatus.NotInstalled);
            }
        }

        public bool CanBeFixed()
        {
            return _canBeFixed;
        }

        public TestFrameworkResultContent Result()
        {
            return _result;
        }

        public Status GetStatus()
        {
            return _status;
        }

        public void Fix()
        {
            InstallLatestTestFramework();
        }

        private void SetStatus(bool canBeFixed, Status status, PackageStatus packageStatus)
        {
            _canBeFixed = canBeFixed;
            _status = status;
            _result.Status(packageStatus);
        }

        private void InstallLatestTestFramework()
        {
            _pu.InstallLatestPackage(TestFrameWorkPackageName);
        }
    }
}