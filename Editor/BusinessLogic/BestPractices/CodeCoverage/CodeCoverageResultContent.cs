using System;
using BestPracticeChecker.Editor.BusinessLogic.PackageUtility;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeCoverage
{
    [Serializable]
    public class CodeCoverageResultContent : IResult
    {
        [SerializeField] private PackageStatus status = PackageStatus.NotInitialised;

        public string Content()
        {
            return status switch
            {
                PackageStatus.UpToDate => "Excellent, code coverage package is installed and up-to-date!",
                PackageStatus.Outdated =>
                    "Your code coverage package is outdated. Please update manually or through the \"fix\" button.",
                PackageStatus.NotInstalled =>
                    "You use no code coverage. It is strongly recommended to install and utilise code coverage in your project. You can install code coverage through the \"fix\" button.",
                _ => "Something went wrong in the code coverage initialization!"
            };
        }

        public void Status(PackageStatus newStatus)
        {
            status = newStatus;
        }
    }
}