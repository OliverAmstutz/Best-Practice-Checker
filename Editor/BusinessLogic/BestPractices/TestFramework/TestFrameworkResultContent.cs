using System;
using BestPracticeChecker.Editor.BusinessLogic.PackageUtility;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework
{
    [Serializable]
    public sealed class TestFrameworkResultContent : IResult
    {
        [SerializeField] private PackageStatus status = PackageStatus.NotInitialised;

        public string Content()
        {
            return status switch
            {
                PackageStatus.UpToDate =>
                    "You have the Unity test framework installed and its up to date!\n" +
                    " Visit this best practice documentation for test framework resources.",
                PackageStatus.Outdated =>
                    "You have the Unity test framework installed, but its not up to date!\n" +
                    " Press \"fix\"to automatically update the package.",
                PackageStatus.NotInstalled =>
                    "You do not have the Unity test framework installed!\n" +
                    " Press \"fix\" to automatically install the package and consult this best practice documentation for test framework resources.",
                _ => "Something went wrong in the test framework initialization!"
            };
        }

        public void Status(PackageStatus packageStatus)
        {
            status = packageStatus;
        }
    }
}