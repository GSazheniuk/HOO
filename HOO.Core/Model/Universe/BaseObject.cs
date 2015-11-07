namespace HOO.Core.Model.Universe
{
	public class BaseObject
	{
		public int OBID { get; set; }
		public Attributes Attributes;
		public Effects Effects;
		public bool IsLoaded { get; set; }
		public bool IsSaved { get; set; }

		public BaseObject ()
		{
			this.Attributes = new Attributes ();
			this.Effects = new Effects ();
			this.IsLoaded = this.IsSaved = false;
		}
	}
}

