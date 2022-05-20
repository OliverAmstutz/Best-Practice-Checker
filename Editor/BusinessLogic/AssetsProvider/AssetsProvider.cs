using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using Object = UnityEngine.Object;

namespace BestPracticeChecker.Editor.BusinessLogic.AssetsProvider
{
    public sealed class AssetsProvider : IAssetsProvider
    {
        private const string MetaFileExtension = "meta";
        private const string MarkDownFileExtension = "md";
        private const string TextFileExtension = "txt";
        private const string TypePrefix = "t:";
        private const string SearchPattern = "*.*";
        private const char ParameterSeparator = '.';

        public IReadOnlyList<T> FindAllAssetsOfType<T>(string searchInFolders) where T : Object
        {
            var typeName = TypePrefix + typeof(T).Name;
            var assets = AssetDatabase.FindAssets(typeName, new[] {searchInFolders});
            var assetsList = new List<T>();
            if (assets.Length <= 0) return assetsList;
            assetsList.AddRange(assets.Select(asset => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(asset))));
            return assetsList.AsReadOnly();
        }

        public IReadOnlyList<Object> FindAllAssetsInFolder(string searchInFolder)
        {
            var assets = Directory.GetFiles(searchInFolder, SearchPattern, SearchOption.TopDirectoryOnly);
            var assetsList = new List<Object>();
            if (assets.Length <= 0) return assetsList;

            assetsList.AddRange(from asset in assets
                let subStrings = asset.Split(ParameterSeparator)
                let fileExtension = subStrings[subStrings.Length - 1]
                where IsNotWhiteListed(fileExtension)
                select AssetDatabase.LoadAssetAtPath<Object>(asset)
                into obj
                where obj != null
                select obj);

            return assetsList.AsReadOnly();
        }

        public string FileExtensionOfAsset(Object asset)
        {
            return Path.GetExtension(AssetDatabase.GetAssetPath(asset));
        }

        public bool FindFolderFromStartPath(string folderName, string path)
        {
            string[] foundGitFolder;
            var currentPath = path;
            var root = Directory.GetDirectoryRoot(path);
            try
            {
                foundGitFolder = Directory.GetDirectories(currentPath, folderName, SearchOption.AllDirectories);
            }
            catch (DirectoryNotFoundException)
            {
                return false;
            }

            if (foundGitFolder.Length > 0) return true;
            while (foundGitFolder.Length <= 0 && !currentPath.Equals(root))
            {
                foundGitFolder = Directory.GetDirectories(currentPath, folderName, SearchOption.TopDirectoryOnly);
                if (foundGitFolder.Length > 0) return true;
                try
                {
                    currentPath = Directory.GetParent(currentPath).FullName;
                }
                catch (NullReferenceException)
                {
                    return false;
                }
            }

            return false;
        }

        public Object FindAssetOfNameAndFileExtension(string fileName, string fileExtension)
        {
            var allAssets = FindAllAssetsOfType<Object>("Assets");
            return allAssets.Where(a => FileExtensionOfAsset(a).Contains(fileExtension)).FirstOrDefault(a => a.name.Equals(fileName));
        }

        private bool IsNotWhiteListed(string fileExtension)
        {
            return !fileExtension.Equals(MetaFileExtension) || !fileExtension.Equals(MarkDownFileExtension) || !fileExtension.Equals(TextFileExtension);
        }
    }
}