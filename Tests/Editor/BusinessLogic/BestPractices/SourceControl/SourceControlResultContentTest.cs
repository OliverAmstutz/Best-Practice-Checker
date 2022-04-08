using BestPracticeChecker.Editor.BusinessLogic.BestPractices.SourceControl;
using NUnit.Framework;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.SourceControl
{
    public class SourceControlResultContentTest
    {
        [Test]
        public void TestGitOk()
        {
            var result = new SourceControlResultContent();
            result.Status(SourceControlStatus.GitOk);
            Assert.That(result.Content().Contains("Excellent, you use Git for source control"));
        }

        [Test]
        public void TestGitVersionControlSetting()
        {
            var result = new SourceControlResultContent();
            result.Status(SourceControlStatus.GitVersionControlSetting);
            Assert.That(result.Content().Contains("It seems, Git is used for Source control."));
        }

        [Test]
        public void TestUnityVersionControlOk()
        {
            var result = new SourceControlResultContent();
            result.Status(SourceControlStatus.UnityVersionControlOk);
            Assert.That(result.Content().Contains("Unity Version Control is used."));
        }

        [Test]
        public void TestUnityVersionControlOkAndVersionControlSetting()
        {
            var result = new SourceControlResultContent();
            result.Status(SourceControlStatus.UnityVersionControlOkAndVersionControlSetting);
            Assert.That(result.Content().Contains("The Version Control mode is not set to Unity Version control."));
        }


        [Test]
        public void TestUnityVersionControlOutdated()
        {
            var result = new SourceControlResultContent();
            result.Status(SourceControlStatus.UnityVersionControlOutdated);
            Assert.That(result.Content().Contains("Your Unity Version Control package is outdated, press"));
        }


        [Test]
        public void TestUnityVersionControlOutdatedAndVersionControlSetting()
        {
            var result = new SourceControlResultContent();
            result.Status(SourceControlStatus.UnityVersionControlOutdatedAndVersionControlSetting);
            Assert.That(result.Content().Contains("In addition, the Version Control mode is not set to U"));
        }


        [Test]
        public void TestNoSourceControl()
        {
            var result = new SourceControlResultContent();
            result.Status(SourceControlStatus.NoSourceControl);
            Assert.That(result.Content().Contains("You use no source control! It is highly recomme"));
        }
        
        
        [Test]
        public void TestDefault()
        {
            var result = new SourceControlResultContent();
            result.Status(SourceControlStatus.NotInitialised);
            Assert.That(result.Content().Contains("Something went wrong in the source control initialization!"));
        }
    }
}