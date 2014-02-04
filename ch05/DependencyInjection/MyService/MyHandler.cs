using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyMessages.Commands;
using NServiceBus;
using NServiceBus.Logging;

namespace MyService
{
	public class MyHandler : IHandleMessages<MyCommand>
	{
		public ITimeProvider Time { get; set; }
		public IComplexService Complex { get; set; }

		public void Handle(MyCommand message)
		{
			Console.WriteLine("ITimeProvider implemented by {0}.", Time.GetType().Name);
			Console.WriteLine("Writing current time using ITimeProvider.");
            Console.WriteLine("    Time.Now = {0}", Time.Now);
            Console.WriteLine("Id will always be the same because only one instance is created.");
            Console.WriteLine("    Id = {0}", Time.Id);
			Console.WriteLine();

			Console.WriteLine("IComplex implemented by {0}.", Complex.GetType().Name);
			Console.WriteLine("Id will always change because DependencyLifecycle=InstancePerCall.");
			Console.WriteLine("    Id = {0}", Complex.Id);
			Console.WriteLine("Output the properties we configured on the type.");
			Console.WriteLine("    TheAnswer = {0}, ConfiguredString = \"{1}\"", 
				Complex.TheAnswer, Complex.ConfiguredString);
			Console.WriteLine("Configured types receive injection of previously configured components.");
			Console.WriteLine("    Complex.Time.Now = {0}", Complex.Time.Now);
			Console.WriteLine("The Bus instance is even injected into the types we register.");
			Console.WriteLine("    Complex.Bus is {0}", Complex.Bus.GetType().Name);
			Console.WriteLine();
		}
	}
}
