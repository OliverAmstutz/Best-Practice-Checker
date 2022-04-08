using System.Collections.Generic;
using BestPracticeChecker.Editor.BusinessLogic.AssetsProvider;
using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio
{
    public class TextureRatioBusinessLogic : ITextureRatioBusinessLogic
    {
        private const string Root = "Assets";
        private readonly IAssetsProvider _assetsProvider;
        private readonly string _rootFolder;
        private bool _canBeFixed;
        private TextureRatioResultContent _result = new TextureRatioResultContent();
        private Status _status;
        private IReadOnlyList<Texture> _textures;

        public TextureRatioBusinessLogic() : this(Root)
        {
        }

        public TextureRatioBusinessLogic(string rootFolder)
        {
            _rootFolder = rootFolder;
            _assetsProvider = new AssetsProvider.AssetsProvider();
            _textures = new List<Texture>().AsReadOnly();
        }

        public void Evaluation()
        {
            _textures = _assetsProvider.FindAllAssetsOfType<Texture>(_rootFolder);
            _result = new TextureRatioResultContent();
            _status = Status.Ok;
            _canBeFixed = false;

            foreach (var texture in _textures) AnalyseTexture(texture);

            _result.Status(_status);
        }

        public void Fix()
        {
            _result.FaultyTextures().ForEach(FixTextureCompressionSetting);
        }

        public bool CanBeFixed()
        {
            return _canBeFixed;
        }

        public TextureRatioResultContent Result()
        {
            return _result;
        }

        public Status GetStatus()
        {
            return _status;
        }


        private void AnalyseTexture(Texture texture)
        {
            var textureImporter = AssetImportProvider.ImporterForTexture(texture);
            if (textureImporter == null) return;
            var isUnCompressed = textureImporter.textureCompression.Equals(TextureImporterCompression.Uncompressed);
            if (texture.width % 2 != 0 || texture.height % 2 != 0)
            {
                if (_status.Equals(Status.Ok))
                {
                    if (isUnCompressed)
                    {
                        _status = Status.Error;
                        _canBeFixed = true;
                    }
                    else
                    {
                        _status = Status.Warning;
                    }
                }

                _result.AddFaultyTexture(new FaultyTexture(texture, isUnCompressed));
            }
            else
            {
                if (!isUnCompressed) return;
                _status = Status.Error;
                _canBeFixed = true;
                _result.AddFaultyTexture(new FaultyTexture(texture, true));
            }
        }

        private static void FixTextureCompressionSetting(FaultyTexture faultyTexture)
        {
            var textureImporter = AssetImportProvider.ImporterForTexture(faultyTexture.Texture());
            textureImporter.textureCompression = TextureImporterCompression.Compressed;
            textureImporter.SaveAndReimport();
        }
    }
}