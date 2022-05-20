using System.ComponentModel;
using BestPracticeChecker.Editor.BusinessLogic.PackageUtility;
using NUnit.Framework;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.PackageUtility
{
    public sealed class EvaluateVersionControlStatusTest
    {
        [Test]
        public void TestEvaluateHiddenMetaFiles()
        {
            var uvc = new VersionControlStatus();
            Assert.IsTrue(uvc.Evaluate("Hidden Meta Files") == UnityVersionControl.HiddenMetaFiles);
        }

        [Test]
        public void TestEvaluateVisibleMetaFiles()
        {
            var uvc = new VersionControlStatus();
            Assert.IsTrue(uvc.Evaluate("Visible Meta Files") == UnityVersionControl.VisibleMetaFiles);
        }

        [Test]
        public void TestEvaluatePerforce()
        {
            var uvc = new VersionControlStatus();
            Assert.IsTrue(uvc.Evaluate("Perforce") == UnityVersionControl.Perforce);
        }

        [Test]
        public void TestEvaluatePlasticSCM()
        {
            var uvc = new VersionControlStatus();
            Assert.IsTrue(uvc.Evaluate("Plastic SCM") == UnityVersionControl.PlasticSCM);
        }

        [Test]
        public void TestEvaluateInvalidString()
        {
            var uvc = new VersionControlStatus();
            try
            {
                uvc.Evaluate("Invalid Mode Input string");
            }
            catch (InvalidEnumArgumentException e)
            {
                Assert.IsNotNull(e);
            }
        }
    }
}