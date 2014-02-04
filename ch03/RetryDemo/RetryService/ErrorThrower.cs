using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RetryService.Messages.Commands;
using NServiceBus;
using System.Collections.Concurrent;

namespace RetryService
{
	public class ErrorThrower : IHandleMessages<GoBoomCmd>
	{
		public IBus Bus { get; set; }

		private static ConcurrentDictionary<Guid, RetryInfo> tracker
			= new ConcurrentDictionary<Guid, RetryInfo>();

		public void Handle(GoBoomCmd cmd)
		{
			// Get the Second Level Retry level from the message headers.
			string slrLevel = cmd.GetHeader(Headers.Retries);

			// Get some info to track the normal retries
			RetryInfo info = tracker.GetOrAdd(cmd.UniqueId, id => new RetryInfo { SlrLevel = slrLevel });

			// If the SLR level has changed, reset the normal try count
			if (info.SlrLevel != slrLevel)
			{
				info.SlrLevel = slrLevel;
				info.Tries = 0;

				// Take note of how much time has passed since the last SLR phase
				TimeSpan ts = DateTime.UtcNow - info.LastMessageProcessed;
				Console.WriteLine("Beginning SLR Round #{0}, {1:0} seconds since last attempt.",
					slrLevel, ts.TotalSeconds);
			}

			// Increment the normal try count and timestamp
			info.Tries++;
			info.LastMessageProcessed = DateTime.UtcNow;

			// Output what is happening this round
			if (String.IsNullOrEmpty(slrLevel))
				Console.WriteLine("Phase: First Level Processing, Try #{0}", info.Tries);
			else
				Console.WriteLine("Phase: Second Level Retries Round #{0}, Try #{1}", slrLevel, info.Tries);

			// Now throw the exception
			throw new ApplicationException("BOOM!");
		}

		class RetryInfo
		{
			internal string SlrLevel;
			internal int Tries;
			internal DateTime LastMessageProcessed;
		}
	}
}
