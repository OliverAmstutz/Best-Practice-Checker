using System;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework
{
    [Serializable]
    public class TestFrameworkResultContent : IResult
    {
        [SerializeField] private string content;

        public TestFrameworkResultContent(string content)
        {
            this.content = content;
        }

        public string Content()
        {
            return content;
        }
    }
}