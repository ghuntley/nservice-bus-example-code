Chapter 3: Preparing for Failure - Example Code

This solution demonstrates error retries and second level retries.

The MessageSender class implements an interface we have not seen before called
IWantToRunWhenBusStartsAndStops, which enables us to perform actions when the
endpoint starts up. In this case, it gives us the ability to send a test message
based on user input.

The ErrorThrower class is our message handler. We do some tracking of the first
and second level retry counts, output them to screen, and then throw an
exception to trigger the retry mechanisms.

The App.config file in the RetryService contains settings (individually
commented) which affect the retry behavior of the endpoint. Play around with
them and watch how the number and speed of retries is affected.