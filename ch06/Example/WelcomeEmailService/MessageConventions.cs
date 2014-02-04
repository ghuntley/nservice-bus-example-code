using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace WelcomeEmailService
{
    public class MessageConventions : IWantToRunBeforeConfiguration
    {
        public void Init()
        {
            // Because this is a simple example, we don't have a common top-level
            // namespace to distinguish the assemblies that are our own code.
            // So we will use a convention where the namespace can not start with
            // "NServiceBus" or "System". Keep in mind that if we were to add a 
            // 3rd party assembly with a type that accidentally fit one of our
            // conventions, we may have to adjust the conventions to match.
            Configure.Instance
                .DefiningCommandsAs(t => IsFromMessageAssembly(t) && t.Namespace.EndsWith("Commands"))
                .DefiningEventsAs(t => IsFromMessageAssembly(t) && t.Namespace.EndsWith("Events"))
                .DefiningMessagesAs(t => IsFromMessageAssembly(t) && t.Namespace.EndsWith("InternalMessages"));
        }

        private static bool IsFromMessageAssembly(Type t)
        {
            return t.Namespace != null
                && !t.Namespace.StartsWith("NServiceBus.")
                && !t.Namespace.StartsWith("System.");
        }
    }
}
