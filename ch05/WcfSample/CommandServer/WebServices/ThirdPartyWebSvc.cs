using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandServer.Messages.Commands;
using NServiceBus;

namespace CommandServer.WebServices
{
	public class ThirdPartyWebSvc : WcfService<ThirdPartyCmd, ThirdPartyCmdResponse>
	{
	}
}
