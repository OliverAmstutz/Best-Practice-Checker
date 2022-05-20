using System;
using System.IO;
using BestPracticeChecker.Editor.BusinessLogic.AssetsProvider;
using BestPracticeChecker.Editor.BusinessLogic.PackageUtility;
using UnityEditor;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.SourceControl
{
    public sealed class SourceControlBusinessLogic : IBusinessLogic<SourceControlResultContent>
    {
        private const string UnityVersionControlPackage = "com.unity.collab-proxy";
        private const string GitFolderName = ".git";
        private readonly IAssetsProvider _assetsProvider;
        private readonly IPackageUtility _pu;
        private readonly IVersionControlStatus _versionControlStatus;
        private bool _canBeFixed;
        private SourceControlResultContent _result;
        private Status _status;

        public SourceControlBusinessLogic() : this(new AssetsProvider.AssetsProvider(), new PackageUtility.PackageUtility(), new VersionControlStatus())
        {
        }

        public SourceControlBusinessLogic(IAssetsProvider assetsProvider, IPackageUtility packageUtility, IVersionControlStatus versionControlStatus)
        {
            _pu = packageUtility;
            _assetsProvider = assetsProvider;
            _versionControlStatus = versionControlStatus;
        }

        public void Evaluation()
        {
            var gitFolderExists = _assetsProvider.FindFolderFromStartPath(GitFolderName, Directory.GetCurrentDirectory());
            var versionControlSetting = _versionControlStatus.Evaluate(VersionControlSettings.mode);
            _canBeFixed = false;
            _result = new SourceControlResultContent();
            if (gitFolderExists)
                UseGit(versionControlSetting);
            else
                UseUnityVersionControl(versionControlSetting);
        }

        public bool CanBeFixed()
        {
            return _canBeFixed;
        }

        public Status GetStatus()
        {
            return _status;
        }

        public SourceControlResultContent Result()
        {
            return _result;
        }

        public void Fix()
        {
            if (_canBeFixed)
                InstallLatestVersionControlPackage();
            else
                throw new InvalidOperationException("Fix should not be called if fix doesnt exist!");
        }

        private void InstallLatestVersionControlPackage()
        {
            _pu.InstallLatestPackage(UnityVersionControlPackage);
        }

        private void UseUnityVersionControl(UnityVersionControl versionControlSetting)
        {
            var packageStatus = _pu.StatusOfPackage(UnityVersionControlPackage);
            switch (packageStatus)
            {
                case PackageStatus.UpToDate:
                {
                    StatePackageUpToDate(versionControlSetting);
                    break;
                }
                case PackageStatus.Outdated:
                {
                    StatePackageOutdated(versionControlSetting);
                    break;
                }
                case PackageStatus.NotInstalled:
                {
                    StatePackageNotInstalled();
                    break;
                }
                case PackageStatus.NotInitialised:
                default:
                    throw new InvalidOperationException("Not supported package status " + packageStatus);
            }
        }

        private void StatePackageNotInstalled()
        {
            _status = Status.Error;
            _result.Status(SourceControlStatus.NoSourceControl);
        }

        private void StatePackageOutdated(UnityVersionControl versionControlSetting)
        {
            _status = Status.Warning;
            if (versionControlSetting == UnityVersionControl.HiddenMetaFiles || versionControlSetting == UnityVersionControl.VisibleMetaFiles)
            {
                _canBeFixed = true;
                _result.Status(SourceControlStatus.UnityVersionControlOutdatedAndVersionControlSetting);
            }
            else
            {
                _canBeFixed = true;
                _result.Status(SourceControlStatus.UnityVersionControlOutdated);
            }
        }

        private void StatePackageUpToDate(UnityVersionControl versionControlSetting)
        {
            if (versionControlSetting == UnityVersionControl.HiddenMetaFiles || versionControlSetting == UnityVersionControl.VisibleMetaFiles)
            {
                _status = Status.Warning;
                _result.Status(SourceControlStatus.UnityVersionControlOkAndVersionControlSetting);
                return;
            }

            _status = Status.Ok;
            _result.Status(SourceControlStatus.UnityVersionControlOk);
        }

        private void UseGit(UnityVersionControl versionControlSetting)
        {
            if (versionControlSetting == UnityVersionControl.VisibleMetaFiles)
            {
                _status = Status.Ok;
                _result.Status(SourceControlStatus.GitOk);
            }
            else
            {
                _status = Status.Warning;
                _result.Status(SourceControlStatus.GitVersionControlSetting);
            }
        }
    }
}