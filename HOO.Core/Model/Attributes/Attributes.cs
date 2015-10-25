using System;
using System.Collections.Generic;

namespace HOO.Core.Model
{
	public class Attributes
	{
		private Dictionary<int, object> _attributes;

		public Attributes ()
		{
			this._attributes = new Dictionary<int, object> ();
		}

		public object this [int index]
		{
			get { return this._attributes [index];}
			set { this._attributes [index] = value;}
		}

		public void Add(int attributeId, object attributeValue)
		{
			this._attributes.Add (attributeId, attributeValue);
		}

		public int RadiationLevel {
			get{ return Convert.ToInt32 (this._attributes [(int)ObjectAttribute.RadiationLevel]);}
			set {
				if (_attributes.ContainsKey ((int)ObjectAttribute.RadiationLevel)) 
					this._attributes [(int)ObjectAttribute.RadiationLevel] = value;
				else 
					_attributes.Add ((int)ObjectAttribute.RadiationLevel, value);
			}
		}

		public int Temperature {
			get{ return Convert.ToInt32 (this._attributes [(int)ObjectAttribute.Temperature]);}
			set {
				if (_attributes.ContainsKey ((int)ObjectAttribute.Temperature)) 
					this._attributes [(int)ObjectAttribute.Temperature] = value;
				else 
					_attributes.Add ((int)ObjectAttribute.Temperature, value);
			}
		}
	}
}

