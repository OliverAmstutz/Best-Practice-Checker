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
        private const int Height = 18;
        private const int Width = 18;
        private const string StatusVarKey = "_isActive";
        private const string ResultButtonText = "Results";
        private const string DocumentationButtonText = "Documentation";
        [SerializeField] private bool isActive = true;

        private IBestPractice _bestPractice;
        private Texture _errorTexture;
        private string _name = "";
        private Texture _needUpdateTexture;
        private Texture _notCalculatedTexture;
        private Texture _notSelectedTexture;
        private string _objectKey = "";
        private Texture _okTexture;
        private IPersistor _persistor = new Persistor();
        private SerializedProperty _propIsActive;
        private Texture _runningTexture;
        private SerializedObject _so;

        private Status _status = Status.NotCalculated;
        private Texture _unknownTexture;
        private Texture _warningTexture;

        public void Init(IBestPractice bestPractice, string entityName)
        {
            _name = entityName;
            _bestPractice = bestPractice;
            BestPractice.UpdateStatus += UpdateStatus;
            _so = new SerializedObject(this);
            _propIsActive = _so.FindProperty(nameof(isActive));
            BestPracticeCheckerEditor.BeforeShutdown += CleanUp;
            _objectKey = _bestPractice.GetName().ToString();
            isActive = _persistor.Load(ClassKey + _objectKey + StatusVarKey, true);
            InitialiseUiTextures();
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

                DisplayStatus();

                ResultButton();

                DocumentationButton();
            }
        }

        private void DisplayStatus()
        {
            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            switch (_status)
            {
                case Status.Ok:
                {
                    GUILayout.Label(new GUIContent(_okTexture, "Status: Ok"), GUILayout.Width(Width),
                        GUILayout.Height(Height));
                    break;
                }
                case Status.Warning:
                {
                    GUILayout.Label(new GUIContent(_warningTexture, "Status: Warning"), GUILayout.Width(Width),
                        GUILayout.Height(Height));
                    break;
                }
                case Status.NotSelected:
                {
                    GUILayout.Label(new GUIContent(_notSelectedTexture, "Status: Not Selected"), GUILayout.Width(Width),
                        GUILayout.Height(Height));
                    break;
                }
                case Status.NotCalculated:
                {
                    GUILayout.Label(new GUIContent(_notCalculatedTexture, "Status: Not Calculated"),
                        GUILayout.Width(Width), GUILayout.Height(Height));
                    break;
                }
                case Status.Running:
                {
                    GUILayout.Label(new GUIContent(_runningTexture, "Status: Running"), GUILayout.Width(Width),
                        GUILayout.Height(Height));
                    break;
                }
                case Status.Error:
                {
                    GUILayout.Label(new GUIContent(_errorTexture, "Status: Error"), GUILayout.Width(Width),
                        GUILayout.Height(Height));
                    break;
                }
                case Status.NeedUpdate:
                {
                    GUILayout.Label(new GUIContent(_needUpdateTexture, "Status: Need Update"), GUILayout.Width(Width),
                        GUILayout.Height(Height));
                    break;
                }
                default:
                    GUILayout.Label(new GUIContent(_unknownTexture, "Status: Unkown"), GUILayout.Width(Width),
                        GUILayout.Height(Height));
                    break;
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

        private void InitialiseUiTextures()
        {
            _okTexture = Resources.Load("StatusOk") as Texture;
            _warningTexture = Resources.Load("StatusWarning") as Texture;
            _errorTexture = Resources.Load("StatusError") as Texture;
            _needUpdateTexture = Resources.Load("StatusNeedUpdate") as Texture;
            _runningTexture = Resources.Load("StatusRunning") as Texture;
            _notSelectedTexture = Resources.Load("StatusNotSelected") as Texture;
            _notCalculatedTexture = Resources.Load("StatusNotCalculated") as Texture;
            _unknownTexture = Resources.Load("StatusUnknown") as Texture;
        }
    }
}