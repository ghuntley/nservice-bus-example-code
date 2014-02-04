using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserService.Messages.Commands
{
    public class SendVerificationEmailCmd
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string VerificationCode { get; set; }
        public bool IsReminder { get; set; }
    }
}
