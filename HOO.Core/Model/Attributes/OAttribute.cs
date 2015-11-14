namespace HOO.Core.Model
{
	public class OAttribute
	{
		public int AID { get; set; }
		public int AttributeID { get; set; }
		public int AttributeType { get; set; }
		public object Value { get; set; }
		public bool IsSaved;
		public bool IsLoaded;

		public OAttribute()
		{
			this.AID = -1000;
			this.Value = "-";
		}

		public OAttribute(ObjectAttribute attribute, AttributeTypes attributeType, object value)
		{
			this.AID = -1;
			this.AttributeID = (int)attribute;
			this.AttributeType = (int)attributeType;
			this.Value = value;
			this.IsSaved = this.IsLoaded = false;
		}
	}
}

