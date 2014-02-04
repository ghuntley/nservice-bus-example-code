using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace RetryService.Messages.Commands
{
	public class GoBoomCmd : ICommand
	{
		public Guid UniqueId { get; set; }
	}
}
