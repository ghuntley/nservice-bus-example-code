Chapter 2: Messaging Patterns - Example Code

This solution builds upon the Chapter 1 example by introducing the concept
of Publish/Subscribe.

The WelcomeEmailService subscribes to the IUserCreatedEvent and demonstrates
how to send a welcome email after a user has been created.

In addition, the ExampleWeb project also subscribes to the IUserCreatedEvent
and maintains a list of recently added users. An additional MVC action has
been added to the HomeController to output the recent users.

The solution is set up with NuGet Package Restore. Building the solution should
download all the required packages.

Keep in mind that the configuration to start multiple projects simulteously as 
described in the text is not stored in the .sln file, so you must set this up
yourself or manually start each project.