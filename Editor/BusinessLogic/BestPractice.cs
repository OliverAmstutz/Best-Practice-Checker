using System;
using System.Collections;
using BestPracticeChecker.Editor.Helper;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic
{
    [Serializable]
    public abstract class BestPractice : ScriptableObject, IBestPractice
    {
        public delegate void UpdateStatusEvent();

        public delegate void UpdateUIEvent();

        protected const string CLASS_KEY = "BEST_PRACTICE_CHECKER_";
        protected const string STATUS_VAR_KEY = "_status";

        [SerializeField] protected Status status = Status.NotCalculated;

        protected BestPracticeNames BpName;
        protected EditorCoroutine EditorCoroutine;

        protected virtual void OnEnable()
        {
            BestPracticeCheckerEditor.BeforeShutdown += CleanUp;
            LoadPersistedData();
        }

        public BestPracticeNames GetName()
        {
            return BpName;
        }

        public void Run()
        {
            status = Status.Running;
            UpdateStatusAndUi();
            EditorCoroutine = EditorCoroutineUtility.StartCoroutineOwnerless(Evaluation());
        }

        public abstract void ShowResults();

        public void ShowDocumentation()
        {
            WebAccess.OpenDocumentation(BpName);
        }

        public Status GetCalculationStatus()
        {
            return status;
        }

        public void StopRun()
        {
            if (EditorCoroutine != null)
            {
                EditorCoroutineUtility.StopCoroutine(EditorCoroutine);
                status = Status.NotCalculated;

                UpdateStatusAndUi();
                Debug.Log(BpName + " calculation stopped!");
            }
        }

        public abstract void Fix();

        public abstract IResult GetResult();

        public abstract bool HasFix();

        protected virtual void CleanUp()
        {
            PersistData();
            EditorApplication.hierarchyChanged -= IsDirty;
            BestPracticeCheckerEditor.BeforeShutdown -= CleanUp;
        }

        protected abstract void LoadPersistedData();

        protected virtual void PersistData()
        {
            if (status.Equals(Status.Running)) StopRun();
        }

        public virtual void Name(BestPracticeNames bpName)
        {
            BpName = bpName;
        }

        public static event UpdateUIEvent UpdateUI;

        public static event UpdateStatusEvent UpdateStatus;


        protected abstract IEnumerator Evaluation();

        protected void UpdateUserInterfaceAfterEvaluation()
        {
            UpdateStatusAndUi();
        }

        private void UpdateStatusAndUi()
        {
            if (UpdateStatus != null && UpdateUI != null)
            {
                UpdateStatus();
                UpdateUI();
            }
        }

        protected void IsDirty()
        {
            status = Status.NeedUpdate;
            UpdateStatusAndUi();
        }
    }
}