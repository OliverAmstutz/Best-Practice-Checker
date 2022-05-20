using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.WebAccess
{
    public sealed class WebAccess : IWebAccess
    {
        private const string URLRoot = "https://github.com/OliverAmstutz/Best-Practice-Checker/wiki/";

        public void OpenDocumentation(BestPracticeName bp)
        {
            Application.OpenURL(URLRoot + bp);
        }
    }
}