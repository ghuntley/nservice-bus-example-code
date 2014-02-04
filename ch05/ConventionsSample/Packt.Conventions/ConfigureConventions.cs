using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace Packt.Conventions
{
    public static class ConfigureConventions
    {
        // An extension method we can use to include the conventions in this
        // assembly when self-hosting in a web application.
        public static Configure UsingPacktConventions(this Configure config)
        {
            new MessageConventions().Init();
            return config;
        }

        // This method shows how we could set up the fluent configuration for
        // self hosting to include the Packt message conventions contained in
        // this project.
        //public static void ExampleForSelfHosting()
        //{
        //    IBus Bus = Configure.With()
        //            .DefineEndpointName("WebsiteEndpoint")
        //            .UsingPacktConventions()   // <--- Call the extension method here
        //            .DefaultBuilder()
        //            .UseTransport<Msmq>()
        //            .PurgeOnStartup(true)
        //            .UnicastBus()
        //            .CreateBus()
        //            .Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());
        //}
    }
}
