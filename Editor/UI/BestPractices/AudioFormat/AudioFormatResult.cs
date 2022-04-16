using System.Collections.Generic;
using System.Linq;
using BestPracticeChecker.Editor.BusinessLogic.AssetsProvider;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.AudioFormat;
using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Editor.UI.BestPractices.AudioFormat
{
    public sealed class AudioFormatResult : ResultEditor
    {
        private readonly AssetsProvider _assetsProvider;
        private IBestPractice _bestPractice;
        private AudioFormatResultContent _result;

        public AudioFormatResult()
        {
            _assetsProvider = new AssetsProvider();
        }

        protected override void DisplayResult()
        {
            if (_result == null && _bestPractice != null)
                _result = (AudioFormatResultContent) _bestPractice.GetResult();


            if (_result != null)
            {
                GUILayout.Label(_result.Content());
                DisplayNotIdealAudioClips(_result.NotIdealAudioClips());
            }
            else
            {
                Debug.LogWarning("_result is null, _bestPractice not yet initialized!");
            }
        }

        public override void GetBestPracticeData(IBestPractice bP)
        {
            _bestPractice = bP;
            HasFix = bP.HasFix();
        }

        protected override void Fix()
        {
            if (_bestPractice != null)
                _bestPractice.Fix();
            else
                Debug.LogWarning("_bestPractice is null!");
        }

        private void DisplayNotIdealAudioClips(List<string> audioClipPaths)
        {
            if (audioClipPaths.Count == 0) return;
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                foreach (var audioClip in audioClipPaths.Select(AssetDatabase.LoadAssetAtPath<AudioClip>)
                             .Where(audioClip => audioClip != null))
                    using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
                    {
                        if (GUILayout.Button("View " + audioClip.name))
                            Highlighter.HighlightObject(audioClip);
                        GUILayout.Label("audio format: " + _assetsProvider.FileExtensionOfAsset(audioClip));
                    }
            }
        }
    }
}