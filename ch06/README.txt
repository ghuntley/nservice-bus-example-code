Chapter 6: Sagas - Example Code

This solution builds upon the example originally introduced in Chapter 1.

	* Chapter 1 introduced send-only concepts.
	* Chapter 2 added publish/subscribe.
	* Chapter 5 added Unobtrusive Mode conventions.

This chapter introduces the VerifyUserEmailPolicy saga to the solution in the
UserService project. The VerifyUserEmailPolicy.cs file includes the saga
class, the VerifyUserEmailPolicyData saga data class, and the
VerifyUserEmailTimeout timeout data class.

The UserService.Messages project contains new commands to support the saga:

	* CreateNewUserWithVerifiedEmailCmd
	* SendVerificationEmailCmd
	* UserVerifyingEmailCmd

The UserService.Tests project contains unit tests for the saga. The unit tests
can be run with any NUnit test runner.