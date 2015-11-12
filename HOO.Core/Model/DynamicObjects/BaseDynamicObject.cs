using HOO.Core.Model.Universe;

namespace HOO.Core.Model
{
	public class BaseDynamicObject
	{
		public int DOBID { get; set; }
		public bool IsLoaded { get; set; }
		public bool IsSaved { get; set; }
		public int ObjectType { get; set; }
		public Player Owner { get; set; }

		public BaseDynamicObject ()
		{
			this.IsLoaded = this.IsSaved = false;
		}
	}
}

