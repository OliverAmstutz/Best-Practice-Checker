using System;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.SourceControl
{
    [Serializable]
    public sealed class SourceControlResultContent : IResult
    {
        [SerializeField] private SourceControlStatus status = SourceControlStatus.NotInitialised;

        public string Content()
        {
            return status switch
            {
                SourceControlStatus.GitOk => "Excellent, you use Git for source control.",
                SourceControlStatus.GitVersionControlSetting =>
                    "You use git without the appropriate Version Control settings. Turn Version Control mode to \"Visible Meta Files\" in the Project Settings.\n For further information, visit this best practice documentation.",
                SourceControlStatus.UnityVersionControlOk =>
                    "Unity Version Control is used. Resources in the documentation recommend to use Git instead of Unity version control system.",
                SourceControlStatus.UnityVersionControlOkAndVersionControlSetting =>
                    "The Version Control mode is not configured appropriately. Turn Version Control mode to \"Perforce\" in the Project Settings.",
                SourceControlStatus.UnityVersionControlOutdated => "Your Unity Version Control package is outdated, press \"fix\" to update Unity Version Control!",
                SourceControlStatus.UnityVersionControlOutdatedAndVersionControlSetting =>
                    "Your Unity Version Control package is outdated, press \"fix\" to update Unity Version Control! \n In addition, the Version Control mode is not configured appropriately. Turn Version Control mode to \"Perforce\" in the Project Settings",
                SourceControlStatus.NoSourceControl =>
                    "You use no source control! It is highly recommended to use Git! Find resources or tutorials in this best practice documentation.",
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