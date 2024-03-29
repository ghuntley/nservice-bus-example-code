﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace UserService.Messages.Events
{
	public interface IUserCreatedEvent : IEvent
	{
		Guid UserId { get; set; }
		string Name { get; set; }
		string EmailAddress { get; set; }
	}
}
