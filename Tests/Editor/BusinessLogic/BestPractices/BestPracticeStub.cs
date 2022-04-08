using System.Collections;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices
{


    internal class BestPracticeStub : BestPractice
    {
        private const bool HAS_FIX = false;
        public override void ShowResults()
        {
            //No implementation.
        }

        public override void Fix()
        {
            //No implementation.
        }

        public override IResult GetResult()
        {
            return null;
        }

        public override bool HasFix()
        {
            return HAS_FIX;
        }

        protected override void LoadPersistedData()
        {
            //No implementation.
        }

        protected override IEnumerator Evaluation()
        {
            yield return null;
        }

    }
}