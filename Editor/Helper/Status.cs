namespace BestPracticeChecker.Editor.Helper
{
    /// <summary>
    /// Serialization of enum Status by integer, therefore, the order of Status is important!
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