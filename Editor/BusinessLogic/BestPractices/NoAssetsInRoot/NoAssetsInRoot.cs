using System;
using System.Collections;
using BestPracticeChecker.Editor.UI.BestPractices.NoAssetsInRoot;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.NoAssetsInRoot
{
    public sealed class NoAssetsInRoot : BestPractice
    {
        private const string ObjectKey = "NoAssetsInRootContent";
        private const string ResultVarKey = "_result";
        private const string CanBeFixedVarKey = "_canBeFixed";
        private bool _canBeFixed;
        private NoAssetsInRootResultContent _result;

        public override void Init()
        {
            base.Init();
            BusinessLogic ??= new NoAssetsInRootBusinessLogic();
            AssetUpdateDetector.ImportAsset += IsDirty;
        }

        protected override IEnumerator Evaluation()
        {
            BusinessLogic.Evaluation();
            _result = (NoAssetsInRootResultContent) BusinessLogic.Result();
            status = BusinessLogic.GetStatus();
            _canBeFixed = BusinessLogic.CanBeFixed();
            yield return null;
            UpdateUserInterfaceAfterEvaluation();
        }

        public override void Fix()
        {
            throw new NotImplementedException("No Assets in Root Directory has no fix!");
        }

        public override void ShowResults()
        {
            Window = ResultEditorFactory.InitialiseResultWindow<NoAssetsInRootResult>(this);
        }

        protected override void LoadPersistedData()
        {
            _canBeFixed = Persistor.Load(CLASS_KEY + ObjectKey + CanBeFixedVarKey, false);
            status = (Status) Persistor.Load(CLASS_KEY + ObjectKey + STATUS_VAR_KEY, 0);
            _result = JsonUtility.FromJson<NoAssetsInRootResultContent>(Persistor.Load(CLASS_KEY + ObjectKey + ResultVarKey,
                JsonUtility.ToJson(new NoAssetsInRootResultContent())));
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
            return _result ??= new NoAssetsInRootResultContent();
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            AssetUpdateDetector.ImportAsset -= IsDirty;
        }
    }
}