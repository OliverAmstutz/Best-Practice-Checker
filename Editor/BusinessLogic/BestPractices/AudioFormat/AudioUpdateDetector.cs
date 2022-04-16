using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.AudioFormat
{
    public sealed class AudioUpdateDetector : AssetPostprocessor
    {
        public delegate void ImportAudioEvent();

        /// <summary>
        ///     Deleted Assets are only available after deletion, no means to detect type of deleted asset.
        /// </summary>
        /// <param name="importedAssets"></param>
        /// <param name="deletedAssets"></param>
        /// <param name="movedAssets"></param>
        /// <param name="movedFromAssetPaths"></param>
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
            string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (deletedAssets.Length > 0 && ImportAudio != null)
                ImportAudio();
        }

        private void OnPostprocessAudio(AudioClip arg)
        {
            ImportAudio?.Invoke();
        }

        public static event ImportAudioEvent ImportAudio;
    }
}