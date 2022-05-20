using System;
using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using BestPracticeChecker.Editor.BusinessLogic.Persistor;
using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Editor.UI.BestPractices
{
    [Serializable]
    public sealed class BestPracticeEntry : ScriptableObject
    {
        private const string ClassKey = "BEST_PRACTICE_CHECKER_BestPracticeEntry_";
        private const string StatusVarKey = "_isActive";
        private const string ResultButtonText = "Results";
        private const string DocumentationButtonText = "Documentation";
        [SerializeField] private bool isActive = true;

        private IBestPractice _bestPractice;
        private string _name = "";
        private string _objectKey = "";
        private IPersistor _persistor = new Persistor();
        private SerializedProperty _propIsActive;
        private SerializedObject _so;
        private Status _status = Status.NotCalculated;
        private StatusDisplayUtility _statusDisplayUtility;

        public void Init(IBestPractice bestPractice, string entityName)
        {
            _name = entityName;
            _bestPractice = bestPractice;
            BestPractice.UpdateStatus += UpdateStatus;
            _so = new SerializedObject(this);
            _propIsActive = _so.FindProperty(nameof(isActive));
            BestPracticeCheckerEditor.BeforeShutdown += CleanUp;
            _objectKey = _bestPractice.GetName();
            isActive = _persistor.Load(ClassKey + _objectKey + StatusVarKey, true);
            _statusDisplayUtility = new StatusDisplayUtility();
        }

        public void Init(IBestPractice bestPractice, string entityName, IPersistor persistor)
        {
            _persistor = persistor;
            Init(bestPractice, entityName);
        }

        private void CleanUp()
        {
            BestPractice.UpdateStatus -= UpdateStatus;
            _persistor.Save(ClassKey + _objectKey + StatusVarKey, isActive);
            BestPracticeCheckerEditor.BeforeShutdown -= CleanUp;
        }

        public void BestPracticeEntryUI()
        {
            using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
            {
                TickBox();

                UpdateStatus();

                _statusDisplayUtility.DisplayStatus(_status);

                ResultButton();

                DocumentationButton();
            }
        }

        public void SwitchActive()
        {
            PersistIsActive(!isActive);
        }

        public bool IsActive()
        {
            return isActive;
        }

        public void RunBestPractice()
        {
            _bestPractice.Run();
            _status = _bestPractice.GetCalculationStatus();
        }

        private void UpdateStatus()
        {
            if (_bestPractice != null) _status = _bestPractice.GetCalculationStatus();
            if (isActive)
            {
                if (_status.Equals(Status.NotSelected)) _status = Status.NotCalculated;
            }
            else
            {
                _status = Status.NotSelected;
            }
        }

        public bool HasResult()
        {
            if (_status == Status.Ok || _status == Status.Error || _status == Status.Warning) return true;
            if (_status != Status.NotSelected) return false;
            var status = _bestPractice.GetCalculationStatus();
            return status == Status.Ok || status == Status.Error || status == Status.Warning;
        }

        public bool IsRunning()
        {
            return _status.Equals(Status.Running);
        }

        public void Stop()
        {
            _bestPractice.StopRun();
        }

        private void PersistIsActive(bool newIsActive)
        {
            if (_so == null) return;
            _so.Update();
            isActive = newIsActive;
            _so.ApplyModifiedProperties();
        }

        private void TickBox()
        {
            if (_so == null) return;
            _so.Update();
            EditorGUILayout.PropertyField(_propIsActive, new GUIContent(_name));
            _so.ApplyModifiedProperties();
        }

        private void ResultButton()
        {
            using (new EditorGUI.DisabledScope(!HasResult()))
            {
                if (GUILayout.Button(new GUIContent(ResultButtonText, "Opens the result window"))) _bestPractice.ShowResults();
            }
        }

        private void DocumentationButton()
        {
            if (GUILayout.Button(new GUIContent(DocumentationButtonText, "Link to this best practice documentation"))) _bestPractice.ShowDocumentation();
        }

        public bool HasFix()
        {
            return _bestPractice.HasFix();
        }

        public void Fix()
        {
            _bestPractice.Fix();
        }
    }
}