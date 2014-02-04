using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using MyMessages.Commands;

namespace MyService
{
	public class StartAndStop : IWantToRunWhenBusStartsAndStops
	{
		public IBus Bus { get; set; }

		public void Start()
		{
			Console.WriteLine("Type 'send' to send a message.");

			while (true)
			{
				string input = Console.ReadLine();
				if (input == null)
					return;
				if (input == "send")
				{
					// Bus.SendLocal is usually used as a way to break up a transport message
					// containing many logical messages into separate transport messages
					// so that each can be processes under its own transaction. We're abusing
					// it slightly here to quickly send a message to ourselves without
					// needing to configure any message routing in the App.config file.
					Bus.SendLocal(new MyCommand());
				}
			}
		}

		public void Stop()
		{
		}
	}
}
