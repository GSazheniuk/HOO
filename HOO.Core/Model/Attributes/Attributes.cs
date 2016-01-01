using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using HOO.Core.Model.Universe;
using MongoDB.Bson.Serialization.Attributes;

namespace HOO.Core.Model
{
	public partial class Attributes
	{
		//public BaseObject ParentObject { get; set; }
		private OAttribute[] _attrs;
        [BsonIgnore]
		private Dictionary<Tuple<int,int>, OAttribute> _ah;
        [BsonIgnore]
        public int TotalAttributes { get { return this._attrs.Length; } }

		public Attributes()
		{
			this._attrs = new OAttribute[0];
			this._ah = new Dictionary<Tuple<int,int>, OAttribute>();
		}

		public void Load(OAttribute[] attributes)
		{
			this._attrs = attributes;
		}

		public void AddAttribute (OAttribute oa)
		{
			//this._ah.Add (Tuple.Create(oa.AttributeID, oa.AttributeType), oa);
		}

		public bool ContainsAttribute(ObjectAttribute attribute, AttributeType attributeType)
		{
			try
			{
				//return this._attrs.Any(oa => oa.AttributeID == (int)attribute && oa.AttributeType == (int)attributeType);
			}
			catch (Exception ex) {
				throw ex;
			}
            return false;
		}

		public object ValueOf(ObjectAttribute attribute, AttributeType attributeType)
		{
			//var res = this._attrs.FirstOrDefault (oa => oa.AttributeID == (int)attribute && oa.AttributeType == (int)attributeType);
			//if (res != null) 
			//	return res.Value;
			//else
				return new OAttribute ().Value;
		}

		public OAttribute[] ChangedAttributes()
		{
			//var res = this._attrs.Where (oa => !oa.IsSaved);
			//if (res != null) {
			//	return res.ToArray ();
			//} else
				return new OAttribute[0];
		}

		public void SaveAll(){
			for (int i=0; i<this._attrs.Length; i++) {
//				this._attrs [i].IsSaved = this._attrs [i].IsLoaded = true;
			}
		}

		public List<OAttribute> ListByType(AttributeType attributeType)
		{
//			var res = this._ah.Where (oa => oa.Value.AttributeType == (int)attributeType);
//			if (res != null) {
////				return res.Select(oa=>oa.Value).ToList();
//				return new List<OAttribute> ();
//			} else
				return new List<OAttribute> ();
		}
	}
}
