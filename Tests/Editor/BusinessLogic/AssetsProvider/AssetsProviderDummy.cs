using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BestPracticeChecker.Editor.BusinessLogic.AssetsProvider;
using UnityEngine;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.AssetsProvider
{
    public class AssetsProviderDummy : IAssetsProvider
    {
        private readonly List<Object> _allAssets;
        private readonly bool _findAssetFolder;
        private readonly bool _findFolderFromStart;

        public AssetsProviderDummy(List<Object> allAssets, bool findAssetFolder, bool findFolderFromStart)
        {
            _allAssets = allAssets;
            _findAssetFolder = findAssetFolder;
            _findFolderFromStart = findFolderFromStart;
        }

        public IReadOnlyList<T> FindAllAssetsOfType<T>(string searchInFolders) where T : Object
        {
            var list = _allAssets.Cast<T>().ToList();
            return list.AsReadOnly();
        }

        public bool FindFileAssetFolder(string fileName)
        {
            return _findAssetFolder;
        }

        public bool FindFolderFromStartPath(string folderName, string path)
        {
            return _findFolderFromStart;
        }
    }
}