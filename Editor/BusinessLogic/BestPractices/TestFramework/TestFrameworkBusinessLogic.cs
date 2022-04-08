using System;
using BestPracticeChecker.Editor.BusinessLogic.PackageUtility;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework
{
    public class TestFrameworkBusinessLogic : ITestFrameworkBusinessLogic
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
                {
                    _canBeFixed = false;
                    _status = Status.Ok;
                    _result.Status(Status.Ok);
                }
                else
                {
                    _canBeFixed = true;
                    _status = Status.Warning;
                    _result.Status(Status.Warning);
                }
            }
            else
            {
                _canBeFixed = true;
                _status = Status.Error;
                _result.Status(Status.Error);
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

        private void InstallLatestTestFramework()
        {
            _pu.InstallLatestPackage(TestFrameWorkPackageName);
        }
    }
}