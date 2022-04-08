using System.Collections;
using BestPracticeChecker.Editor.UI.BestPractices.TextureRatio;
using Unity.EditorCoroutines.Editor;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio
{
    public sealed class TextureRatio : BestPractice
    {
        private const string ObjectKey = "TextureRatioContent";
        private const string ResultVarKey = "_result";
        private const string CanBeFixedVarKey = "_canBeFixed";
        private ITextureRatioBusinessLogic _businessLogic;
        private bool _canBeFixed;
        private TextureRatioResultContent _result;

        public override void Init()
        {
            base.Init();
            _businessLogic = new TextureRatioBusinessLogic();
            TextureUpdateDetector.ImportTexture += IsDirty;
        }

        protected override IEnumerator Evaluation()
        {
            _businessLogic.Evaluation();
            _result = _businessLogic.Result();
            status = _businessLogic.GetStatus();
            _canBeFixed = _businessLogic.CanBeFixed();
            yield return null;
            UpdateUserInterfaceAfterEvaluation();
        }

        public override void Fix()
        {
            _businessLogic.Fix();

            EditorCoroutineUtility.StartCoroutineOwnerless(Evaluation());
        }

        public override void ShowResults()
        {
            ResultEditorFactory.InitialiseResultWindow<TextureRatioResult>(this);
        }

        protected override void LoadPersistedData()
        {
            _canBeFixed = Persistor.Load(CLASS_KEY + ObjectKey + CanBeFixedVarKey, false);
            status = (Status) Persistor.Load(CLASS_KEY + ObjectKey + STATUS_VAR_KEY, 0);
            _result = JsonUtility.FromJson<TextureRatioResultContent>(Persistor.Load(
                CLASS_KEY + ObjectKey + ResultVarKey, JsonUtility.ToJson(new TextureRatioResultContent())));
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
            return _result ??= new TextureRatioResultContent();
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            TextureUpdateDetector.ImportTexture -= IsDirty;
        }
    }
}