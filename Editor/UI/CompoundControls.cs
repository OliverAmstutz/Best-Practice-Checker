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
        private const string FixAllText = "Fix All                ";
        private const string ObjectKey = "CompoundControls";
        private const string RunSelectedText = "Run selected";
        private const string ScrollVarKey = "_scrollPosition";
        private const string SelectAllText = "Select All   ";
        private const string StopText = "Stop              ";

        private readonly List<BestPracticeEntry> _listOfBestPractices = new List<BestPracticeEntry>();
        private IPersistor _persistor = new Persistor();

        private Vector2 _scrollPosition;

        public void Init()
        {
            _scrollPosition = JsonUtility.FromJson<Vector2>(_persistor.Load(ClassKey + ObjectKey + ScrollVarKey,
                JsonUtility.ToJson(new Vector2(0.0f, 0.0f))));
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
                        if (GUILayout.Button(SelectAllText, GUILayout.ExpandWidth(true))) SelectAll();
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
            foreach (var bpe in _listOfBestPractices.Where(HasFix))
                bpe.Fix();
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
            foreach (var bpe in _listOfBestPractices.Where(bpe => bpe.IsRunning()))
                bpe.Stop();
        }

        private void RunSelected()
        {
            EditorCoroutineUtility.StartCoroutineOwnerless(RunAllBestPractices());
        }

        private void SelectAll()
        {
            foreach (var bestPracticeEntry in _listOfBestPractices.Where(bestPracticeEntry =>
                         !bestPracticeEntry.IsActive()))
            {
                Undo.RecordObject(bestPracticeEntry, SelectAllText);
                bestPracticeEntry.SwitchActive();
            }
        }

        private void DeSelectAll()
        {
            foreach (var bestPracticeEntry in _listOfBestPractices.Where(bestPracticeEntry =>
                         bestPracticeEntry.IsActive()))
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