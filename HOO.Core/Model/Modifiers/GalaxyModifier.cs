using System;
using HOO.Core.Model;

namespace HOO.Core.Model.Modifiers
{
	public class GalaxyModifier
	{
		public Attributes.GalaxyAttribute Attribute{ get; set; }
		public object ModifierValue { get; set; }

		public GalaxyModifier()
		{
		}
	}
}

