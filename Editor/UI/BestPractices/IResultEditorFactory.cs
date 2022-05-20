using BestPracticeChecker.Editor.BusinessLogic.BestPractices;

namespace BestPracticeChecker.Editor.UI.BestPractices
{
    /// <summary>
    ///     Factory for Result editor objects.
    /// </summary>
    public interface IResultEditorFactory
    {
        /// <summary>
        ///     Initialises the result editor of given best practice type.
        /// </summary>
        /// <param name="bP"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ResultEditor InitialiseResultWindow<T>(BestPractice bP) where T : ResultEditor;
    }
}