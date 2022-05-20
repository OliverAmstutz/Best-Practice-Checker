using System.Collections;
using BestPracticeChecker.Editor.UI.BestPractices.AudioFormat;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.AudioFormat
{
    public sealed class AudioFormat : BestPractice
    {
        private const string ObjectKey = "AudioFormatContent";
        private const string ResultVarKey = "_result";
        private const string CanBeFixedVarKey = "_canBeFixed";
        private bool _canBeFixed;
        private AudioFormatResultContent _result;


        public override void Init()
        {
            base.Init();
            BusinessLogic ??= new AudioFormatBusinessLogic();
            AudioUpdateDetector.ImportAudio += IsDirty;
        }

        protected override IEnumerator Evaluation()
        {
            BusinessLogic.Evaluation();
            _result = (AudioFormatResultContent) BusinessLogic.Result();
            status = BusinessLogic.GetStatus();
            _canBeFixed = BusinessLogic.CanBeFixed();
            yield return null;
            UpdateUserInterfaceAfterEvaluation();
        }

        public override void Fix()
        {
            BusinessLogic.Fix();
        }

        public override void ShowResults()
        {
            Window = ResultEditorFactory.InitialiseResultWindow<AudioFormatResult>(this);
        }

        protected override void LoadPersistedData()
        {
            _canBeFixed = Persistor.Load(CLASS_KEY + ObjectKey + CanBeFixedVarKey, false);
            status = (Status) Persistor.Load(CLASS_KEY + ObjectKey + STATUS_VAR_KEY, 0);
            _result = JsonUtility.FromJson<AudioFormatResultContent>(Persistor.Load(
                CLASS_KEY + ObjectKey + ResultVarKey, JsonUtility.ToJson(new AudioFormatResultContent())));
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
            return _result ??= new AudioFormatResultContent();
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            AudioUpdateDetector.ImportAudio -= IsDirty;
        }
    }
}