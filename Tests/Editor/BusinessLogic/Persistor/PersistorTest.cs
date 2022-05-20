using System;
using NUnit.Framework;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.Persistor
{
    public sealed class PersistorTest
    {
        private const string KeyRoot = "BEST_PRACTICE_CHECKER_PersistorTest_";

        [Test]
        public void TestPersistBoolTrue()
        {
            const bool value = true;
            const string methodName = "TestPersistBoolTrue";
            var p = new BestPracticeChecker.Editor.BusinessLogic.Persistor.Persistor();
            p.Save(KeyRoot + methodName, value);
            Assert.True(p.Load(KeyRoot + methodName, false));
        }

        [Test]
        public void TestPersistBoolFalse()
        {
            const bool value = false;
            const string methodName = "TestPersistBoolFalse";
            var p = new BestPracticeChecker.Editor.BusinessLogic.Persistor.Persistor();
            p.Save(KeyRoot + methodName, value);
            Assert.False(p.Load(KeyRoot + methodName, true));
        }

        [Test]
        public void TestPersistInt()
        {
            const int value = 1234;
            const string methodName = "TestPersistInt";
            var p = new BestPracticeChecker.Editor.BusinessLogic.Persistor.Persistor();
            p.Save(KeyRoot + methodName, value);
            Assert.That(p.Load(KeyRoot + methodName, 0).Equals(value));
        }

        [Test]
        public void TestPersistFloat()
        {
            const float value = -0.387f;
            const string methodName = "TestPersistInt";
            var p = new BestPracticeChecker.Editor.BusinessLogic.Persistor.Persistor();
            p.Save(KeyRoot + methodName, value);
            var result = p.Load(KeyRoot + methodName, 0.0f);
            Assert.True(Math.Abs(result - value) < 0.0001f);
        }

        [Test]
        public void TestPersistString()
        {
            const string value = "my test string";
            const string methodName = "TestPersistInt";
            var p = new BestPracticeChecker.Editor.BusinessLogic.Persistor.Persistor();
            p.Save(KeyRoot + methodName, value);
            Assert.That(p.Load(KeyRoot + methodName, "default test string").Equals(value));
        }


        [Test]
        public void TestSaveInvalidType()
        {
            var p = new BestPracticeChecker.Editor.BusinessLogic.Persistor.Persistor();
            try
            {
                p.Save("whatever", new object());
            }
            catch (ArgumentException e)
            {
                Assert.NotNull(e);
            }
        }

        [Test]
        public void TestLoadInvalidType()
        {
            var p = new BestPracticeChecker.Editor.BusinessLogic.Persistor.Persistor();
            try
            {
                p.Load("not used", new object());
            }
            catch (ArgumentException e)
            {
                Assert.NotNull(e);
            }
        }
    }
}