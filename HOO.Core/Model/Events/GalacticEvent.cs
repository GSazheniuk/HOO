using System;

namespace HOO.Core.Model.Events
{
	public class GalacticEvent:BaseEvent
	{
		public Modifiers.GalaxyModifier Modifier { get; set; }
		public Universe.Galaxy[] AffectedGalaxies { get; set; }

		public GalacticEvent (Universe.Galaxy[] gals)
		{
			this.AffectedGalaxies = gals;
		}

		private void SaveEvent()
		{
		}

		public void RadiationChange(int eventDuration)
		{
			Modifier = new HOO.Core.Model.Modifiers.GalaxyModifier ();
			Modifier.Attribute = HOO.Core.Model.Attributes.GalaxyAttribute.RadiationLevel;
			Modifier.ModifierValue = Configuration.MrRandom.rnd.Next (100) - 50; //TO-DO implement min-max values for attributes
			this.Cycle = HOO.Core.Model.Events.Config.CycleType.Period;
			this.Duration = eventDuration;

			SaveEvent ();
		}
	}
}

