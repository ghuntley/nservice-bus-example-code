using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Packt.ConventionsSample.Messages
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    internal class ExpressAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    internal class TimeToBeReceivedAttribute : Attribute
    {
        private readonly TimeSpan timeToBeReceived;

        public TimeToBeReceivedAttribute(string timeSpan)
        {
            if (!TimeSpan.TryParse(timeSpan, out timeToBeReceived))
                timeToBeReceived = TimeSpan.MaxValue;
        }

        public TimeSpan TimeToBeReceived
        {
            get { return timeToBeReceived; }
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    internal class DataBusAttribute : Attribute
    {
    }
}
