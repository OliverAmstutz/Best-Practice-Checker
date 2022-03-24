using System;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.Placeholder
{
    [Serializable]
    public class PlaceholderResultContent : IResult
    {
        [SerializeField] private string content;

        public PlaceholderResultContent(string content)
        {
            this.content = content;
        }

        public string Content()
        {
            return content;
        }
    }
}