using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using UnityEngine;

namespace BestPracticeChecker.Editor.UI.BestPractices.SourceControl
{
    public sealed class SourceControlResult : ResultEditor
    {
        private IBestPractice _bestPractice;
        private IResult _result;

        protected override void DisplayResult()
        {
            if (_result == null && _bestPractice != null) _result = _bestPractice.GetResult();

            if (_result != null)
                GUILayout.Label(_result.Content());
            else
                Debug.LogWarning("_result is null, _bestPractice not yet initialized!");
        }

        public override void GetBestPracticeData(IBestPractice bP)
        {
            _bestPractice = bP;
            HasFix = bP.HasFix();
        }

        protected override void Fix()
        {
            if (_bestPractice != null)
            {
                _bestPractice.Fix();
                CleanUp();
                Close();
            }
            else
            {
                Debug.LogWarning("_bestPractice is null!");
            }
        }
    }
}