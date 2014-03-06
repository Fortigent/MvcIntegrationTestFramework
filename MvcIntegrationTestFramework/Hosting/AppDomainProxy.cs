using System;
using MvcIntegrationTestFramework.Browsing;

namespace MvcIntegrationTestFramework.Hosting
{
    /// <summary>
    /// Simply provides a remoting gateway to execute code within the ASP.NET-hosting appdomain
    /// </summary>
    internal class AppDomainProxy : MarshalByRefObject
    {
			private BrowsingSession _browsingSession;

        public void RunCodeInAppDomain(Action codeToRun)
        {
            codeToRun();
        }

				public void RunBrowsingSessionInAppDomain(SerializableDelegate<Action<BrowsingSession>> script)
        {
						script.Delegate(_browsingSession ?? new BrowsingSession());
        }

        public override object InitializeLifetimeService()
        {
            return null; // Tells .NET not to expire this remoting object
        }

				public void StartBrowsingSession()
				{
					_browsingSession = new BrowsingSession();
				}

				public void EndBrowsingSession()
				{
					_browsingSession = null;
				}
    }
}