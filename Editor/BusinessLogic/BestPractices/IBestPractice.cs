namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices
{
    /// <summary>
    ///     Interface to best practice business logic.
    /// </summary>
    public interface IBestPractice
    {
        /// <summary>
        ///     Executes the best practice analysis.
        /// </summary>
        void Run();

        /// <summary>
        ///     Interrupts the calculation of the best practice.
        /// </summary>
        void StopRun();

        /// <summary>
        ///     Displays result of best practice analysis.
        /// </summary>
        void ShowResults();

        /// <summary>
        ///     Returns the Result object.
        /// </summary>
        /// <returns>IResult marker interface.</returns>
        IResult GetResult();

        /// <summary>
        ///     Show best practice documentation.
        /// </summary>
        void ShowDocumentation();

        /// <summary>
        ///     Fix best practices.
        /// </summary>
        /// <returns>Boolean if the operation was successful.</returns>
        void Fix();

        /// <summary>
        ///     Returns a boolean if the best practice has a Fix.
        /// </summary>
        /// <returns> Boolean of best practice has a fix. </returns>
        bool HasFix();

        /// <summary>
        ///     Returns the current Status of the best practice.
        /// </summary>
        /// <returns>Status enum.</returns>
        Status GetCalculationStatus();

        /// <summary>
        ///     Returns the best practice name.
        /// </summary>
        /// <returns></returns>
        string GetName();
    }
}