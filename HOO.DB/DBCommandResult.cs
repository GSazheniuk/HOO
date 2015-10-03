using System;

namespace HOO.DB
{
	public class DBCommandResult
	{
		public int ResultCode { get; set; }
		public string ResultMsg { get; set; }
		public object Tag { get; set; }

		public DBCommandResult()
		{
			ResultCode = -1;
			ResultMsg = "Not implemented";
		}
	}

}

