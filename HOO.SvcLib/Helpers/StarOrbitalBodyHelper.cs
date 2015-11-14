using System;
using HOO.Core.Model.Universe;
using HOO.Core.Configuration;
using HOO.Core.Model;
using HOO.DB;

namespace HOO.SvcLib.Helpers
{
	public class StarOrbitalBodyHelper
	{
		public StarOrbitalBody OrbitalBody;
		private MySqlDBHelper _dh ;

		public StarOrbitalBodyHelper()
		{
			_dh = new MySqlDBHelper (SensitiveData.ConnectionString);
		}

		public StarOrbitalBodyHelper (StarOrbitalBody starOrbitalBody)
		{
			_dh = new MySqlDBHelper (SensitiveData.ConnectionString);
			OrbitalBody = starOrbitalBody;
		}

		public void LoadOrbitalBody()
		{
			DBCommandResult res = new DBCommandResult ();
			res = _dh.LoadOrbitalBody (OrbitalBody);

			if (res.ResultCode == 0) {
				OrbitalBody.IsLoaded = OrbitalBody.IsSaved = true;

				if (this.OrbitalBody.Attributes.TotalAttributes == 0) {
					InitDefaultParameters ();
				} 
			} else {
				throw new Exception (res.ResultMsg);
			}
		}

		private void InitDefaultParameters()
		{
			OAttribute[] attr = new OAttribute[4];
			if (OrbitalBody is Planet) {
				Planet p = (Planet)OrbitalBody;

				//Population
				switch ((int)p.Size) {
				case 0://Tiny
					attr[0] = new OAttribute(ObjectAttribute.BasePopulation, AttributeTypes.Attribute, 2);
					break;
				case 1://Small
					attr[0] = new OAttribute(ObjectAttribute.BasePopulation, AttributeTypes.Attribute, 3);
					break;
				case 2://Medium
					attr[0] = new OAttribute(ObjectAttribute.BasePopulation, AttributeTypes.Attribute, 5);
					break;
				case 3://Large
					attr[0] = new OAttribute(ObjectAttribute.BasePopulation, AttributeTypes.Attribute, 8);
					break;
				case 4://Huge
					attr[0] = new OAttribute(ObjectAttribute.BasePopulation, AttributeTypes.Attribute, 13);
					break;
				}

				//Farming
				switch ((int)p.Type) {
				case 0://Baren
					attr[1] = new OAttribute(ObjectAttribute.BaseFarming, AttributeTypes.Attribute, 0);
					break;
				case 1://Desert
					attr[1] = new OAttribute(ObjectAttribute.BaseFarming, AttributeTypes.Attribute, 0);
					break;
				case 2://Tundra
					attr[1] = new OAttribute(ObjectAttribute.BaseFarming, AttributeTypes.Attribute, 0.5);
					break;
				case 3://Arid
					attr[1] = new OAttribute(ObjectAttribute.BaseFarming, AttributeTypes.Attribute, 0.5);
					break;
				case 4://Swamp
					attr[1] = new OAttribute(ObjectAttribute.BaseFarming, AttributeTypes.Attribute, 1);
					break;
				case 5://Ocean
					attr[1] = new OAttribute(ObjectAttribute.BaseFarming, AttributeTypes.Attribute, 1);
					break;
				case 6://Terran
					attr[1] = new OAttribute(ObjectAttribute.BaseFarming, AttributeTypes.Attribute, 2);
					break;
				case 7://Gaia
					attr[1] = new OAttribute(ObjectAttribute.BaseFarming, AttributeTypes.Attribute, 3);
					break;
				}

				//Production
				attr[2] = new OAttribute(ObjectAttribute.BaseProduction, AttributeTypes.Attribute, 0.3 * (9 - p.OrbitNo));

				//Research
				attr[3] = new OAttribute(ObjectAttribute.BaseResearch, AttributeTypes.Attribute, HOO.Core.Model.Configuration.MrRandom.rnd.Next (6) / 2.0);
			}
			this.OrbitalBody.Attributes.Load (attr);
			this.OrbitalBody.IsSaved = false;
		}

		public void Save()
		{
			if (this.OrbitalBody.IsLoaded && !this.OrbitalBody.IsSaved) {
				DBCommandResult res = _dh.SaveOrbitalBody (this.OrbitalBody);
				if (res.ResultCode == 0)
					this.OrbitalBody.IsSaved = true;
				else
					throw new Exception (res.ResultMsg);
			}
		}
	}
}

