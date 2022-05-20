using System;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices
{
    public static class BestPracticeNameUtility
    {
        public static string Name(BestPracticeName bpn)
        {
            return bpn switch
            {
                BestPracticeName.Placeholder => "Placeholder",
                BestPracticeName.Audio_Format => "Audio Format",
                BestPracticeName.Test_Framework => "Test Framework",
                BestPracticeName.Texture_Ratio => "Texture Ratio",
                BestPracticeName.Source_Control => "Source Control",
                BestPracticeName.Code_Coverage => "Code Coverage",
                BestPracticeName.No_Assets_In_Root => "No Assets in Root",
                BestPracticeName.Code_Analyser => "Code Analyser",
                _ => throw new ArgumentOutOfRangeException(nameof(bpn), bpn, "Invalid best practice: " + bpn)
            };
        }
    }
}