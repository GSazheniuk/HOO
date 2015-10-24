using System;

namespace HOO.Core.Model.Events
{
	public class BaseEvent
	{
		public Config.CycleType Cycle{ get; set; }
		public int Duration { get; set; }
		public string EventName { get; set; }
		public string EventDescription { get; set; }

		public BaseEvent ()
		{

		}
	}
}

