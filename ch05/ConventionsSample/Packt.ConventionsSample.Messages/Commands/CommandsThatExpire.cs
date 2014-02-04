using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Packt.ConventionsSample.Messages.Commands
{
    [TimeToBeReceived("00:01:00")]
    public class ExpiresAfter1Min
    {
    }

    [TimeToBeReceived("30.00:00:00")]
    public class ExpiresAfter30Days
    {
    }
}
