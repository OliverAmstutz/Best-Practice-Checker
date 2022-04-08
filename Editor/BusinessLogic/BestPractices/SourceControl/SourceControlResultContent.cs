using System;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.SourceControl
{
    [Serializable]
    public class SourceControlResultContent : IResult
    {
        [SerializeField] private SourceControlStatus status = SourceControlStatus.NotInitialised;

        public string Content()
        {
            return status switch
            {
                SourceControlStatus.GitOk => "Excellent, you use Git for source control.",
                SourceControlStatus.GitVersionControlSetting =>
                    "It seems, Git is used for Source control. If that's true, turn Version Control mode to Visible Meta Files in the Project Settings",
                SourceControlStatus.UnityVersionControlOk =>
                    "Unity Version Control is used. Sources in the documentation recommend git instead of Unity version control system",
                SourceControlStatus.UnityVersionControlOkAndVersionControlSetting =>
                    "The Version Control mode is not set to Unity Version control. Turn Version Control mode to Perforce in the Project Settings",
                SourceControlStatus.UnityVersionControlOutdated =>
                    "Your Unity Version Control package is outdated, press \"fix\" to update Unity Version Control!",
                SourceControlStatus.UnityVersionControlOutdatedAndVersionControlSetting =>
                    "Your Unity Version Control package is outdated, press \"fix\" to update Unity Version Control! \n " +
                    "In addition, the Version Control mode is not set to Unity Version control. Turn Version Control mode to Perforce in the Project Settings",
                SourceControlStatus.NoSourceControl =>
                    "You use no source control! It is highly recommended to use Git! Find a tutorial in this best practice documentation.",
                _ => "Something went wrong in the source control initialization!"
            };
        }

        public void Status(SourceControlStatus sourceControlStatus)
        {
            status = sourceControlStatus;
        }

        public SourceControlStatus Status()
        {
            return status;
        }
    }
}