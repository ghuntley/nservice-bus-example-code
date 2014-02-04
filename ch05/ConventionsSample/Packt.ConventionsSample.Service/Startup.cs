using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using NServiceBus.Unicast.Messages;

namespace Packt.ConventionsSample.Service
{
    public class Startup : IWantToRunWhenBusStartsAndStops
    {
        //public IBus Bus { get; set; }
        public MessageMetadataRegistry MessageMetadata { get; set; }

        public void Start()
        {
            Console.WriteLine("Going to list every message type and metadata.");
            Console.WriteLine();

            //var msgRegistry = (Bus as NServiceBus.Unicast.UnicastBus).MessageMetadataRegistry;

            foreach (var metadata in MessageMetadata.GetAllMessages())
            {
                Type type = metadata.MessageType;
                string name = type.FullName;

                if (MessageConventionExtensions.IsInSystemConventionList(type))
                    Console.WriteLine("System Msg: {0}", name);
                else if (MessageConventionExtensions.IsCommandType(type))
                    Console.WriteLine("   Command: {0}", name);
                else if (MessageConventionExtensions.IsEventType(type))
                    Console.WriteLine("     Event: {0}", name);
                else if (MessageConventionExtensions.IsMessageType(type))
                    Console.WriteLine("   Message: {0}", name);

                if (!metadata.Recoverable)
                    Console.WriteLine("            Express=true");
                if (metadata.TimeToBeReceived != TimeSpan.MaxValue)
                    Console.WriteLine("            TimeToBeReceived={0}", metadata.TimeToBeReceived);
                

            }
        }

        public void Stop()
        {
        }
    }
}
