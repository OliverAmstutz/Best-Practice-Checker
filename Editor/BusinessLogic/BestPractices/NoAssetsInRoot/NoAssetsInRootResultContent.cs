using System;
using System.Collections.Generic;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.NoAssetsInRoot
{
    [Serializable]
    public sealed class NoAssetsInRootResultContent : IResult
    {
        [SerializeField] private Status status = BusinessLogic.Status.NotCalculated;
        [SerializeField] private List<string> misplacedAssetPaths = new List<string>();

        public string Content()
        {
            return status switch
            {
                BusinessLogic.Status.Ok =>
                    "Excellent, your root hierarchy is clean and organised.",
                BusinessLogic.Status.Warning =>
                    "You have assets in your root hierarchy!\n It is recommended to use subdirectories for storing assets.",
                _ => "Something went wrong in the no assets in root initialization!"
            };
        }

        public void Status(Status packageStatus)
        {
            status = packageStatus;
        }

        public void AddMisplacedAssetsPath(string obj)
        {
            misplacedAssetPaths.Add(obj);
        }

        public List<string> MisplacedAssetsPaths()
        {
            return misplacedAssetPaths;
        }
    }
}