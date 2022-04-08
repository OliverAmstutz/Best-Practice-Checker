using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace BestPracticeChecker.Editor.BusinessLogic.PackageUtility
{
    public class PackageUtility : IPackageUtility
    {
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
            return _packages.Where(packageInfo => packageInfo.name.Equals(packageName)).Select(packageInfo =>
                packageInfo.versions.verified.Equals(GetVersionFromPackageId(packageInfo.packageId))).FirstOrDefault();
        }

        public void InstallLatestPackage(string packageName)
        {
            if (!_listRequest.IsCompleted) WaitForInitialisation();
            Debug.Log("Installing " + packageName);
            _addRequest = Client.Add(packageName);
            EditorApplication.update += AddProgress;
            while (!_addRequest.IsCompleted)
            {
                //Is on purpose empty, as it is used to stuck here until the async list call is executed.
            }

            AddProgress();
            Debug.Log("Installation of " + packageName + " was a " + _addRequest.Status);
        }

        public void RemovePackage(string packageName)
        {
            if (!_listRequest.IsCompleted) WaitForInitialisation();
            var rmRequest = Client.Remove(packageName);
            while (!rmRequest.IsCompleted)
            {
                //Is on purpose empty, as it is used to stuck here until the async list call is executed.
            }

            Debug.Log("Removing " + packageName + " was a: " + rmRequest.Status);
        }

        private void WaitForInitialisation()
        {
            while (!_listRequest.IsCompleted)
            {
                //Is on purpose empty, as it is used to stuck here until the async list call is executed.
            }
        }

        private void ListProgress()
        {
            if (!_listRequest.IsCompleted) return;
            if (_listRequest.Status == StatusCode.Success)
                foreach (var package in _listRequest.Result)
                    _packages.Add(package);

            else if (_listRequest.Status >= StatusCode.Failure)
                Debug.LogError(_listRequest.Error.message);

            EditorApplication.update -= ListProgress;
        }

        private void ReinitialisePackages()
        {
            if (_packages.Count != 0) return;
            ListProgress();
        }

        private static string GetVersionFromPackageId(string packageId)
        {
            return packageId.Split(char.Parse("@"))[1];
        }

        private void AddProgress()
        {
            if (!_addRequest.IsCompleted) return;
            if (_addRequest.Status >= StatusCode.Failure)
                Debug.Log(_addRequest.Error.message);

            EditorApplication.update -= AddProgress;
        }
    }
}