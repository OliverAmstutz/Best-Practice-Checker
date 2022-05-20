using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace BestPracticeChecker.Editor.BusinessLogic.PackageUtility
{
    public sealed class PackageUtility : IPackageUtility
    {
        private const string ParseSymbol = "@";
        private const string EmptyString = "";
        private const char ParameterSeparatorDot = '.';
        private const char ParameterSeparatorDash = '-';
        private const int VerifiedVersionLength = 3;
        private readonly ListRequest _listRequest;
        private readonly List<PackageInfo> _packages;
        private AddRequest _addRequest;


        public PackageUtility()
        {
            _packages = new List<PackageInfo>();
            _listRequest = Client.List();
            EditorApplication.update += ListProgress;
        }

        public PackageStatus StatusOfPackage(string packageName)
        {
            if (!_listRequest.IsCompleted) WaitForInitialisation();
            if (!PackageExists(packageName)) return PackageStatus.NotInstalled;
            return IsUpToDate(packageName) ? PackageStatus.UpToDate : PackageStatus.Outdated;
        }

        public bool PackageExists(string packageName)
        {
            if (!_listRequest.IsCompleted) WaitForInitialisation();
            ReinitialisePackages();
            return _packages.Any(packageInfo => packageInfo.name.Equals(packageName));
        }

        public bool IsUpToDate(string packageName)
        {
            if (!_listRequest.IsCompleted) WaitForInitialisation();
            ReinitialisePackages();
            return _packages.Any(packageInfo => VerifyVersion(packageName, packageInfo));
        }

        public void InstallLatestPackage(string packageName)
        {
            if (!_listRequest.IsCompleted) WaitForInitialisation();
            Debug.Log("Installing " + packageName);
            _addRequest = Client.Add(packageName);
            EditorApplication.update += AddProgress;
            while (!_addRequest.IsCompleted)
            {
                //Is on purpose empty, as it is used to stuck here until the async list call is finished.
            }

            AddProgress();
            Debug.Log("Installation of " + packageName + " was a " + _addRequest.Status);
        }

        private bool VerifyVersion(string packageName, PackageInfo packageInfo)
        {
            if (!packageInfo.name.Equals(packageName)) return false;
            var version = packageInfo.versions.verified;
            if (version.Equals(EmptyString) && GetLatestVerifiedVersion(packageInfo).Equals(GetVersionFromPackageId(packageInfo.packageId))) return true;
            return version.Equals(GetVersionFromPackageId(packageInfo.packageId));
        }

        public void RemovePackage(string packageName)
        {
            if (!_listRequest.IsCompleted) WaitForInitialisation();
            var rmRequest = Client.Remove(packageName);
            while (!rmRequest.IsCompleted)
            {
                //Is on purpose empty, as it is used to stuck here until the async list call is finished.
            }

            Debug.Log("Removing " + packageName + " was a: " + rmRequest.Status);
        }

        private string GetLatestVerifiedVersion(PackageInfo packageInfo)
        {
            var allVersions = packageInfo.versions.all;
            for (var i = allVersions.Length - 1; i >= 0; i--)
            {
                var split = allVersions[i].Split(ParameterSeparatorDot, ParameterSeparatorDash);
                if (split.Length != VerifiedVersionLength) continue;
                return allVersions[i];
            }

            return EmptyString;
        }

        private void WaitForInitialisation()
        {
            while (!_listRequest.IsCompleted)
            {
                //Is on purpose empty, as it is used to stuck here until the async list call is finished.
            }
        }

        private void ListProgress()
        {
            if (!_listRequest.IsCompleted) return;
            if (_listRequest.Status == StatusCode.Success) _packages.AddRange(_listRequest.Result);
            else if (_listRequest.Status >= StatusCode.Failure) Debug.LogError(_listRequest.Error.message);
            EditorApplication.update -= ListProgress;
        }

        private void ReinitialisePackages()
        {
            if (_packages.Count != 0) return;
            ListProgress();
        }

        private static string GetVersionFromPackageId(string packageId)
        {
            return packageId.Split(char.Parse(ParseSymbol))[1];
        }

        private void AddProgress()
        {
            if (!_addRequest.IsCompleted) return;
            if (_addRequest.Status >= StatusCode.Failure) Debug.Log(_addRequest.Error.message);
            EditorApplication.update -= AddProgress;
        }
    }
}