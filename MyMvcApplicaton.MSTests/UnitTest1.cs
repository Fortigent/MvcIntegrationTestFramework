using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcIntegrationTestFramework.Browsing;
using MvcIntegrationTestFramework.Hosting;
using System.Web.Mvc;

namespace MyMvcApplicaton.MSTests
{
    [TestClass]
    public class UnitTest1
    {
        private AppHost appHost;

        //[TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            //If you MVC project is not in the root of your solution directory then include the path
            //e.g. AppHost.Simulate("Website\MyMvcApplication")
            appHost = AppHost.Simulate("MyMvcApplication");
        }

        [TestMethod]
        public void Root_Url_Renders_Index_View()
        {
            TestFixtureSetUp();

            appHost.Start(browsingSession =>
            {
                // Request the root URL
                RequestResult result = browsingSession.Get("");

                // Can make assertions about the ActionResult...
                var viewResult = (ViewResult)result.ActionExecutedContext.Result;
                Assert.AreEqual("Index", viewResult.ViewName);
                Assert.AreEqual("Welcome to ASP.NET MVC!", viewResult.ViewData["Message"]);

                // ... or can make assertions about the rendered HTML
                Assert.IsTrue(result.ResponseText.Contains("<!DOCTYPE html"));
            });
        }
    }
}
