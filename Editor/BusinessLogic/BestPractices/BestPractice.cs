using System;
using System.Collections;
using BestPracticeChecker.Editor.BusinessLogic.Persistor;
using BestPracticeChecker.Editor.BusinessLogic.WebAccess;
using BestPracticeChecker.Editor.UI.BestPractices;
using Unity.EditorCoroutines.Editor;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices
{
    [Serializable]
    public abstract class BestPractice : ScriptableObject, IBestPractice
    {
        public delegate void UpdateStatusEvent();

        public delegate void UpdateUIEvent();

        protected const string CLASS_KEY = "BEST_PRACTICE_CHECKER_BestPractice_";
        protected const string STATUS_VAR_KEY = "_status";

        [SerializeField] protected Status status = Status.NotCalculated;

        private IWebAccess _webAccess = new WebAccess.WebAccess();

        protected BestPracticeName BpName;
        protected IBusinessLogic BusinessLogic;

        protected EditorCoroutine EditorCoroutine;

        protected IPersistor Persistor = new Persistor.Persistor();
        protected IResultEditorFactory ResultEditorFactory;

        private void OnDestroy()
        {
            CleanUp();
        }

        public BestPracticeName GetName()
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
            _webAccess.OpenDocumentation(BpName);
        }

        public Status GetCalculationStatus()
        {
            return status;
        }

        public void StopRun()
        {
            if (EditorCoroutine == null) return;

            EditorCoroutineUtility.StopCoroutine(EditorCoroutine);
            status = Status.NotCalculated;
            UpdateStatusAndUi();
            Debug.Log(BpName + " calculation stopped!");
        }

        public abstract void Fix();

        public abstract IResult GetResult();

        public abstract bool HasFix();

        public virtual void Init()
        {
            BestPracticeCheckerEditor.BeforeShutdown += CleanUp;
            LoadPersistedData();
        }

        public void Init(IPersistor persistor, IWebAccess webAccess, IBusinessLogic businessLogic,
            IResultEditorFactory resultEditorFactory)
        {
            Persistor = persistor;
            BusinessLogic = businessLogic;
            ResultEditorFactory = resultEditorFactory;
            _webAccess = webAccess;
            Init();
        }

        protected virtual void CleanUp()
        {
            PersistData();
            BestPracticeCheckerEditor.BeforeShutdown -= CleanUp;
        }

        protected abstract void LoadPersistedData();

        protected virtual void PersistData()
        {
            if (status.Equals(Status.Running)) StopRun();
        }

        public void Name(BestPracticeName bpName)
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
            if (UpdateStatus == null || UpdateUI == null) return;
            UpdateStatus();
            UpdateUI();
        }

        protected void IsDirty()
        {
            status = Status.NeedUpdate;
            UpdateStatusAndUi();
        }
    }
}