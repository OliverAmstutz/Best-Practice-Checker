using BestPracticeChecker.Editor.UI;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.Placeholder
{
    internal sealed class PlaceholderResult : ResultEditor
    {
        private IBestPractice _bestPractice;
        private IResult _result;

        protected override void DisplayResult()
        {
            if (_result == null && _bestPractice != null) _result = _bestPractice.GetResult();

            if (_result != null)
                GUILayout.Label("Result Object: " + _result.Content());
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
                _bestPractice.Fix();
            else
                Debug.LogWarning("_bestPractice is null!");
        }
    }
}