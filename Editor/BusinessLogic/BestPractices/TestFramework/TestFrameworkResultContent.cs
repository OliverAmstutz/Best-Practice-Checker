using System;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework
{
    [Serializable]
    public sealed class TestFrameworkResultContent : IResult
    {
        [SerializeField] private Status status = BusinessLogic.Status.NotCalculated;

        public string Content()
        {
            return status switch
            {
                BusinessLogic.Status.Ok =>
                    "You have the Unity test framework installed and its up to date!\n" +
                    " Visit this best practice documentation for test framework sources.",
                BusinessLogic.Status.Warning =>
                    "You have the Unity test framework installed, but its not up to date!\n" +
                    " Press \"fix\"to automatically update the package.",
                BusinessLogic.Status.Error =>
                    "You do not have the Unity test framework installed!\n" +
                    " Press \"fix\" to automatically install the package and consult this best practice documentation for test framework sources.",
                _ => "Something went wrong in the test framework initialization!"
            };
        }

        public void Status(Status packageStatus)
        {
            status = packageStatus;
        }
    }
}