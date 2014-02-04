using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserService.Messages.Events
{
	public interface IUserCreatedEvent
	{
		Guid UserId { get; set; }
		string Name { get; set; }
		string EmailAddress { get; set; }
	}
}
