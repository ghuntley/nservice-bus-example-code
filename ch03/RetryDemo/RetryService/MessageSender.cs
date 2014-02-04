using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus.Unicast;
using NServiceBus;
using RetryService.Messages.Commands;

namespace RetryService
{
	public class MessageSender : IWantToRunWhenBusStartsAndStops
	{
		public IBus Bus { get; set; }

		public void Start()
		{
			Console.WriteLine("Type GoBoom to send a message that will cause an error.");
			while (true)
			{
				if(String.Equals(Console.ReadLine(), "GoBoom", StringComparison.OrdinalIgnoreCase))
					Bus.Send(new GoBoomCmd { UniqueId = Guid.NewGuid() });
			}
		}

		public void Stop()
		{
		}
	}
}
