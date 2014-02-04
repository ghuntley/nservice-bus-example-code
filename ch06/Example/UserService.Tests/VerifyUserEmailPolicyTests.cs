using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NServiceBus.Testing;
using UserService.Messages.Commands;

namespace UserService.Tests
{
    [TestFixture]
    public class VerifyUserEmailPolicyTests
    {
        public VerifyUserEmailPolicyTests()
        {
            Test.Initialize();
        }

        [Test]
        public void User_should_be_created()
        {
            CreateNewUserCmd createUser = new CreateNewUserCmd
            {
                Name = "David",
                EmailAddress = "david@example.com"
            };

            UserVerifyingEmailCmd verifyCmd = new UserVerifyingEmailCmd
            {
                EmailAddress = "david@example.com",
                VerificationCode = "THIS IS NOT THE CORRECT CODE!"
            };

            string correctCode = null;

            var testSaga = Test.Saga<VerifyUserEmailPolicy>();

            // We expect the timeouts to be set and the verification email to be sent.
            testSaga.ExpectSend<SendVerificationEmailCmd>(cmd =>
                    {
                        correctCode = cmd.VerificationCode;
                        return cmd.EmailAddress == createUser.EmailAddress && cmd.Name == createUser.Name;
                    })
                .ExpectTimeoutToBeSetIn<VerifyUserEmailReminderTimeout>((timeout, timespan) => timespan == TimeSpan.FromDays(2))
                .ExpectTimeoutToBeSetIn<VerifyUserEmailExpiredTimeout>((timeout, timespan) => timespan == TimeSpan.FromDays(7))
                .When(saga => saga.Handle(createUser));

            // We expect the user to NOT be created if the verification code is wrong
            testSaga.ExpectNotSend<CreateNewUserWithVerifiedEmailCmd>(cmd =>
                cmd.EmailAddress == createUser.EmailAddress && cmd.Name == createUser.Name)
                .When(saga => saga.Handle(verifyCmd));

            // We expect the user to be created if the correct verification code is used.
            verifyCmd.VerificationCode = correctCode;
            testSaga.ExpectSend<CreateNewUserWithVerifiedEmailCmd>(cmd =>
                cmd.EmailAddress == createUser.EmailAddress && cmd.Name == createUser.Name)
                .When(saga => saga.Handle(verifyCmd));

            testSaga.AssertSagaCompletionIs(true);
        }

        [Test]
        public void User_should_not_be_created_after_timeout()
        {
            CreateNewUserCmd createUser = new CreateNewUserCmd
            {
                Name = "David",
                EmailAddress = "david@example.com"
            };

            UserVerifyingEmailCmd verifyCmd = new UserVerifyingEmailCmd
            {
                EmailAddress = "david@example.com",
                VerificationCode = "THIS IS NOT THE CORRECT CODE!"
            };

            string correctCode = null;

            var testSaga = Test.Saga<VerifyUserEmailPolicy>();

            // We expect the timeouts to be set and the verification email to be sent.
            testSaga.ExpectSend<SendVerificationEmailCmd>(cmd =>
                    {
                        correctCode = cmd.VerificationCode;
                        return cmd.EmailAddress == createUser.EmailAddress && cmd.Name == createUser.Name;
                    })
                .ExpectTimeoutToBeSetIn<VerifyUserEmailReminderTimeout>((timeout, timespan) => timespan == TimeSpan.FromDays(2))
                .ExpectTimeoutToBeSetIn<VerifyUserEmailExpiredTimeout>((timeout, timespan) => timespan == TimeSpan.FromDays(7))
                .When(saga => saga.Handle(createUser));

            testSaga
                .ExpectSend<SendVerificationEmailCmd>(cmd =>
                    cmd.EmailAddress == createUser.EmailAddress
                    && cmd.Name == createUser.Name
                    && cmd.VerificationCode == correctCode
                    && cmd.IsReminder == true)
                .When(saga => saga.Timeout(new VerifyUserEmailReminderTimeout()))
                .AssertSagaCompletionIs(false);

            testSaga
                .When(saga => saga.Timeout(new VerifyUserEmailExpiredTimeout()))
                .AssertSagaCompletionIs(true);
        }
    }
}
