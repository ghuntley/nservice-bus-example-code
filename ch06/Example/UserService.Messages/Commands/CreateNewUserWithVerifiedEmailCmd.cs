﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserService.Messages.Commands
{
	public class CreateNewUserWithVerifiedEmailCmd
	{
		public string EmailAddress { get; set; }
		public string Name { get; set; }
	}
}
