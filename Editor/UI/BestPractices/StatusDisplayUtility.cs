using BestPracticeChecker.Editor.BusinessLogic;
using UnityEngine;

namespace BestPracticeChecker.Editor.UI.BestPractices
{
    public sealed class StatusDisplayUtility
    {
        private const int Height = 18;
        private const int Width = 18;
        private Texture _errorTexture;
        private Texture _needUpdateTexture;
        private Texture _notCalculatedTexture;
        private Texture _notSelectedTexture;
        private Texture _okTexture;
        private Texture _runningTexture;
        private Texture _unknownTexture;
        private Texture _warningTexture;


        public StatusDisplayUtility()
        {
            InitialiseUiTextures();
        }

        public void DisplayStatus(Status status)
        {
            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            switch (status)
            {
                case Status.Ok:
                {
                    GUILayout.Label(new GUIContent(_okTexture, "Status: Ok"), GUILayout.Width(Width), GUILayout.Height(Height));
                    break;
                }
                case Status.Warning:
                {
                    GUILayout.Label(new GUIContent(_warningTexture, "Status: Warning"), GUILayout.Width(Width), GUILayout.Height(Height));
                    break;
                }
                case Status.NotSelected:
                {
                    GUILayout.Label(new GUIContent(_notSelectedTexture, "Status: Not Selected"), GUILayout.Width(Width), GUILayout.Height(Height));
                    break;
                }
                case Status.NotCalculated:
                {
                    GUILayout.Label(new GUIContent(_notCalculatedTexture, "Status: Not Calculated"), GUILayout.Width(Width), GUILayout.Height(Height));
                    break;
                }
                case Status.Running:
                {
                    GUILayout.Label(new GUIContent(_runningTexture, "Status: Running"), GUILayout.Width(Width), GUILayout.Height(Height));
                    break;
                }
                case Status.Error:
                {
                    GUILayout.Label(new GUIContent(_errorTexture, "Status: Error"), GUILayout.Width(Width), GUILayout.Height(Height));
                    break;
                }
                case Status.NeedUpdate:
                {
                    GUILayout.Label(new GUIContent(_needUpdateTexture, "Status: Need Update"), GUILayout.Width(Width), GUILayout.Height(Height));
                    break;
                }
                default:
                    GUILayout.Label(new GUIContent(_unknownTexture, "Status: Unknown"), GUILayout.Width(Width), GUILayout.Height(Height));
                    break;
            }
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