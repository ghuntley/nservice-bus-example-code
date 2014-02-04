using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandServer.Messages.Commands;
using NServiceBus;
using NServiceBus.Logging;

namespace CommandServer.Handlers
{
	public class ThirdPartyHandler : IHandleMessages<ThirdPartyCmd>
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(ThirdPartyHandler));

		public IBus Bus { get; set; }

		public void Handle(ThirdPartyCmd message)
		{
			log.InfoFormat("Received ThirdPartyCmd with Contents=\"{0}\"", message.Contents);

			if (String.Equals(message.Contents, "OK", StringComparison.OrdinalIgnoreCase))
				Bus.Return<ThirdPartyCmdResponse>(ThirdPartyCmdResponse.OK);
			else
				Bus.Return<ThirdPartyCmdResponse>(ThirdPartyCmdResponse.Rejected);
		}
	}
}
