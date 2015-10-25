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

		public object this [int index]
		{
			get { return this._effects [index];}
			set { this._effects [index] = value;}
		}

		public void Add(int attributeId, object effectValue)
		{
			this._effects.Add (attributeId, effectValue);
		}
	}
}

