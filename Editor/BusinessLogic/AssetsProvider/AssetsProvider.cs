using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using Object = UnityEngine.Object;

namespace BestPracticeChecker.Editor.BusinessLogic.AssetsProvider
{
    public class AssetsProvider : IAssetsProvider
    {
        public IReadOnlyList<T> FindAllAssetsOfType<T>(string searchInFolders) where T : Object
        {
            var typeName = "t:" + typeof(T).Name;
            var assets = AssetDatabase.FindAssets(typeName, new[] {searchInFolders});
            var assetsList = new List<T>();
            if (assets.Length <= 0) return assetsList;
            assetsList.AddRange(assets.Select(asset =>
                AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(asset))));
            return assetsList.AsReadOnly();
        }

        public IReadOnlyList<Object> FindAllAssetsInFolder(string searchInFolder)
        {
            var assets = Directory.GetFiles(searchInFolder, "*.*", SearchOption.TopDirectoryOnly);
            var assetsList = new List<Object>();
            if (assets.Length <= 0) return assetsList;

            assetsList.AddRange(from asset in assets
                let subStrings = asset.Split('.')
                let fileExtension = subStrings[subStrings.Length - 1]
                where !fileExtension.Equals("meta") || !fileExtension.Equals("md")
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

        public bool FindFileAssetFolder(string fileName)
        {
            var info = new DirectoryInfo("./Assets");
            var fileInfo = info.GetFiles();
            return fileInfo.Any(file => file.Name.Equals(fileName));
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
    }
}