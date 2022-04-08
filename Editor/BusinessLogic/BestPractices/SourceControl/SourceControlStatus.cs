namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.SourceControl
{
    public enum SourceControlStatus
    {
        NotInitialised,
        GitOk,
        GitVersionControlSetting,
        UnityVersionControlOk,
        UnityVersionControlOutdated,
        UnityVersionControlOutdatedAndVersionControlSetting,
        UnityVersionControlOkAndVersionControlSetting,
        NoSourceControl
    }
}