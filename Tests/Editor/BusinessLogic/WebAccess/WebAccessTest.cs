using System;
using BestPracticeChecker.Editor.BusinessLogic.BestPractices;
using NUnit.Framework;

namespace BestPracticeChecker.Tests.Editor.BusinessLogic.WebAccess
{
    public class WebAccessTest
    {
        [Test]
        public void IntegrationTestOpenDocumentation()
        {
            var webAccess = new BestPracticeChecker.Editor.BusinessLogic.WebAccess.WebAccess();
            try
            {
                webAccess.OpenDocumentation(BestPracticeName.Test_Framework);
            }
            catch (Exception e)
            {
                Assert.Null(e);
                return;
            }
            Assert.True(true);
        }
    }
}