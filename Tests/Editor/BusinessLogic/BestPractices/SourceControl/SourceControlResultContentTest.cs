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
            Assert.That(result.Content()
                .Contains(
                    "You use git without the appropriate Version Control settings. Turn Version Control mode to \"Visible Meta Files\" in the Project Settings."));
        }

        [Test]
        public void TestUnityVersionControlOk()
        {
            var result = new SourceControlResultContent();
            result.Status(SourceControlStatus.UnityVersionControlOk);
            Assert.That(result.Content()
                .Contains(
                    "Unity Version Control is used. Resources in the documentation recommend to use Git instead of Unity version control system."));
        }

        [Test]
        public void TestUnityVersionControlOkAndVersionControlSetting()
        {
            var result = new SourceControlResultContent();
            result.Status(SourceControlStatus.UnityVersionControlOkAndVersionControlSetting);
            Assert.That(result.Content()
                .Contains(
                    "The Version Control mode is not configured appropriately. Turn Version Control mode to \"Perforce\" in the Project Settings."));
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
            Assert.That(result.Content()
                .Contains("In addition, the Version Control mode is not configured appropriately."));
        }


        [Test]
        public void TestNoSourceControl()
        {
            var result = new SourceControlResultContent();
            result.Status(SourceControlStatus.NoSourceControl);
            Assert.That(result.Content()
                .Contains(
                    "You use no source control! It is highly recommended to use Git! Find resources or tutorials in this best practice documentation."));
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