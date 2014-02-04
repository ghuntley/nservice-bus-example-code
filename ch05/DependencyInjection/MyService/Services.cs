using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace MyService
{
	public interface ITimeProvider
	{
        Guid Id { get; }
		DateTime Now { get; }
		DateTime UtcNow { get; }
	}

	public class DateTimeProvider : ITimeProvider
	{
        private Guid _id = Guid.NewGuid();

        public Guid Id
        {
            get { return _id; }
        }

		public DateTime Now
		{
			get { return DateTime.Now; }
		}

		public DateTime UtcNow
		{
			get { return DateTime.UtcNow; }
		}
	}

	public interface IComplexService
	{
		Guid Id { get; }
		ITimeProvider Time { get; }
		IBus Bus { get; }
		string ConfiguredString { get; }
		int TheAnswer { get; }
	}

	public class ComplexServiceImpl : IComplexService
	{
		private Guid _id = Guid.NewGuid();

		public Guid Id
		{
			get { return _id; }
		}

		public ITimeProvider Time { get; set; }
		public IBus Bus { get; set; }
		public string ConfiguredString { get; set; }
		public int TheAnswer { get; set; }
	}
}