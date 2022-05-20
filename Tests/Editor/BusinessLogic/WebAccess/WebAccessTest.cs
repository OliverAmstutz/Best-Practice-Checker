using System;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using BestPracticeChecker.Editor.BusinessLogic.WebAccess;
using NUnit.Framework;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.WebAccess
{
    public sealed class WebAccessTest
    {
        [Test]
        public void IntegrationTestOpenDocumentation()
        {
            IWebAccess webAccess = new BestPracticeChecker.Editor.BusinessLogic.WebAccess.WebAccess();
            try
            {
                webAccess.OpenDocumentation(BestPracticeName.Test_Framework);
            }
            catch (Exception e)
            {
                Assert.Null(e);
            }

            Assert.True(true);
        }
    }
}