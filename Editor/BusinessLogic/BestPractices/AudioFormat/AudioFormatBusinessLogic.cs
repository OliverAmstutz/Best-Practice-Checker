using System;
using System.Collections.Generic;
using BestPracticeChecker.Editor.BusinessLogic.AssetsProvider;
using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.AudioFormat
{
    public class AudioFormatBusinessLogic : IBusinessLogic<AudioFormatResultContent>
    {
        private const string Root = "Assets";
        private const bool _canBeFixed = false;
        private readonly IAssetsProvider _assetsProvider;
        private readonly string _rootFolder;
        private IReadOnlyList<AudioClip> _audios;
        private AudioFormatResultContent _result = new AudioFormatResultContent();
        private Status _status;

        public AudioFormatBusinessLogic() : this(Root)
        {
        }

        public AudioFormatBusinessLogic(string rootFolder)
        {
            _rootFolder = rootFolder;
            _assetsProvider = new AssetsProvider.AssetsProvider();
            _audios = new List<AudioClip>().AsReadOnly();
        }

        public void Evaluation()
        {
            _audios = _assetsProvider.FindAllAssetsOfType<AudioClip>(_rootFolder);
            _result = new AudioFormatResultContent();
            _status = Status.Ok;

            foreach (var audio in _audios) AnalyseAudio(audio);

            _result.Status(_status);
        }

        public void Fix()
        {
            throw new InvalidOperationException("Audio Format has no fix operation!");
        }

        public bool CanBeFixed()
        {
            return _canBeFixed;
        }

        public AudioFormatResultContent Result()
        {
            return _result;
        }

        public Status GetStatus()
        {
            return _status;
        }

        private void AnalyseAudio(AudioClip audio)
        {
            var audioType = new AudioFormatType().Evaluate(_assetsProvider.FileExtensionOfAsset(audio));
            switch (audioType)
            {
                case SupportedAudioFormat.Mp3:
                {
                    if (_status == Status.Ok) _status = Status.Warning;
                    _result.AddNotIdealAudioClip(AssetDatabase.GetAssetPath(audio));
                    break;
                }
                case SupportedAudioFormat.Ogg:
                {
                    if (_status == Status.Ok) _status = Status.Warning;
                    _result.AddNotIdealAudioClip(AssetDatabase.GetAssetPath(audio));
                    break;
                }
                case SupportedAudioFormat.Wav:
                    break;
                case SupportedAudioFormat.Aif:
                    break;
                case SupportedAudioFormat.Mod:
                    break;
                case SupportedAudioFormat.It:
                    break;
                case SupportedAudioFormat.S3M:
                    break;
                case SupportedAudioFormat.Xm:
                    break;
                case SupportedAudioFormat.UnkownAudioFormat:
                {
                    _status = Status.Error;
                    _result.AddNotIdealAudioClip(AssetDatabase.GetAssetPath(audio));
                    break;
                }
                default:
                    throw new InvalidOperationException("Invalid audio Type detected");
            }
        }
    }
}