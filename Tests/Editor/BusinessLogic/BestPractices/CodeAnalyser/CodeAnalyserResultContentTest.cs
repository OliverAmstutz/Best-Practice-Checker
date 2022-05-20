using BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeAnalyser;
using NUnit.Framework;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.BestPractices.CodeAnalyser
{
    public sealed class CodeAnalyserResultContentTest
    {
        [Test]
        public void TestCodeAnalyserOk()
        {
            var result = new CodeAnalyserResultContent();
            result.Status(CodeAnalyserStatus.SetupOk);
            Assert.That(result.Content().Contains("Great, your code analyser is ready to use!"));
        }

        [Test]
        public void TestCodeAnalyserWarning()
        {
            var result = new CodeAnalyserResultContent();
            result.Status(CodeAnalyserStatus.SetupMisconfigured);
            Assert.That(result.Content().Contains("Your code analyser is misconfigured."));
        }

        [Test]
        public void TestCodeAnalyserErrorNotSupportedIde()
        {
            var result = new CodeAnalyserResultContent();
            result.Status(CodeAnalyserStatus.NotSupportedIde);
            Assert.That(result.Content().Contains("Your IDE is not officially supported by Unity and the best practice code analyser."));
        }


        [Test]
        public void TestCodeAnalyserError()
        {
            var result = new CodeAnalyserResultContent();
            result.Status(CodeAnalyserStatus.NoCodeAnalyser);
            Assert.That(result.Content().Contains("You use no code analyser. It is strongly recommended"));
        }


        [Test]
        public void TestDefault()
        {
            var result = new CodeAnalyserResultContent();
            result.Status(CodeAnalyserStatus.NotInitialised);
            Assert.That(result.Content().Contains("Something went wrong in the code analyser initialization!"));
        }
    }
}