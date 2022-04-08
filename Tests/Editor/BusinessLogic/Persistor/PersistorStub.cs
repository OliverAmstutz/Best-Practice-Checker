using BestPracticeChecker.Editor.BusinessLogic.Persistor;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.Persistor
{
    public class PersistorStub : IPersistor
    {
        public void Save<T>(string key, T value)
        {
            //no implementation necessary
        }

        public T Load<T>(string key, T defaultValue)
        {
            return defaultValue;
        }
    }
}