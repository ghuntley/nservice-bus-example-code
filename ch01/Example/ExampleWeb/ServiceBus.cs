using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NServiceBus;
using NServiceBus.Installation.Environments;

namespace ExampleWeb
{
	public static class ServiceBus
	{
		public static IBus Bus { get; private set; }

		public static void Init()
		{
			if (Bus != null)
				return;

			lock (typeof(ServiceBus))
			{
				if (Bus != null)
					return;

				Bus = Configure.With()
					.DefineEndpointName("ExampleWeb")
					.DefaultBuilder()
					.UseTransport<Msmq>()
					.PurgeOnStartup(true)
					.UnicastBus()
					.CreateBus()
					.Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());
			}
		}
	}
}
