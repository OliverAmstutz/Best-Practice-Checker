using System;
using BestPracticeChecker.Editor.BusinessLogic.PackageUtility;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeCoverage
{
    public sealed class CodeCoverageBusinessLogic : IBusinessLogic<CodeCoverageResultContent>
    {
        private const string CodeCoveragePackageName = "com.unity.testtools.codecoverage";
        private readonly IPackageUtility _pu;
        private bool _canBeFixed;
        private CodeCoverageResultContent _result;
        private Status _status;

        public CodeCoverageBusinessLogic() : this(new PackageUtility.PackageUtility())
        {
        }

        public CodeCoverageBusinessLogic(IPackageUtility packageUtility)
        {
            _pu = packageUtility;
        }

        public void Evaluation()
        {
            _result = new CodeCoverageResultContent();
            if (_pu == null) throw new NullReferenceException("PackageUtility not instantiated");

            if (_pu.PackageExists(CodeCoveragePackageName))
            {
                if (_pu.IsUpToDate(CodeCoveragePackageName))
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

        public Status GetStatus()
        {
            return _status;
        }

        public CodeCoverageResultContent Result()
        {
            return _result;
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
            _pu.InstallLatestPackage(CodeCoveragePackageName);
        }
    }
}