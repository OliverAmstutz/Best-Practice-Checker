using System;
using System.Collections;
using BestPracticeChecker.Editor.UI.BestPractices.CodeAnalyser;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeAnalyser
{
    [Serializable]
    public sealed class CodeAnalyser : BestPractice
    {
        private const string ObjectKey = "CodeAnalyserResultContent";
        private const string ResultVarKey = "_result";
        private const string CanBeFixedVarKey = "_canBeFixed";
        private bool _canBeFixed;
        private CodeAnalyserResultContent _result;

        public override void Init()
        {
            base.Init();
            BusinessLogic ??= new CodeAnalyserBusinessLogic();
            CodeAnalyserDetector.ChangePlugin += IsDirty;
        }

        protected override IEnumerator Evaluation()
        {
            BusinessLogic.Evaluation();
            _canBeFixed = BusinessLogic.CanBeFixed();
            _result = (CodeAnalyserResultContent) BusinessLogic.Result();
            status = BusinessLogic.GetStatus();
            yield return null;

            UpdateUserInterfaceAfterEvaluation();
        }

        public override void Fix()
        {
            BusinessLogic.Fix();
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            CodeAnalyserDetector.ChangePlugin -= IsDirty;
        }

        public override void ShowResults()
        {
            ResultEditorFactory.InitialiseResultWindow<CodeAnalyserResult>(this);
        }

        protected override void LoadPersistedData()
        {
            _canBeFixed = Persistor.Load(CLASS_KEY + ObjectKey + CanBeFixedVarKey, false);
            status = (Status) Persistor.Load(CLASS_KEY + ObjectKey + STATUS_VAR_KEY, 0);
            _result = JsonUtility.FromJson<CodeAnalyserResultContent>(Persistor.Load(
                CLASS_KEY + ObjectKey + ResultVarKey, JsonUtility.ToJson(new CodeAnalyserResultContent())));
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
            return _result ??= new CodeAnalyserResultContent();
        }
    }
}