using BestPracticeChecker.Editor.BusinessLogic.PackageUtility;
using NUnit.Framework;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.PackageUtility
{
    public class PackageUtilityIntegrationTest
    {
        [Test]
        public void IntegrationTestPackageIsInstalledTrue()
        {
            Assert.True(
                new BestPracticeChecker.Editor.BusinessLogic.PackageUtility.PackageUtility().PackageExists(
                    "com.unity.testtools.codecoverage"));
        }

        [Test]
        public void IntegrationTestPackageIsInstalledFalse()
        {
            Assert.False(
                new BestPracticeChecker.Editor.BusinessLogic.PackageUtility.PackageUtility().PackageExists(
                    "com.unity.polybrush"));
        }

        [Test]
        public void IntegrationTestPackageIsInstalledNotExistent()
        {
            Assert.False(
                new BestPracticeChecker.Editor.BusinessLogic.PackageUtility.PackageUtility().PackageExists(
                    "Weird.name.of.nonexisting.package"));
        }

        [Test]
        public void IntegrationTestIsUpToDateTrue()
        {
            Assert.True(
                new BestPracticeChecker.Editor.BusinessLogic.PackageUtility.PackageUtility().IsUpToDate(
                    "com.unity.editorcoroutines"));
        }

        [Test]
        public void IntegrationTestIsUpToDateFalse()
        {
            Assert.False(
                new BestPracticeChecker.Editor.BusinessLogic.PackageUtility.PackageUtility().IsUpToDate(
                    "com.unity.ide.vscode"));
        }

        [Test]
        public void IntegrationTestIsUpToDateNonExistent()
        {
            Assert.False(
                new BestPracticeChecker.Editor.BusinessLogic.PackageUtility.PackageUtility().IsUpToDate(
                    "Weird.name.of.nonexisting.package"));
        }

        [Test]
        public void IntegrationTestPackageStatusUpToDate()
        {
            Assert.That(new BestPracticeChecker.Editor.BusinessLogic.PackageUtility.PackageUtility()
                .StatusOfPackage("com.unity.editorcoroutines")
                .Equals(PackageStatus.UpToDate));
        }

        [Test]
        public void IntegrationTestPackageStatusNotInstalled()
        {
            Assert.That(new BestPracticeChecker.Editor.BusinessLogic.PackageUtility.PackageUtility()
                .StatusOfPackage("com.unity.polybrush").Equals(PackageStatus.NotInstalled));
        }

        [Test]
        public void IntegrationTestPackageStatusOutdated()
        {
            Assert.That(new BestPracticeChecker.Editor.BusinessLogic.PackageUtility.PackageUtility()
                .StatusOfPackage("com.unity.ide.vscode").Equals(PackageStatus.Outdated));
        }

        [Test]
        [Order(1)]
        public void IntegrationTestInstallPackage()
        {
            var packageName = "com.unity.collab-proxy";
            new BestPracticeChecker.Editor.BusinessLogic.PackageUtility.PackageUtility().InstallLatestPackage(
                packageName);
            Assert.True(
                new BestPracticeChecker.Editor.BusinessLogic.PackageUtility.PackageUtility()
                    .PackageExists(packageName));
        }

        [Test]
        [Order(2)]
        public void IntegrationTestRemovePackage()
        {
            var packageName = "com.unity.collab-proxy";
            new BestPracticeChecker.Editor.BusinessLogic.PackageUtility.PackageUtility().RemovePackage(packageName);
            Assert.False(
                new BestPracticeChecker.Editor.BusinessLogic.PackageUtility.PackageUtility()
                    .PackageExists(packageName));
        }
    }
}