using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NServiceBus;
using UserService.Messages.Events;
using ExampleWeb.Controllers;

namespace ExampleWeb.MessageHandlers
{
	public class RecentlyCreatedUserWatcher : IHandleMessages<IUserCreatedEvent>
	{
		public void Handle(IUserCreatedEvent message)
		{
			string result = String.Format("{0}: User {1} ({2}) joined.", message.UserId, message.Name, message.EmailAddress);
			HomeController.RecentlyCreatedUsers.Enqueue(result);

			while (HomeController.RecentlyCreatedUsers.Count > 5)
				HomeController.RecentlyCreatedUsers.Dequeue();
		}
	}
}