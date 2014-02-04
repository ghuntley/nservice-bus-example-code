using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleAppClient.ThirdPartySvc;

namespace ConsoleAppClient
{
	class Program
	{
		static void Main(string[] args)
		{
			WcfServiceOf_ThirdPartyCmd_ThirdPartyCmdResponseClient svc = new WcfServiceOf_ThirdPartyCmd_ThirdPartyCmdResponseClient();

			Console.WriteLine("Type 'OK' to receive an 'OK' response.");
			Console.WriteLine("Type anything else to get a 'Rejected' response.");
			Console.WriteLine("Type 'quit' to quit.");

			while (true)
			{
				Console.WriteLine();
				string input = Console.ReadLine();
				if (String.Equals(input, "quit", StringComparison.OrdinalIgnoreCase))
					return;

				ThirdPartyCmd cmd = new ThirdPartyCmd { Contents = input };
				ThirdPartyCmdResponse response = svc.Process(cmd);
				Console.WriteLine("Received response: {0}", response);
			}
		}
	}
}
