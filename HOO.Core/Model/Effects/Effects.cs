using System;
using System.Collections.Generic;

namespace HOO.Core.Model
{
	public class Effects
	{
		private Dictionary<int, object> _effects;

		public Effects ()
		{
			this._effects = new Dictionary<int, object> ();
		}

		public int Count { get { return this._effects.Count; } }
		private int[] _keys;
		public int[] Keys {
			get { 
				this._keys = new int[this._effects.Keys.Count];
				this._effects.Keys.CopyTo (_keys, 0);
				return this._keys; 
			} 
		}

		public object this [int index]
		{
			get { return this._effects [index];}
			set { this._effects [index] = value;}
		}

		public bool ContainsEffect(ObjectAttribute attribute)
		{
			return this._effects.ContainsKey ((int)attribute);
		}

		public void Add(int attributeId, object effectValue)
		{
			this._effects.Add (attributeId, effectValue);
		}

/*		public double Income {
			get{ return Convert.ToDouble (this._effects [(int)ObjectAttribute.TotalCredits]);}
			set {
				if (_effects.ContainsKey ((int)ObjectAttribute.TotalCredits)) 
					this._effects [(int)ObjectAttribute.TotalCredits] = value;
				else 
					_effects.Add ((int)ObjectAttribute.TotalCredits, value);
			}
		}*/
	}
}

