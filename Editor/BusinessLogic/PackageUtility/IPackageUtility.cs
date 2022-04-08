namespace BestPracticeChecker.Editor.BusinessLogic.PackageUtility
{
    /// <summary>
    /// Operations on the unity package manager.
    /// </summary>
    public interface IPackageUtility
    {
        /// <summary>
        /// Returns the status of enquired package.
        /// </summary>
        /// <param name="packageName"></param>
        /// <returns></returns>
        public PackageStatus StatusOfPackage(string packageName);

        /// <summary>
        /// Returns boolean of package existence within the project.
        /// </summary>
        /// <param name="packageName"></param>
        /// <returns></returns>
        public bool PackageExists(string packageName);

        /// <summary>
        /// Returns boolean of package is up to date.
        /// </summary>
        /// <param name="packageName"></param>
        /// <returns></returns>
        public bool IsUpToDate(string packageName);

        /// <summary>
        /// Installs the latest package.
        /// </summary>
        /// <param name="packageName"></param>
        public void InstallLatestPackage(string packageName);

        /// <summary>
        /// Removes the package.
        /// </summary>
        /// <param name="packageName"></param>
        public void RemovePackage(string packageName);
    }
}