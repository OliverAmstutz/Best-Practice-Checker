using System.Collections.Generic;
using System.Linq;
using BestPracticeChecker.Editor.BusinessLogic.AssetsProvider;
using UnityEngine;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.AssetsProvider
{
    public sealed class AssetsProviderStub : IAssetsProvider
    {
        private readonly List<Object> _allAssets;
        private readonly string _fileExtension;
        private readonly bool _findFolderFromStart;
        private readonly Object _foundAsset;

        public AssetsProviderStub(List<Object> allAssets, bool findFolderFromStart, string fileExtension, Object foundAsset)
        {
            _allAssets = allAssets;
            _findFolderFromStart = findFolderFromStart;
            _fileExtension = fileExtension;
            _foundAsset = foundAsset;
        }

        public IReadOnlyList<T> FindAllAssetsOfType<T>(string searchInFolders) where T : Object
        {
            var list = _allAssets.Cast<T>().ToList();
            return list.AsReadOnly();
        }

        public bool FindFolderFromStartPath(string folderName, string path)
        {
            return _findFolderFromStart;
        }

        public string FileExtensionOfAsset(Object asset)
        {
            return _fileExtension;
        }

        public Object FindAssetOfNameAndFileExtension(string fileName, string fileExtension)
        {
            return _foundAsset;
        }

        public IReadOnlyList<Object> FindAllAssetsInFolder(string searchInFolder)
        {
            return _allAssets;
        }
    }
}