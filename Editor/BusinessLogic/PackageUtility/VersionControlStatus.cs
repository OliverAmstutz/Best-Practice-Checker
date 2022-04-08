using System.ComponentModel;

namespace BestPracticeChecker.Editor.BusinessLogic.PackageUtility
{
    public class VersionControlStatus : IVersionControlStatus
    {
        public UnityVersionControl Evaluate(string mode)
        {
            return mode switch
            {
                "Hidden Meta Files" => UnityVersionControl.HiddenMetaFiles,
                "Visible Meta Files" => UnityVersionControl.VisibleMetaFiles,
                "Perforce" => UnityVersionControl.Perforce,
                "Plastic SCM" => UnityVersionControl.PlasticSCM,
                _ => throw new InvalidEnumArgumentException("Version Control Settings invalid option")
            };
        }
    }
}