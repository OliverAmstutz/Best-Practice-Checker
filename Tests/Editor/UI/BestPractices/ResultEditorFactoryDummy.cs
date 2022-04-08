using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using BestPracticeChecker.Editor.UI.BestPractices;

namespace BestPracticeChecker.Tests.Editor.UI.BestPractices
{
    public class ResultEditorFactoryDummy : IResultEditorFactory
    {
        public ResultEditor InitialiseResultWindow<T>(BestPractice bP) where T : ResultEditor
        {
            return null;
        }
    }
}