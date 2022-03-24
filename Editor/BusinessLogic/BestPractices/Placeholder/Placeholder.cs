using System;
using System.Collections;
using BestPracticeChecker.Editor.Helper;
using BestPracticeChecker.Editor.UI;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.Placeholder
{
    [Serializable]
    public sealed class Placeholder : BestPractice
    {
        private const bool CanBeFixed = true;
        private const string ObjectKey = "PlaceholderResultContent";
        private const string ResultVarKey = "_result";
        private PlaceholderResultContent _result;
        private float _timeToWait;


        protected override void OnEnable()
        {
            base.OnEnable();
            EditorApplication.hierarchyChanged += IsDirty;
        }

        protected override IEnumerator Evaluation()
        {
            _result = new PlaceholderResultContent("Best Practice Result of " + BpName + " with time to wait: " +
                                                   _timeToWait);

            yield return new EditorWaitForSeconds(_timeToWait);

            status = Status.Ok; //ToDo: Make this dependent on the result!

            UpdateUserInterfaceAfterEvaluation();
        }

        public void SetTimeToWait(float timeToWait)
        {
            _timeToWait = timeToWait;
        }

        public override void Fix()
        {
            Debug.Log("Fix best practice here!");
        }

        public override void ShowResults()
        {
            var window = (ResultEditor) EditorWindow.GetWindow(typeof(PlaceholderResult),
                true, BpName + " Result", true);
            window.GetBestPracticeData(this);
            window.ShowAuxWindow();
        }

        protected override void LoadPersistedData()
        {
            status = (Status) EditorPrefs.GetInt(CLASS_KEY + ObjectKey + STATUS_VAR_KEY, 0);
            _result = JsonUtility.FromJson<PlaceholderResultContent>(EditorPrefs.GetString(
                CLASS_KEY + ObjectKey + ResultVarKey,
                JsonUtility.ToJson(new PlaceholderResultContent("UNDEFINED"))));
        }

        protected override void PersistData()
        {
            base.PersistData();
            EditorPrefs.SetInt(CLASS_KEY + ObjectKey + STATUS_VAR_KEY, (int) status);
            EditorPrefs.SetString(CLASS_KEY + ObjectKey + ResultVarKey, JsonUtility.ToJson(_result));
        }

        public override bool HasFix()
        {
            return CanBeFixed;
        }

        public override IResult GetResult()
        {
            return _result;
        }
    }
}