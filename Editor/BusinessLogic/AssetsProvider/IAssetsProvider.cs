using System.Collections.Generic;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.AssetsProvider
{
    /// <summary>
    /// Provides Asset operating methods.
    ///     Supports the following syntax:
    ///     't:type' syntax (e.g 't:Texture2D' will show Texture2D objects)
    ///     'l:assetLabel' syntax (e.g 'l:architecture' will show assets with AssetLabel 'architecture')
    ///     'ref[:id]:path' syntax (e.g 'ref:1234' will show objects that references the object with instanceID 1234)
    ///     'v:versionState' syntax (e.g 'v:modified' will show objects that are modified locally)
    ///     's:softLockState' syntax (e.g 's:inprogress' will show objects that are modified by anyone (except you))
    ///     'a:area' syntax (e.g 'a:all' will s search in all assets, 'a:assets' will s search in assets folder only and
    ///     'a:packages' will s search in packages folder only)
    ///     'glob:path' syntax (e.g 'glob:Assets/**/*.{png|PNG}' will show objects in any subfolder with name ending by .png or
    ///     .PNG)
    /// </summary>
    public interface IAssetsProvider
    {
        /// <summary>
        /// Returns a Collection of assets of type in given folder.
        /// </summary>
        /// <param name="searchInFolders"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IReadOnlyList<T> FindAllAssetsOfType<T>(string searchInFolders) where T : Object;
        
        /// <summary>
        /// Return boolean of the file exists within the assets folder.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool FindFileAssetFolder(string fileName);
        
        /// <summary>
        /// Return boolean of a specific folder exists within the path.
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool FindFolderFromStartPath(string folderName, string path);
    }
}