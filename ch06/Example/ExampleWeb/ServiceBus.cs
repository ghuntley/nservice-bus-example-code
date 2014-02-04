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
                    .DefiningCommandsAs(t => IsFromMessageAssembly(t) && t.Namespace.EndsWith("Commands"))
                    .DefiningEventsAs(t => IsFromMessageAssembly(t) && t.Namespace.EndsWith("Events"))
                    .DefiningMessagesAs(t => IsFromMessageAssembly(t) && t.Namespace.EndsWith("InternalMessages"))
					.DefaultBuilder()
					.UseTransport<Msmq>()
					.PurgeOnStartup(true)
					.UnicastBus()
					.CreateBus()
					.Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());
			}
		}

        private static bool IsFromMessageAssembly(Type t)
        {
            return t.Namespace != null
                && !t.Namespace.StartsWith("NServiceBus.")
                && !t.Namespace.StartsWith("System.");
        }
	}
}
