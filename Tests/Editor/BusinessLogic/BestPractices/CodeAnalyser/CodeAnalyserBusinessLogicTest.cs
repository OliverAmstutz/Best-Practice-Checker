using System.Collections.Generic;
using System.IO;
using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.AssetsProvider;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeAnalyser;
using BestPracticeChecker.Editor.BusinessLogic.PreferenceUtility;
using BestPracticeChecker.Tests.Editor.BusinessLogic.AssetsProvider;
using BestPracticeChecker.Tests.Editor.BusinessLogic.PreferenceUtility;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.CodeAnalyser
{
    public sealed class CodeAnalyserBusinessLogicTest
    {
        [Test]
        public void IntegrationTestEvaluateOk()
        {
            var bestPracticeChecker =
                (DefaultAsset) new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider().FindAssetOfNameAndFileExtension("BestPracticeChecker", "dll");
            AssetDatabase.SetLabels(bestPracticeChecker, new[] {"RoslynAnalyzer"});
            AssetDatabase.Refresh();
            var assets = new List<Object> {bestPracticeChecker};
            var bL = new CodeAnalyserBusinessLogic(new AssetsProviderStub(assets, false, "notUsed", bestPracticeChecker), new PreferencesStub(Ide.Rider));
            bL.Evaluation();
            Assert.That(bL.GetStatus() == Status.Ok);
            Assert.IsFalse(bL.CanBeFixed());
            AssetDatabase.SetLabels(bestPracticeChecker, new[] {""});
            AssetDatabase.Refresh();
        }

        [Test]
        public void IntegrationTestEvaluateWarning()
        {
            var bestPracticeChecker =
                (DefaultAsset) new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider().FindAssetOfNameAndFileExtension("BestPracticeChecker", "dll");
            var assets = new List<Object> {bestPracticeChecker};
            var bL = new CodeAnalyserBusinessLogic(new AssetsProviderStub(assets, false, "notUsed", bestPracticeChecker), new PreferencesStub(Ide.VisualStudio));
            bL.Evaluation();
            Assert.That(bL.GetStatus() == Status.Warning);
            Assert.IsTrue(bL.CanBeFixed());
        }


        [Test]
        public void IntegrationTestEvaluateErrorNotSupportedIde()
        {
            var bL = new CodeAnalyserBusinessLogic(new AssetsProviderStub(new List<Object>(), false, "notUsed", null), new PreferencesStub(Ide.Unknown));
            bL.Evaluation();
            Assert.That(bL.GetStatus() == Status.Error);
            Assert.IsFalse(bL.CanBeFixed());
        }

        [Test]
        public void IntegrationTestEvaluateError()
        {
            var bL = new CodeAnalyserBusinessLogic(new AssetsProviderStub(new List<Object>(), false, "notUsed", null), new PreferencesStub(Ide.VisualStudio));
            bL.Evaluation();
            Assert.That(bL.GetStatus() == Status.Error);
            Assert.IsTrue(bL.CanBeFixed());
        }

        [Test]
        public void IntegrationTestFixMisConfig()
        {
            var bL = new CodeAnalyserBusinessLogic();
            var bestPracticeChecker =
                (DefaultAsset) new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider().FindAssetOfNameAndFileExtension("BestPracticeChecker", "dll");
            var defaultAssetImporter = AssetImportProvider.ImporterForPlugin(bestPracticeChecker);
            defaultAssetImporter.SetCompatibleWithAnyPlatform(true);
            bL.Evaluation();
            Assert.DoesNotThrow(bL.Fix);
            var updatedBpc =
                (DefaultAsset) new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider().FindAssetOfNameAndFileExtension("BestPracticeChecker", "dll");
            var updatedImporter = AssetImportProvider.ImporterForPlugin(updatedBpc);
            Assert.IsFalse(updatedImporter.GetCompatibleWithAnyPlatform());
            AssetDatabase.SetLabels(updatedBpc, new[] {""});
            AssetDatabase.Refresh();
        }

        [Test]
        public void IntegrationTestFixSetup()
        {
            var bL = new CodeAnalyserBusinessLogic(new AssetsProviderStub(new List<Object>(), false, "notUsed", null), new PreferencesStub(Ide.VisualStudio));
            bL.Fix();
            AssetDatabase.Refresh();
            const string expectedPath = "Assets/Editor/Plugin";
            Assert.IsTrue(AssetDatabase.IsValidFolder(expectedPath));
            Assert.IsNotEmpty(new BestPracticeChecker.Editor.BusinessLogic.AssetsProvider.AssetsProvider().FindAllAssetsInFolder(expectedPath));
            AssetDatabase.Refresh();
            Assert.IsTrue(AssetDatabase.DeleteAsset("Assets/Editor/Plugin/BestPracticeChecker.dll"));
            AssetDatabase.Refresh();
            Cleanup();
        }

        private void Cleanup()
        {
            if (!Directory.Exists("Assets/Editor/Plugin")) return;
            FileUtil.DeleteFileOrDirectory("Assets/Editor/Plugin.meta");
            FileUtil.DeleteFileOrDirectory("Assets/Editor/Plugin");
            FileUtil.DeleteFileOrDirectory("Assets/Editor.meta");
            FileUtil.DeleteFileOrDirectory("Assets/Editor");
            AssetDatabase.Refresh();
        }


        [Test]
        public void TestResult()
        {
            var bL = new CodeAnalyserBusinessLogic();
            Assert.IsNull(bL.Result());
        }
    }
}