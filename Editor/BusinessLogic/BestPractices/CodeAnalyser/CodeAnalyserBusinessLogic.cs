using System;
using System.IO;
using System.Linq;
using BestPracticeChecker.Editor.BusinessLogic.AssetsProvider;
using BestPracticeChecker.Editor.BusinessLogic.PreferenceUtility;
using UnityEditor;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeAnalyser
{
    public sealed class CodeAnalyserBusinessLogic : IBusinessLogic<CodeAnalyserResultContent>
    {
        private const string RoslynAnalyzerLabel = "RoslynAnalyzer";
        private const string RootPath = "Assets";
        private const string DefaultEditorPath = "Editor";
        private const string DefaultPluginPath = "Plugin";

        private readonly IAssetsProvider _assetProvider;
        private readonly IPreferences _preferences;
        private bool _canBeFixed;
        private CodeAnalyserResultContent _result;
        private Status _status;

        public CodeAnalyserBusinessLogic() : this(new AssetsProvider.AssetsProvider(), new Preferences())
        {
        }

        public CodeAnalyserBusinessLogic(IAssetsProvider assetsProvider, IPreferences preferences)
        {
            _assetProvider = assetsProvider;
            _preferences = preferences;
        }

        public void Evaluation()
        {
            _result = new CodeAnalyserResultContent();
            if (_assetProvider == null) throw new NullReferenceException("AssetsProvider not instantiated");

            if (IdeIsNotSupported())
            {
                SetState(false, Status.Error, CodeAnalyserStatus.NotSupportedIde);
                return;
            }

            var codeAnalyserAsset = (DefaultAsset) _assetProvider.FindAssetOfNameAndFileExtension("BestPracticeChecker", "dll");
            if (codeAnalyserAsset != null)
            {
                if (IsCorrectlySetup(codeAnalyserAsset))
                    SetState(false, Status.Ok, CodeAnalyserStatus.SetupOk);
                else
                    SetState(true, Status.Warning, CodeAnalyserStatus.SetupMisconfigured);
            }
            else
            {
                SetState(true, Status.Error, CodeAnalyserStatus.NoCodeAnalyser);
            }
        }

        public bool CanBeFixed()
        {
            return _canBeFixed;
        }

        public Status GetStatus()
        {
            return _status;
        }

        public CodeAnalyserResultContent Result()
        {
            return _result;
        }

        public void Fix()
        {
            var codeAnalyserAsset = (DefaultAsset) _assetProvider.FindAssetOfNameAndFileExtension("BestPracticeChecker", "dll");
            if (codeAnalyserAsset == null)
                SetupCodeAnalyser();
            else
                ConfigureCodeAnalyser(codeAnalyserAsset);
        }

        private void SetState(bool canBeFixed, Status status, CodeAnalyserStatus codeAnalyserStatus)
        {
            _canBeFixed = canBeFixed;
            _status = status;
            _result.Status(codeAnalyserStatus);
        }

        private void SetupCodeAnalyser()
        {
            if (!AssetDatabase.IsValidFolder(RootPath + "/" + DefaultEditorPath))
                AssetDatabase.CreateFolder(RootPath, DefaultEditorPath);

            if (!AssetDatabase.IsValidFolder(RootPath + "/" + DefaultEditorPath + "/" + DefaultPluginPath))
                AssetDatabase.CreateFolder(RootPath + "/" + DefaultEditorPath, DefaultPluginPath);

            AssetDatabase.Refresh();

            var pathToBpcDll = string.Empty;

            if (File.Exists("Packages/ch.hslu.bestpracticechecker/Editor/UI/Resources/BestPracticeChecker.dll"))
                pathToBpcDll = "Packages/ch.hslu.bestpracticechecker/Editor/UI/Resources/BestPracticeChecker.dll";
            else if (File.Exists("Assets/BestPracticeChecker/Editor/UI/Resources/BestPracticeChecker.dll"))
                pathToBpcDll = "Assets/BestPracticeChecker/Editor/UI/Resources/BestPracticeChecker.dll";
            if (pathToBpcDll != string.Empty &&
                !AssetDatabase.CopyAsset(pathToBpcDll, RootPath + "/" + DefaultEditorPath + "/" + DefaultPluginPath + "/" + "BestPracticeChecker.dll"))
                throw new ArgumentException("Could not copy BestPracticeChecker.dll asset to new path: " + RootPath + "/" + DefaultEditorPath + "/" + DefaultPluginPath + "/" +
                                            "BestPracticeChecker.dll");
            AssetDatabase.Refresh();
            SetLabel((DefaultAsset) _assetProvider.FindAssetOfNameAndFileExtension("BestPracticeChecker", "dll"));
        }

        private bool IdeIsNotSupported()
        {
            var ide = _preferences.UsedIde();
            return !ide.Equals(Ide.Rider) && !ide.Equals(Ide.VisualStudio);
        }

        private bool IsCorrectlySetup(DefaultAsset codeAnalyser)
        {
            if (codeAnalyser == null)
                throw new NullReferenceException("Could not found asset, which should not be possible at this state");

            var hasRoslynAnalyzerLabel = AssetDatabase.GetLabels(codeAnalyser).Contains("RoslynAnalyzer");
            if (!hasRoslynAnalyzerLabel) return false;
            var defaultAssetImporter = AssetImportProvider.ImporterForPlugin(codeAnalyser);
            return !defaultAssetImporter.GetCompatibleWithAnyPlatform();
        }

        private void ConfigureCodeAnalyser(DefaultAsset codeAnalyser)
        {
            SetLabel(codeAnalyser);
            SetCompatibilityWithAnyPlatform(codeAnalyser);
        }

        private static void SetCompatibilityWithAnyPlatform(DefaultAsset bestPracticeChecker)
        {
            var defaultAssetImporter = AssetImportProvider.ImporterForPlugin(bestPracticeChecker);
            defaultAssetImporter.SetCompatibleWithAnyPlatform(false);
        }

        private static void SetLabel(DefaultAsset bestPracticeChecker)
        {
            var currentLabels = AssetDatabase.GetLabels(bestPracticeChecker).ToList();
            currentLabels.Add(RoslynAnalyzerLabel);
            AssetDatabase.SetLabels(bestPracticeChecker, currentLabels.ToArray());
        }
    }
}