using System;
using System.Collections;
using System.Collections.Generic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Editor.UI
{
    [Serializable]
    public class CompoundControls : ScriptableObject
    {
        private const string ClassKey = "BEST_PRACTICE_CHECKER_";
        private const string DeselectAllText = "De-Select All";
        private const string FixAllText = "Fix All";
        private const string ObjectKey = "CompoundControls";
        private const string RunSelectedText = "Run selected";
        private const string ScrollVarKey = "_scrollPosition";
        private const string SelectAllText = "Select All";
        private const string StopText = "Stop";
        private readonly List<BestPracticeEntry> _listOfBestPractices = new List<BestPracticeEntry>();

        private Vector2 _scrollPosition;

        private void OnEnable()
        {
            _scrollPosition = JsonUtility.FromJson<Vector2>(EditorPrefs.GetString(
                ClassKey + ObjectKey + ScrollVarKey, JsonUtility.ToJson(new Vector2(0.0f, 0.0f))));
            BestPracticeCheckerEditor.BeforeShutdown += PersistState;
            _listOfBestPractices.AddRange(InstantiateBestPractice.All());
        }

        public void BestPractices()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                foreach (var bpe in _listOfBestPractices)
                    bpe.BestPracticeEntryUI();
            }

            EditorGUILayout.EndScrollView();
        }


        public void SelectionButtons()
        {
            using (new GUILayout.VerticalScope())
            {
                using (new GUILayout.HorizontalScope())
                {
                    if (IsRunning())
                        using (new EditorGUI.DisabledScope(!IsRunning()))
                        {
                            if (GUILayout.Button(StopText)) StopRunning();
                        }
                    else
                        using (new EditorGUI.DisabledScope(HasNoActiveBestPractices() || IsRunning()))
                        {
                            if (GUILayout.Button(RunSelectedText)) RunSelected();
                        }

                    using (new EditorGUI.DisabledScope(HasNoInactiveBestPractices() || IsRunning()))
                    {
                        if (GUILayout.Button(SelectAllText)) SelectAll();
                    }
                }

                using (new GUILayout.HorizontalScope())
                {
                    using (new EditorGUI.DisabledScope(!HasFixWithinAllBestPractices()))
                    {
                        if (GUILayout.Button(FixAllText)) FixAll();
                    }

                    using (new EditorGUI.DisabledScope(HasNoActiveBestPractices() || IsRunning()))
                    {
                        if (GUILayout.Button(DeselectAllText)) DeSelectAll();
                    }
                }
            }
        }

        private void FixAll()
        {
            foreach (var bpe in _listOfBestPractices)
                if (HasFix(bpe))
                    bpe.Fix();
        }

        private bool HasFixWithinAllBestPractices()
        {
            foreach (var bpe in _listOfBestPractices)
                if (HasFix(bpe))
                    return true;

            return false;
        }

        private bool HasFix(BestPracticeEntry bpe)
        {
            return bpe.IsActive() && bpe.HasFix() && bpe.HasResult();
        }

        private void StopRunning()
        {
            foreach (var bpe in _listOfBestPractices)
                if (bpe.IsRunning())
                    bpe.Stop();
        }

        private void RunSelected()
        {
            EditorCoroutineUtility.StartCoroutineOwnerless(RunAllBestPractices());
        }

        private void SelectAll()
        {
            foreach (var bestPracticeEntry in _listOfBestPractices)
                if (!bestPracticeEntry.IsActive())
                {
                    Undo.RecordObject(bestPracticeEntry, SelectAllText);
                    bestPracticeEntry.SwitchActive();
                }
        }

        private void DeSelectAll()
        {
            foreach (var bestPracticeEntry in _listOfBestPractices)
                if (bestPracticeEntry.IsActive())
                {
                    Undo.RecordObject(bestPracticeEntry, DeselectAllText);
                    bestPracticeEntry.SwitchActive();
                }
        }


        private IEnumerator RunAllBestPractices()
        {
            foreach (var bpe in _listOfBestPractices)
                if (bpe.IsActive())
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
            foreach (var bpe in _listOfBestPractices)
                if (bpe.IsActive())
                    return false;

            return true;
        }

        private bool HasNoInactiveBestPractices()
        {
            foreach (var bpe in _listOfBestPractices)
                if (!bpe.IsActive())
                    return false;

            return true;
        }

        private bool IsRunning()
        {
            foreach (var bpe in _listOfBestPractices)
                if (bpe.IsRunning())
                    return true;

            return false;
        }

        public void PersistState()
        {
            EditorPrefs.SetString(ClassKey + ObjectKey + ScrollVarKey, JsonUtility.ToJson(_scrollPosition));
            BestPracticeCheckerEditor.BeforeShutdown -= PersistState;
        }
    }
}