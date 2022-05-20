using System;
using System.Collections.Generic;
using BestPracticeChecker.Editor.BusinessLogic;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices.NoAssetsInRoot;
using BestPracticeChecker.Tests.Editor.BusinessLogic.AssetsProvider;
using NUnit.Framework;
using Object = UnityEngine.Object;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.NoAssetsInRoot
{
    public sealed class NoAssetsInRootBusinessLogicTest
    {
        [Test]
        public void TestEvaluationOk()
        {
            var bL = new NoAssetsInRootBusinessLogic("Assets", new AssetsProviderStub(new List<Object>(), true, ".notUsed", null));
            bL.Evaluation();
            Assert.That(bL.GetStatus() == Status.Ok);
            Assert.IsFalse(bL.CanBeFixed());
        }

        [Test]
        public void TestEvaluationWarning()
        {
            var bL = new NoAssetsInRootBusinessLogic("Assets", new AssetsProviderStub(new List<Object> {new Object()}, true, ".notUsed", null));
            bL.Evaluation();
            Assert.That(bL.GetStatus() == Status.Warning);
            Assert.IsFalse(bL.CanBeFixed());
        }


        [Test]
        public void TestResult()
        {
            var bL = new NoAssetsInRootBusinessLogic();
            Assert.IsEmpty(bL.Result().MisplacedAssetsPaths());
            Assert.That(bL.Result().Content().Contains("Something went wrong in the "));
        }


        [Test]
        public void TestFix()
        {
            Assert.Throws<NotImplementedException>(new NoAssetsInRootBusinessLogic().Fix);
        }
    }
}