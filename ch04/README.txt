Chapter 4: Self Hosting - Example Code

SendOnlyExample
---------------

This solution is a copy of the example solution from Chapter 1, modified to be
a SendOnly endpoint.

In Chapter 1, we did not create a send-only endpoint so that we could
demonstrate how to add the .LoadMessageHandlers() method in Chapter 2 when we
started publishing messages.

To verify the difference between this solutions, delete all of your message
queues and run this solution. It should create no queues on startup, but will
still be able to send the CreateNewUserCmd to the UserService endpoint.