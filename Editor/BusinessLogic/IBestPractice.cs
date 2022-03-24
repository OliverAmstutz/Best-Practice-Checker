using BestPracticeChecker.Editor.Helper;

namespace BestPracticeChecker.Editor.BusinessLogic
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
        ///     Returns the Result object implementing the IResult marker interface.
        /// </summary>
        /// <returns>IResult marker interface.</returns>
        IResult GetResult();

        /// <summary>
        ///     Opens a link to the best practice documentation.
        /// </summary>
        void ShowDocumentation();

        /// <summary>
        ///     Updates context according best practices.
        /// </summary>
        /// <returns>Boolean if the operation was successful.</returns>
        void Fix();

        /// <summary>
        ///     Returns a boolean if the best practice has a Fix.
        /// </summary>
        /// <returns> Boolean of best practice has a fix. </returns>
        bool HasFix();

        /// <summary>
        ///     Returns the current <c>Status</c> of the best practice.
        /// </summary>
        /// <returns>Status enum.</returns>
        Status GetCalculationStatus();

        /// <summary>
        ///     Returns the best practice enum.
        /// </summary>
        /// <returns>BestPracticeNames enum.</returns>
        BestPracticeNames GetName();
    }
}