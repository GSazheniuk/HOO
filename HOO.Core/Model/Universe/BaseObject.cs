namespace HOO.Core.Model.Universe
{
	public class BaseObject
	{
		public int OBID { get; set; }
		public Attributes Attributes;
		public bool IsLoaded { get; set; }
		public bool IsSaved { get; set; }
		public int ObjectType { get; set; }

		public BaseObject ()
		{
			this.Attributes = new Attributes ();
			this.IsLoaded = this.IsSaved = false;
		}
	}
}

