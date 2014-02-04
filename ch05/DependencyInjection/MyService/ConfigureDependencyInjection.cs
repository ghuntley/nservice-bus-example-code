using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.ObjectBuilder;

namespace MyService
{
	public class ConfigureDependencyInjection : NServiceBus.IWantCustomInitialization
	{
		public void Init()
		{
			// Configure the DateTimeProvider as a single instance (singleton)
			Configure.Component<DateTimeProvider>(DependencyLifecycle.SingleInstance);

			// Configure the complex service, showing how to configure properties
			// and to show that ITimeProvider and IBus will be injected into the
			// class as well.
			Configure.Component<ComplexServiceImpl>(DependencyLifecycle.InstancePerCall)
				.ConfigureProperty(csi => csi.ConfiguredString, "Hello world!")
				.ConfigureProperty(csi => csi.TheAnswer, 42);
		}
	}

	

}
