using System;
using System.Collections.Generic;

namespace HOO.Core.Model
{
	public class Requisites
	{
		private Dictionary<int, object> _requisites;

		public Requisites ()
		{
			this._requisites = new Dictionary<int, object> ();
		}

		public int Count { get { return this._requisites.Count; } }
		private int[] _keys;
		public int[] Keys {
			get { 
				this._keys = new int[this._requisites.Keys.Count];
				this._requisites.Keys.CopyTo (_keys, 0);
				return this._keys; 
			} 
		}

		public object this [int index]
		{
			get { return this._requisites [index];}
			set { this._requisites [index] = value;}
		}

		public bool ContainsRequisite(ObjectRequisite requisite)
		{
			return this._requisites.ContainsKey ((int)requisite);
		}

		public void Add(int requisiteId, object requisiteValue)
		{
			this._requisites.Add (requisiteId, requisiteValue);
		}

		public int Capitol {
			get{ return Convert.ToInt32 (this._requisites [(int)ObjectRequisite.Capitol]);}
			set {
				if (_requisites.ContainsKey ((int)ObjectRequisite.Capitol)) 
					this._requisites [(int)ObjectRequisite.Capitol] = value;
				else 
					_requisites.Add ((int)ObjectRequisite.Capitol, value);
			}
		}
	}
}

