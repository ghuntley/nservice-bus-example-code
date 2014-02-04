Chapter 1: Getting on the IBus - Example Code

This solution demonstrates basic message sending using NServiceBus.

The UserService.Messages project includes the command class CreateNewUserCmd.

The ExampleWeb project is an ASP.NET MVC web application that includes a
controller action to send the CreateNewUserCmd to the back end service to 
be processed.

The UserService project is an NServiceBus hosted endpoint which processes
the CreateNewUserCmd.

The solution is set up with NuGet Package Restore. Building the solution should
download all the required packages.

To run the solution, you must first prepare your computer to use NServiceBus
using the instructions in the text under the heading "Getting the code." Then,
once your computer is prepared, you can run the solution by following the 
instructions in under the heading "Running the solution."

Keep in mind that the configuration to start multiple projects simulteously as 
described in the text is not stored in the .sln file, so you must set this up
yourself or manually start each project.