using System.Collections;
using BestPracticeChecker.Editor.UI.BestPractices.TestFramework;
using UnityEditor.PackageManager;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework
{
    public sealed class TestFramework : BestPractice
    {
        private const string ObjectKey = "TestFrameworkResultContent";
        private const string ResultVarKey = "_result";
        private const string CanBeFixedVarKey = "_canBeFixed";
        private ITestFrameworkBusinessLogic _businessLogic;
        private bool _canBeFixed;
        private TestFrameworkResultContent _result;

        public override void Init()
        {
            base.Init();
            if (BusinessLogic == null)
                _businessLogic = new TestFrameworkBusinessLogic();
            else
                _businessLogic = (ITestFrameworkBusinessLogic) BusinessLogic;
            Events.registeredPackages += IsDirtyUpdate;
        }

        private void IsDirtyUpdate(PackageRegistrationEventArgs action)
        {
            IsDirty();
        }

        protected override IEnumerator Evaluation()
        {
            _businessLogic.Evaluation();
            _result = _businessLogic.Result();
            _canBeFixed = _businessLogic.CanBeFixed();
            status = _businessLogic.GetStatus();

            yield return null;

            UpdateUserInterfaceAfterEvaluation();
        }

        public override void Fix()
        {
            _businessLogic.Fix();
        }

        public override void ShowResults()
        {
            ResultEditorFactory.InitialiseResultWindow<TestFrameworkResult>(this);
        }

        protected override void LoadPersistedData()
        {
            _canBeFixed = Persistor.Load(CLASS_KEY + ObjectKey + CanBeFixedVarKey, false);
            status = (Status) Persistor.Load(CLASS_KEY + ObjectKey + STATUS_VAR_KEY, 0);
            _result = JsonUtility.FromJson<TestFrameworkResultContent>(Persistor.Load(
                CLASS_KEY + ObjectKey + ResultVarKey, JsonUtility.ToJson(new TestFrameworkResultContent())));
        }

        protected override void PersistData()
        {
            base.PersistData();
            Persistor.Save(CLASS_KEY + ObjectKey + CanBeFixedVarKey, _canBeFixed);
            Persistor.Save(CLASS_KEY + ObjectKey + STATUS_VAR_KEY, (int) status);
            Persistor.Save(CLASS_KEY + ObjectKey + ResultVarKey, JsonUtility.ToJson(_result));
        }

        public override bool HasFix()
        {
            return _canBeFixed;
        }

        public override IResult GetResult()
        {
            return _result ??= new TestFrameworkResultContent();
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            Events.registeredPackages -= IsDirtyUpdate;
        }
    }
}