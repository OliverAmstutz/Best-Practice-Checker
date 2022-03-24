using System;
using System.Collections;
using BestPracticeChecker.Editor.Helper;
using BestPracticeChecker.Editor.UI;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.TestFramework
{
    [Serializable]
    public sealed class TestFramework : BestPractice
    {
        private const bool CanBeFixed = false;
        private const string ObjectKey = "TestFrameworkResultContent";
        private const string ResultVarKey = "_result";
        private TestFrameworkResultContent _result;

        protected override void OnEnable()
        {
            base.OnEnable();
            EditorApplication.hierarchyChanged += IsDirty;
        }

        protected override IEnumerator Evaluation()
        {
            _result = new TestFrameworkResultContent("Best Practice Result of " + BpName);

            yield return new EditorWaitForSeconds(2f);
            status = Status.Ok; //ToDo: Make this dependent on the result!
            UpdateUserInterfaceAfterEvaluation();
        }

        public override void Fix()
        {
            Debug.Log("Fix best practice here!");
        }

        public override void ShowResults()
        {
            var window = (ResultEditor) EditorWindow.GetWindow(typeof(TestFrameworkResult),
                true, BpName + " Result", true);
            window.GetBestPracticeData(this);
            window.ShowAuxWindow();
        }

        protected override void LoadPersistedData()
        {
            status = (Status) EditorPrefs.GetInt(CLASS_KEY + ObjectKey + STATUS_VAR_KEY, 0);
            _result = JsonUtility.FromJson<TestFrameworkResultContent>(EditorPrefs.GetString(
                CLASS_KEY + ObjectKey + ResultVarKey,
                JsonUtility.ToJson(new TestFrameworkResultContent("UNDEFINED"))));
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