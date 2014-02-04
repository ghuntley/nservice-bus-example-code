using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using System.Reflection;

namespace Packt.Conventions
{
    // Because our conventions are encapsulated in a standalone assembly, we can simply
    // include this assembly in all of our endpoints to define our message conventions
    // across our entire organization.
    public class MessageConventions : IWantToRunBeforeConfiguration
    {
        public void Init()
        {
            // All our messages will start with Packt.
            // Commands will have namespace ending with "Commands"
            // Events will have namespace ending with "Events" or "Contracts"
            // Internal messages will have namespace ending with "InternalMessages"
            Configure.Instance
                .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("Packt.") && t.Namespace.EndsWith("Commands"))
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("Packt.") && (t.Namespace.EndsWith("Events") || t.Namespace.EndsWith("Contracts")))
                .DefiningMessagesAs(t => t.Namespace != null && t.Namespace.StartsWith("Packt.") && t.Namespace.EndsWith("InternalMessages"))

                // More involved conventions for Express/TimeToBeReceived/DataBus that use reflection.
                // Don't worry too much about using reflection on each type because the type that
                // apply to each convention are cached by NServiceBus. The reflection isn't applied
                // on every single message, only once at endpoint startup.
                .DefiningExpressMessagesAs(t => IsExpressMessage(t))
                .DefiningTimeToBeReceivedAs(t => GetTimeToBeReceived(t))
                .DefiningDataBusPropertiesAs(pi => IsDataBusProperty(pi));
        }

        private static bool IsExpressMessage(Type t)
        {
            // It is an Express message if the type name ends with Express
            if (t.Name.EndsWith("Express"))
                return true;

            // Or if we don't want to be bound by that, we can create an ExpressAttribute
            // right in our messages assembly and find it using reflection.
            if (t.GetCustomAttributes(true).Any(att => att.GetType().Name == "ExpressAttribute"))
                return true;

            return false;
        }

        private static bool IsDataBusProperty(PropertyInfo pi)
        {
            // It is a DataBus property if the property name ends with DataBus
            if (pi.Name.EndsWith("DataBus"))
                return true;

            // Or if we don't want to be bound by that, we can create a DataBusAttribute
            // right in our messages assembly and find it using reflection.
            if (pi.GetCustomAttributes(true).Any(att => att.GetType().Name == "DataBusAttribute"))
                return true;

            return false;
        }

        private static TimeSpan GetTimeToBeReceived(Type t)
        {
            // Look for any attribute called TimeToBeReceivedAttribute, which we can
            // recreate directly in our messages assembly instead of binding to the
            // one in the NServiceBus assembly.
            var attribute = t.GetCustomAttributes(true)
                .FirstOrDefault(att => att.GetType().Name == "TimeToBeReceivedAttribute");

            if (attribute != null)
            {
                // Find the TimeToBeReceived property and return its value
                var property = attribute.GetType().GetProperty("TimeToBeReceived");
                if (property != null && property.PropertyType == typeof(TimeSpan))
                    return (TimeSpan)property.GetValue(attribute, null);
            }

            // We didn't find a suitable attribute, so return MaxValue as default.
            return TimeSpan.MaxValue;
        }
    }
}
