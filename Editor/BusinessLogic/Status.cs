namespace BestPracticeChecker.Editor.BusinessLogic
{
    /// <summary>
    ///     Serialization of enum Status by integer, therefore, the order of Status is important! Add new status at the end of the list.
    /// </summary>
    public enum Status
    {
        NotSelected,
        NotCalculated,
        Running,
        Ok,
        Warning,
        Error,
        NeedUpdate
    }
}