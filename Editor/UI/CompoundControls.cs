using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BestPracticeChecker.Editor.BusinessLogic.Persistor;
using BestPracticeChecker.Editor.UI.BestPractices;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Editor.UI
{
    [Serializable]
    public sealed class CompoundControls : ScriptableObject
    {
        private const string ClassKey = "BEST_PRACTICE_CHECKER_CompoundControls_";
        private const string DeselectAllText = "De-Select All";
        private const string FixSelectedText = "Fix selected     ";
        private const string RunSelectedText = "Run selected";
        private const string StopText = "Stop              ";
        private const string SelectAllText = "Select All   ";
        private const string ObjectKey = "CompoundControls";
        private const string ScrollVarKey = "_scrollPosition";

        private readonly List<BestPracticeEntry> _listOfBestPractices = new List<BestPracticeEntry>();
        private IPersistor _persistor = new Persistor();

        private Vector2 _scrollPosition;

        public void Init()
        {
            _scrollPosition = JsonUtility.FromJson<Vector2>(_persistor.Load(ClassKey + ObjectKey + ScrollVarKey, JsonUtility.ToJson(new Vector2(0.0f, 0.0f))));
            BestPracticeCheckerEditor.BeforeShutdown += PersistState;
            _listOfBestPractices.AddRange(BestPracticeEntryFactory.CreateAll());
        }

        public void Init(IPersistor persistor)
        {
            _persistor = persistor;
            Init();
        }

        public void BestPractices()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                _listOfBestPractices.ForEach(bpe => bpe.BestPracticeEntryUI());
            }

            EditorGUILayout.EndScrollView();
        }


        public void SelectionButtons()
        {
            GUI.skin.button.alignment = TextAnchor.MiddleLeft;
            using (new GUILayout.VerticalScope())
            {
                using (new GUILayout.HorizontalScope())
                {
                    LeftSideButtons();
                }

                using (new GUILayout.HorizontalScope())
                {
                    RightSideButtons();
                }
            }
        }

        private void RightSideButtons()
        {
            using (new EditorGUI.DisabledScope(!HasFixWithinAllBestPractices()))
            {
                FixSelectedButton();
            }

            using (new EditorGUI.DisabledScope(HasNoActiveBestPractices() || IsRunning()))
            {
                DeSelectButton();
            }
        }

        private void LeftSideButtons()
        {
            if (IsRunning())
                using (new EditorGUI.DisabledScope(!IsRunning()))
                {
                    StopButton();
                }
            else
                using (new EditorGUI.DisabledScope(HasNoActiveBestPractices() || IsRunning()))
                {
                    RunButton();
                }

            using (new EditorGUI.DisabledScope(HasNoInactiveBestPractices() || IsRunning()))
            {
                SelectAllButton();
            }
        }

        private void DeSelectButton()
        {
            if (GUILayout.Button(new GUIContent(DeselectAllText, "De-selects all best practices"))) DeSelectAll();
        }

        private void FixSelectedButton()
        {
            if (GUILayout.Button(new GUIContent(FixSelectedText,
                    "Tries to fix all selected best practices. Individual best practice fix option is in the result window available"))) FixSelected();
        }

        private void SelectAllButton()
        {
            if (GUILayout.Button(new GUIContent(SelectAllText, "Selects all best practices"), GUILayout.ExpandWidth(true))) SelectAll();
        }

        private void RunButton()
        {
            if (GUILayout.Button(new GUIContent(RunSelectedText, "Run's all selected best practices"))) RunSelected();
        }

        private void StopButton()
        {
            if (GUILayout.Button(new GUIContent(StopText, "Stops all current calculations"))) StopRunning();
        }

        private void FixSelected()
        {
            foreach (var bpe in _listOfBestPractices.Where(HasFix)) bpe.Fix();
        }

        private bool HasFixWithinAllBestPractices()
        {
            return _listOfBestPractices.Any(HasFix);
        }

        private bool HasFix(BestPracticeEntry bpe)
        {
            return bpe.IsActive() && bpe.HasFix() && bpe.HasResult();
        }

        private void StopRunning()
        {
            foreach (var bpe in _listOfBestPractices.Where(bpe => bpe.IsRunning())) bpe.Stop();
        }

        private void RunSelected()
        {
            EditorCoroutineUtility.StartCoroutineOwnerless(RunAllBestPractices());
        }

        private void SelectAll()
        {
            foreach (var bestPracticeEntry in _listOfBestPractices.Where(bestPracticeEntry => !bestPracticeEntry.IsActive()))
            {
                Undo.RecordObject(bestPracticeEntry, SelectAllText);
                bestPracticeEntry.SwitchActive();
            }
        }

        private void DeSelectAll()
        {
            foreach (var bestPracticeEntry in _listOfBestPractices.Where(bestPracticeEntry => bestPracticeEntry.IsActive()))
            {
                Undo.RecordObject(bestPracticeEntry, DeselectAllText);
                bestPracticeEntry.SwitchActive();
            }
        }


        private IEnumerator RunAllBestPractices()
        {
            foreach (var bpe in _listOfBestPractices.Where(bpe => bpe.IsActive()))
            {
                EditorCoroutineUtility.StartCoroutineOwnerless(RunBestPractice(bpe));
                yield return null;
            }
        }

        private IEnumerator RunBestPractice(BestPracticeEntry bpe)
        {
            bpe.RunBestPractice();
            yield return null;
        }

        private bool HasNoActiveBestPractices()
        {
            return _listOfBestPractices.All(bpe => !bpe.IsActive());
        }

        private bool HasNoInactiveBestPractices()
        {
            return _listOfBestPractices.All(bpe => bpe.IsActive());
        }

        private bool IsRunning()
        {
            return _listOfBestPractices.Any(bpe => bpe.IsRunning());
        }

        private void PersistState()
        {
            _persistor.Save(ClassKey + ObjectKey + ScrollVarKey, JsonUtility.ToJson(_scrollPosition));
            BestPracticeCheckerEditor.BeforeShutdown -= PersistState;
        }
    }
}