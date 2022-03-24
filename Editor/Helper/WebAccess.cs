using UnityEngine;

namespace BestPracticeChecker.Editor.Helper
{
    public static class WebAccess
    {
        private const string URLRoot = "https://github.com/OliverAmstutz/Best-Practice-Checker/wiki/";

        public static void OpenDocumentation(BestPracticeNames bp)
        {
            Application.OpenURL(URLRoot + bp);
        }
    }
}