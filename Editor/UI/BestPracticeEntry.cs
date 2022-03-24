using System;
using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.Helper;
using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Editor.UI
{
    [Serializable]
    public sealed class BestPracticeEntry : ScriptableObject
    {
        private const string ClassKey = "BEST_PRACTICE_CHECKER_";
        private const string StatusVarKey = "_isActive";
        private const string ResultButtonText = "Results";
        private const string DocumentationButtonText = "Documentation";
        [SerializeField] private bool isActive = true;

        private IBestPractice _bestPractice;
        private string _name = "";
        private string _objectKey = "";
        private SerializedProperty _propIsActive;
        private SerializedObject _so;
        private Status _status = Status.NotCalculated;

        private void OnEnable()
        {
            _so = new SerializedObject(this);
            _propIsActive = _so.FindProperty(nameof(isActive));
            BestPracticeCheckerEditor.BeforeShutdown += CleanUp;
            BestPractice.UpdateStatus += UpdateStatus;
        }

        private void CleanUp()
        {
            EditorPrefs.SetBool(ClassKey + _objectKey + StatusVarKey, isActive);
            BestPractice.UpdateStatus -= UpdateStatus;
            BestPracticeCheckerEditor.BeforeShutdown -= CleanUp;
        }

        public void SetBestPractice(IBestPractice bestPractice, string entityName)
        {
            _name = entityName;
            _bestPractice = bestPractice;
            _objectKey = _bestPractice.GetName().ToString();
            isActive = EditorPrefs.GetBool(ClassKey + _objectKey + StatusVarKey, true);
        }


        public void BestPracticeEntryUI()
        {
            using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
            {
                TickBox();

                UpdateStatus();
                GUILayout.Label("Status: " + _status);

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
            return _status.Equals(Status.Ok) || _status.Equals(Status.Error) || _status.Equals(Status.Warning);
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
            _so.Update();
            isActive = newIsActive;
            _so.ApplyModifiedProperties();
        }

        private void TickBox()
        {
            _so.Update();
            EditorGUILayout.PropertyField(_propIsActive, new GUIContent(_name));
            _so.ApplyModifiedProperties();
        }

        private void ResultButton()
        {
            using (new EditorGUI.DisabledScope(!HasResult()))
            {
                if (GUILayout.Button(ResultButtonText)) _bestPractice.ShowResults();
            }
        }

        private void DocumentationButton()
        {
            if (GUILayout.Button(DocumentationButtonText)) _bestPractice.ShowDocumentation();
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