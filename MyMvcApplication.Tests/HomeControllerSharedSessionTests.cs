using MvcIntegrationTestFramework.Browsing;
using MvcIntegrationTestFramework.Hosting;
using NUnit.Framework;

namespace MyMvcApplication.Tests
{
	[TestFixture]
	public class HomeControllerSharedSessionTests
	{
		private AppHost appHost;

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
            //If you MVC project is not in the root of your solution directory then include the path
            //e.g. AppHost.Simulate("Website\MyMvcApplication")
			appHost = AppHost.Simulate("MyMvcApplication");
			appHost.StartBrowsingSession();

			//login
			appHost.Start(browsingSession =>
			{
				//follow redirection to logon page
				var loginRedirectUrl = "/Account/LogOn";
				string loginFormResponseText = browsingSession.Get(loginRedirectUrl).ResponseText;
				string suppliedAntiForgeryToken = MvcUtils.ExtractAntiForgeryToken(loginFormResponseText);

				// Now post the login form, including the verification token
				browsingSession.Post(loginRedirectUrl, new
				{
					UserName = "steve",
					Password = "secret",
					__RequestVerificationToken = suppliedAntiForgeryToken
				});
			});
		}

		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			appHost.EndBrowsingSession();
		}

		[Test]
		public void ShouldLoadSecuredAction()
		{
			string securedActionUrl = "/home/SecretAction";

			appHost.Start(browsingSession =>
			{
				RequestResult afterLoginResult = browsingSession.Get(securedActionUrl);
				Assert.AreEqual("Hello, you're logged in as steve", afterLoginResult.ResponseText);
			});
		}

		[Test]
		public void ShouldLoadSecuredAction2()
		{
			string securedActionUrl = "/home/SecretAction2";

			appHost.Start(browsingSession =>
			{
				RequestResult afterLoginResult = browsingSession.Get(securedActionUrl);
				Assert.AreEqual("SecretAction2 - Hello, you're logged in as steve", afterLoginResult.ResponseText);
			});
		}

		[Test]
		public void ShouldRedirectToLogonWhenSessionIsKilled()
		{
			string securedActionUrl = "/home/SecretAction2";

			appHost.EndBrowsingSession();
			appHost.Start(browsingSession =>
			{
				RequestResult initialRequestResult = browsingSession.Get(securedActionUrl);
				string loginRedirectUrl = initialRequestResult.Response.RedirectLocation;
				Assert.IsTrue(loginRedirectUrl.StartsWith("/Account/LogOn"), "Didn't redirect to logon page");
			});
		}
	}
}
