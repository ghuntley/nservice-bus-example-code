using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandServer.Messages.Commands
{
	public class ThirdPartyCmd
	{
		public string Contents { get; set; }
	}

	public enum ThirdPartyCmdResponse
	{
		OK,
		Rejected
	}
}
