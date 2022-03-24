using System.Collections.Generic;
using BestPracticeChecker.Editor.Helper;
using BestPracticeChecker.Editor.UI;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices
{
    public class InstantiateBestPractice : ScriptableObject
    {
        public static List<BestPracticeEntry> All()
        {
            var listOfBestPractices = new List<BestPracticeEntry>
            {
                AddTemplateForTestingPurposes(),
                AddTestFramework()
            };

            return listOfBestPractices;
        }

        private static BestPracticeEntry AddTemplateForTestingPurposes()
        {
            var bp = CreateInstance<Placeholder.Placeholder>();
            bp.Name(BestPracticeNames.Template);
            bp.SetTimeToWait(1f);
            var bpe = CreateInstance<BestPracticeEntry>();
            bpe.SetBestPractice(bp, bp.GetName().ToString());
            return bpe;
        }

        private static BestPracticeEntry AddTestFramework()
        {
            var bp = CreateInstance<TestFramework.TestFramework>();
            bp.Name(BestPracticeNames.Test_Framework);
            var bpe = CreateInstance<BestPracticeEntry>();
            bpe.SetBestPractice(bp, bp.GetName().ToString());
            return bpe;
        }
    }
}