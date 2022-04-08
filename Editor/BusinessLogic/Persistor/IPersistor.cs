namespace BestPracticeChecker.Editor.BusinessLogic.Persistor
{
    /// <summary>
    ///     Persists data by save and load mechanism.
    /// </summary>
    public interface IPersistor
    {
        /// <summary>
        ///     Saves value of type T under given key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        void Save<T>(string key, T value);

        /// <summary>
        ///     Loads value under Key, or if not available return default type.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Load<T>(string key, T defaultValue);
    }
}