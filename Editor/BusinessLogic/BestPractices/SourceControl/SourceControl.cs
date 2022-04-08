using System;
using System.Collections;
using BestPracticeChecker.Editor.UI.BestPractices.SourceControl;
using UnityEditor.PackageManager;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.SourceControl
{
    [Serializable]
    public sealed class SourceControl : BestPractice
    {
        private const string ObjectKey = "SourceControlResultContent";
        private const string ResultVarKey = "_result";
        private const string CanBeFixedVarKey = "_canBeFixed";
        private ISourceControlBusinessLogic _businessLogic;
        private bool _canBeFixed;
        private SourceControlResultContent _result;

        public override void Init()
        {
            base.Init();
            if (BusinessLogic == null)
                _businessLogic = new SourceControlBusinessLogic();
            else
                _businessLogic = (ISourceControlBusinessLogic) BusinessLogic;
            Events.registeredPackages += IsDirtyUpdate;
        }

        private void IsDirtyUpdate(PackageRegistrationEventArgs action)
        {
            IsDirty();
        }

        protected override IEnumerator Evaluation()
        {
            _businessLogic.Evaluation();
            _canBeFixed = _businessLogic.CanBeFixed();
            _result = _businessLogic.Result();
            status = _businessLogic.GetStatus();
            yield return null;

            UpdateUserInterfaceAfterEvaluation();
        }

        public override void Fix()
        {
            _businessLogic.Fix();
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            Events.registeredPackages -= IsDirtyUpdate;
        }

        public override void ShowResults()
        {
            ResultEditorFactory.InitialiseResultWindow<SourceControlResult>(this);
        }

        protected override void LoadPersistedData()
        {
            _canBeFixed = Persistor.Load(CLASS_KEY + ObjectKey + CanBeFixedVarKey, false);
            status = (Status) Persistor.Load(CLASS_KEY + ObjectKey + STATUS_VAR_KEY, 0);
            _result = JsonUtility.FromJson<SourceControlResultContent>(Persistor.Load(
                CLASS_KEY + ObjectKey + ResultVarKey, JsonUtility.ToJson(new SourceControlResultContent())));
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
            return _result ??= new SourceControlResultContent();
        }
    }
}