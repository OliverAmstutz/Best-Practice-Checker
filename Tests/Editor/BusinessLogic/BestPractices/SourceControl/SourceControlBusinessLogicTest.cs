using System;
using System.Collections.Generic;
using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.SourceControl;
using BestPracticeChecker.Editor.BusinessLogic.PackageUtility;
using BestPracticeChecker.Tests.Editor.BusinessLogic.AssetsProvider;
using BestPracticeChecker.Tests.Editor.BusinessLogic.PackageUtility;
using NUnit.Framework;
using Object = UnityEngine.Object;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.SourceControl
{
    public sealed class SourceControlBusinessLogicTest
    {
        [Test]
        public void TestEvaluateGitOk()
        {
            var bL = new SourceControlBusinessLogic(new AssetsProviderStub(new List<Object>(), true, "notUsed", null), new PackageUtilityStub(true, true, PackageStatus.Outdated),
                new VersionControlStatusStub(UnityVersionControl.VisibleMetaFiles));
            bL.Evaluation();
            Assert.That(bL.GetStatus() == Status.Ok);
            Assert.That(bL.Result().Status() == SourceControlStatus.GitOk);
            Assert.IsFalse(bL.CanBeFixed());
        }

        [Test]
        public void TestEvaluateGitVersionControlSetting()
        {
            var bL = new SourceControlBusinessLogic(new AssetsProviderStub(new List<Object>(), true, "notUsed", null), new PackageUtilityStub(true, true, PackageStatus.Outdated),
                new VersionControlStatusStub(UnityVersionControl.PlasticSCM));
            bL.Evaluation();
            Assert.That(bL.GetStatus() == Status.Warning);
            Assert.That(bL.Result().Status() == SourceControlStatus.GitVersionControlSetting);
            Assert.IsFalse(bL.CanBeFixed());
        }

        [Test]
        public void TestEvaluateUnityVersionControlOkAndVersionControlSetting()
        {
            var bL = new SourceControlBusinessLogic(new AssetsProviderStub(new List<Object>(), false, "notUsed", null), new PackageUtilityStub(true, true, PackageStatus.UpToDate),
                new VersionControlStatusStub(UnityVersionControl.VisibleMetaFiles));
            bL.Evaluation();
            Assert.That(bL.GetStatus() == Status.Warning);
            Assert.That(bL.Result().Status() == SourceControlStatus.UnityVersionControlOkAndVersionControlSetting);
            Assert.IsFalse(bL.CanBeFixed());
        }

        [Test]
        public void TestEvaluateUnityVersionControlOk()
        {
            var bL = new SourceControlBusinessLogic(new AssetsProviderStub(new List<Object>(), false, "notUsed", null), new PackageUtilityStub(true, true, PackageStatus.UpToDate),
                new VersionControlStatusStub(UnityVersionControl.Perforce));
            bL.Evaluation();
            Assert.That(bL.GetStatus() == Status.Ok);
            Assert.That(bL.Result().Status() == SourceControlStatus.UnityVersionControlOk);
            Assert.IsFalse(bL.CanBeFixed());
        }

        [Test]
        public void TestEvaluateUnityVersionControlOutdatedAndVersionControlSetting()
        {
            var bL = new SourceControlBusinessLogic(new AssetsProviderStub(new List<Object>(), false, "notUsed", null), new PackageUtilityStub(true, true, PackageStatus.Outdated),
                new VersionControlStatusStub(UnityVersionControl.VisibleMetaFiles));
            bL.Evaluation();
            Assert.That(bL.GetStatus() == Status.Warning);
            Assert.That(bL.Result().Status() == SourceControlStatus.UnityVersionControlOutdatedAndVersionControlSetting);
            Assert.IsTrue(bL.CanBeFixed());
        }

        [Test]
        public void TestEvaluateUnityVersionControlOutdated()
        {
            var bL = new SourceControlBusinessLogic(new AssetsProviderStub(new List<Object>(), false, "notUsed", null), new PackageUtilityStub(true, true, PackageStatus.Outdated),
                new VersionControlStatusStub(UnityVersionControl.Perforce));
            bL.Evaluation();
            Assert.That(bL.GetStatus() == Status.Warning);
            Assert.That(bL.Result().Status() == SourceControlStatus.UnityVersionControlOutdated);
            Assert.IsTrue(bL.CanBeFixed());
        }

        [Test]
        public void TestEvaluateNoSourceControl()
        {
            var bL = new SourceControlBusinessLogic(new AssetsProviderStub(new List<Object>(), false, "notUsed", null),
                new PackageUtilityStub(true, true, PackageStatus.NotInstalled), new VersionControlStatusStub(UnityVersionControl.VisibleMetaFiles));
            bL.Evaluation();
            Assert.That(bL.GetStatus() == Status.Error);
            Assert.That(bL.Result().Status() == SourceControlStatus.NoSourceControl);
            Assert.IsFalse(bL.CanBeFixed());
        }

        [Test]
        public void TestFixFalse()
        {
            var bL = new SourceControlBusinessLogic(new AssetsProviderStub(new List<Object>(), true, "notUsed", null), new PackageUtilityStub(true, true, PackageStatus.Outdated),
                new VersionControlStatusStub(UnityVersionControl.VisibleMetaFiles));
            bL.Evaluation();
            Assert.Throws<InvalidOperationException>(bL.Fix);
        }

        [Test]
        public void TestFixTrue()
        {
            var bL = new SourceControlBusinessLogic(new AssetsProviderStub(new List<Object>(), false, "notUsed", null), new PackageUtilityStub(true, true, PackageStatus.Outdated),
                new VersionControlStatusStub(UnityVersionControl.HiddenMetaFiles));
            bL.Evaluation();
            Assert.DoesNotThrow(bL.Fix);
        }
    }
}