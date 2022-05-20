using System;
using System.Collections.Generic;
using System.Linq;
using BestPracticeChecker.Editor.BusinessLogic.AssetsProvider;
using UnityEditor;
using Object = UnityEngine.Object;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.NoAssetsInRoot
{
    public sealed class NoAssetsInRootBusinessLogic : IBusinessLogic<NoAssetsInRootResultContent>
    {
        private const string Root = "Assets";
        private const bool _canBeFixed = false;
        private readonly IAssetsProvider _assetsProvider;
        private readonly string _rootFolder;
        private IReadOnlyList<Object> _misplacedAssets;
        private NoAssetsInRootResultContent _result = new NoAssetsInRootResultContent();

        private Status _status;

        public NoAssetsInRootBusinessLogic() : this(Root, new AssetsProvider.AssetsProvider())
        {
        }

        public NoAssetsInRootBusinessLogic(string rootFolder, IAssetsProvider assetsProvider)
        {
            _rootFolder = rootFolder;
            _assetsProvider = assetsProvider;
            _misplacedAssets = new List<Object>().AsReadOnly();
        }

        public void Evaluation()
        {
            _misplacedAssets = _assetsProvider.FindAllAssetsInFolder(_rootFolder);
            _result = new NoAssetsInRootResultContent();
            _status = _misplacedAssets.Count > 0 ? Status.Warning : Status.Ok;

            _misplacedAssets.ToList().ForEach(obj => _result.AddMisplacedAssetsPath(AssetDatabase.GetAssetPath(obj)));

            _result.Status(_status);
        }

        public bool CanBeFixed()
        {
            return _canBeFixed;
        }

        public NoAssetsInRootResultContent Result()
        {
            return _result;
        }

        public Status GetStatus()
        {
            return _status;
        }

        public void Fix()
        {
            throw new NotImplementedException("This method is not used in NoAssetsInRootBusinessLogic");
        }
    }
}