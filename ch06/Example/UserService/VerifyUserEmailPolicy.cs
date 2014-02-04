using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using NServiceBus.Saga;
using UserService.Messages.Commands;
using UserService.Messages.Events;
using NServiceBus.Logging;

namespace UserService
{
    public class VerifyUserEmailPolicy : Saga<VerifyUserEmailPolicyData>,
        IAmStartedByMessages<CreateNewUserCmd>,
        IHandleMessages<UserVerifyingEmailCmd>,
        IHandleTimeouts<VerifyUserEmailReminderTimeout>,
        IHandleTimeouts<VerifyUserEmailExpiredTimeout>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(VerifyUserEmailPolicy));

        public override void ConfigureHowToFindSaga()
        {
            this.ConfigureMapping<CreateNewUserCmd>(msg => msg.EmailAddress).ToSaga(data => data.EmailAddress);
            this.ConfigureMapping<UserVerifyingEmailCmd>(msg => msg.EmailAddress).ToSaga(data => data.EmailAddress);
        }

        public void Handle(CreateNewUserCmd message)
        {
            this.Data.Name = message.Name;
            this.Data.EmailAddress = message.EmailAddress;
            this.Data.VerificationCode = Guid.NewGuid().ToString("n").Substring(0, 4);

            Bus.Send(new SendVerificationEmailCmd
            {
                Name = message.Name,
                EmailAddress = message.EmailAddress,
                VerificationCode = Data.VerificationCode,
                IsReminder = false
            });

            this.RequestTimeout<VerifyUserEmailReminderTimeout>(TimeSpan.FromDays(2));
            this.RequestTimeout<VerifyUserEmailExpiredTimeout>(TimeSpan.FromDays(7));
        }

        public void Handle(UserVerifyingEmailCmd message)
        {
            if (message.VerificationCode == this.Data.VerificationCode)
            {
                Bus.Send(new CreateNewUserWithVerifiedEmailCmd
                {
                    EmailAddress = this.Data.EmailAddress,
                    Name = this.Data.Name
                });

                this.MarkAsComplete();
            }
        }

        public void Timeout(VerifyUserEmailReminderTimeout state)
        {
            Bus.Send(new SendVerificationEmailCmd
            {
                Name = Data.Name,
                EmailAddress = Data.EmailAddress,
                VerificationCode = Data.VerificationCode,
                IsReminder = true
            });
        }

        public void Timeout(VerifyUserEmailExpiredTimeout state)
        {
            this.MarkAsComplete();
        }
    }

    public class VerifyUserEmailPolicyData : ContainSagaData
    {
        public string Name { get; set; }
        [Unique]
        public string EmailAddress { get; set; }
        public string VerificationCode { get; set; }
    }

    public class VerifyUserEmailReminderTimeout
    {
    }

    public class VerifyUserEmailExpiredTimeout
    {
    }
}