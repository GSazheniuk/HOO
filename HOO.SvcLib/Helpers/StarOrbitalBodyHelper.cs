using System;
using HOO.Core.Model.Universe;
using HOO.Core.Configuration;
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

				if (!OrbitalBody.Attributes.ContainsAttribute (HOO.Core.Model.ObjectAttribute.BaseResearch)) {
					InitBaseAttributes ();
					OrbitalBody.IsSaved = false;
				} else {
					throw new Exception (res.ResultMsg);
				}
			}
		}

		public void InitBaseAttributes()
		{
				var attr = OrbitalBody.Attributes;
			if (OrbitalBody is Planet) {
				Planet p = (Planet)OrbitalBody;

				//Population
				switch ((int)p.Size) {
				case 0://Tiny
					attr.BasePopulation = 1;
					break;
				case 1://Small
					attr.BasePopulation = 2;
					break;
				case 2://Medium
					attr.BasePopulation = 3;
					break;
				case 3://Large
					attr.BasePopulation = 5;
					break;
				case 4://Huge
					attr.BasePopulation = 8;
					break;
				}

				//Farming
				switch ((int)p.Type) {
				case 0://Baren
					attr.BaseFarming = 0;
					break;
				case 1://Desert
					attr.BaseFarming = 0;
					break;
				case 2://Tundra
					attr.BaseFarming = 0.5;
					break;
				case 3://Arid
					attr.BaseFarming = 0.5;
					break;
				case 4://Swamp
					attr.BaseFarming = 1;
					break;
				case 5://Ocean
					attr.BaseFarming = 1;
					break;
				case 6://Terran
					attr.BaseFarming = 2;
					break;
				case 7://Gaia
					attr.BaseFarming = 3;
					break;
				}

				//Production
				attr.BaseProduction = 0.3 * (9 - p.OrbitNo); //Nearer to Sun - more production.

				//Research
				attr.BaseResearch = HOO.Core.Model.Configuration.MrRandom.rnd.Next (6) / 2.0;
			}
		}
	}
}

