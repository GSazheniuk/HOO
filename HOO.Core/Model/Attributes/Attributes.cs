using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using HOO.Core.Model.Universe;

namespace HOO.Core.Model
{
	public partial class Attributes
	{
		public BaseObject ParentObject { get; set; }
		private OAttribute[] _attrs;
		public int TotalAttributes { get { return this._attrs.Length; } }

		public Attributes()
		{
			this._attrs = new OAttribute[0];
		}

		public void Load(OAttribute[] attributes)
		{
			this._attrs = attributes;
		}

		public bool ContainsAttribute(ObjectAttribute attribute, AttributeTypes attributeType)
		{
			try
			{
				return this._attrs.Any(oa => oa.AttributeID == (int)attribute && oa.AttributeType == (int)attributeType);
			}
			catch (Exception ex) {
				throw ex;
			}
		}

		public object ValueOf(ObjectAttribute attribute, AttributeTypes attributeType)
		{
			return this._attrs.FirstOrDefault (oa => oa.AttributeID == (int)attribute && oa.AttributeType == (int)attributeType).Value;
		}

		public OAttribute[] ChangedAttributes()
		{
			if (this._attrs.Any (oa => !oa.IsSaved)) {
				return this._attrs.Where (oa => !oa.IsSaved).ToArray();
			} else
				return new OAttribute[0];
		}

		public void SaveAll(){
			for (int i=0; i<this._attrs.Length; i++) {
				this._attrs [i].IsSaved = this._attrs [i].IsLoaded = true;
			}
		}
	}
}
