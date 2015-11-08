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

		public int Count { get { return this._attributes.Count; } }
		private int[] _keys;
		public int[] Keys {
			get { 
				this._keys = new int[this._attributes.Keys.Count];
				this._attributes.Keys.CopyTo (_keys, 0);
				return this._keys; 
			} 
		}

		public object this [int index]
		{
			get { return this._attributes [index];}
			set { this._attributes [index] = value;}
		}

		public bool ContainsAttribute(ObjectAttribute attribute)
		{
			return this._attributes.ContainsKey ((int)attribute);
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
		
		public int BasePopulation {
			get{ return Convert.ToInt32 (this._attributes [(int)ObjectAttribute.BasePopulation]);}
			set {
				if (_attributes.ContainsKey ((int)ObjectAttribute.BasePopulation)) 
					this._attributes [(int)ObjectAttribute.BasePopulation] = value;
				else 
					_attributes.Add ((int)ObjectAttribute.BasePopulation, value);
			}
		}
		
		public double BaseProduction {
			get{ return Convert.ToDouble (this._attributes [(int)ObjectAttribute.BaseProduction]);}
			set {
				if (_attributes.ContainsKey ((int)ObjectAttribute.BaseProduction)) 
					this._attributes [(int)ObjectAttribute.BaseProduction] = value;
				else 
					_attributes.Add ((int)ObjectAttribute.BaseProduction, value);
			}
		}
		
		public double BaseFarming {
			get{ return Convert.ToDouble (this._attributes [(int)ObjectAttribute.BaseFarming]);}
			set {
				if (_attributes.ContainsKey ((int)ObjectAttribute.BaseFarming)) 
					this._attributes [(int)ObjectAttribute.BaseFarming] = value;
				else 
					_attributes.Add ((int)ObjectAttribute.BaseFarming, value);
			}
		}
		
		public double BaseResearch {
			get{ return Convert.ToDouble (this._attributes [(int)ObjectAttribute.BaseResearch]);}
			set {
				if (_attributes.ContainsKey ((int)ObjectAttribute.BaseResearch)) 
					this._attributes [(int)ObjectAttribute.BaseResearch] = value;
				else 
					_attributes.Add ((int)ObjectAttribute.BaseResearch, value);
			}
		}

		public double TotalCredits {
			get{ return Convert.ToDouble (this._attributes [(int)ObjectAttribute.TotalCredits]);}
			set {
				if (_attributes.ContainsKey ((int)ObjectAttribute.TotalCredits)) 
					this._attributes [(int)ObjectAttribute.TotalCredits] = value;
				else 
					_attributes.Add ((int)ObjectAttribute.TotalCredits, value);
			}
		}
	}
}
