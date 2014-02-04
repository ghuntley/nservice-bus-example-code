using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserService.Messages.Commands;
using NServiceBus;
using NServiceBus.Logging;

namespace UserService
{
    public class VerificationEmailSender : IHandleMessages<SendVerificationEmailCmd>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(VerificationEmailSender));

        public void Handle(SendVerificationEmailCmd message)
        {
            if (message.IsReminder)
            {
                log.InfoFormat("Send reminder email to {0} at address {1} with verification code {2}",
                    message.Name, message.EmailAddress, message.VerificationCode);
            }
            else
            {
                log.InfoFormat("Send original verification email to {0} at address {1} with verification code {2}",
                    message.Name, message.EmailAddress, message.VerificationCode);
            }
        }
    }
}
