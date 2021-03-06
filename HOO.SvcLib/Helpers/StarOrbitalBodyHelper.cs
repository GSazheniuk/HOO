using System;
using HOO.Core.Model.Universe;
using HOO.Core.Model;
using HOO.DB;
using System.Collections.Generic;

namespace HOO.SvcLib.Helpers
{
	public class StarOrbitalBodyHelper
	{
		public StarOrbitalBody OrbitalBody;
		private MySqlDBHelper _dh ;
        private MongoDBHelper _mdh;
        private Log.Logger log;

        public StarOrbitalBodyHelper()
		{
//			_dh = new MySqlDBHelper (SensitiveData.ConnectionString);
            this._mdh = new MongoDBHelper();
            this.log = new Log.Logger("HOO.SvcLib", typeof(StarOrbitalBodyHelper));
        }

        public StarOrbitalBodyHelper (StarOrbitalBody starOrbitalBody)
		{
//			_dh = new MySqlDBHelper (SensitiveData.ConnectionString);
            this._mdh = new MongoDBHelper();
            OrbitalBody = starOrbitalBody;
            this.log = new Log.Logger("HOO.SvcLib", typeof(StarOrbitalBodyHelper));
        }

        public void LoadOrbitalBody()
		{
			DBCommandResult res = new DBCommandResult ();
			res = _dh.LoadOrbitalBody (OrbitalBody);

			if (res.ResultCode == 0) {
				OrbitalBody.IsLoaded = OrbitalBody.IsSaved = true;

				if (this.OrbitalBody.Attributes.Count == 0) {
					InitDefaultParameters ();
				} 
			} else {
				throw new Exception (res.ResultMsg);
			}
		}

		private void InitDefaultParameters()
		{
            List<OAttribute> attr = new List<OAttribute>();
            if (OrbitalBody is Planet)
            {
                Planet p = (Planet)OrbitalBody;

                //Population
                switch ((int)p.Size)
                {
                    case 0://Tiny
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BasePopulation, AttributeType = AttributeType.Attribute, Value = 2 });
                        break;
                    case 1://Small
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BasePopulation, AttributeType = AttributeType.Attribute, Value = 3 });
                        break;
                    case 2://Medium
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BasePopulation, AttributeType = AttributeType.Attribute, Value = 5 });
                        break;
                    case 3://Large
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BasePopulation, AttributeType = AttributeType.Attribute, Value = 8 });
                        break;
                    case 4://Huge
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BasePopulation, AttributeType = AttributeType.Attribute, Value = 13 });
                        break;
                }

                //Farming
                switch ((int)p.Type)
                {
                    case 0://Baren
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BaseFarming, AttributeType = AttributeType.Attribute, Value = 0 });
                        break;
                    case 1://Desert
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BaseFarming, AttributeType = AttributeType.Attribute, Value = 0 });
                        break;
                    case 2://Tundra
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BaseFarming, AttributeType = AttributeType.Attribute, Value = 0.5 });
                        break;
                    case 3://Arid
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BaseFarming, AttributeType = AttributeType.Attribute, Value = 0.5 });
                        break;
                    case 4://Swamp
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BaseFarming, AttributeType = AttributeType.Attribute, Value = 1 });
                        break;
                    case 5://Ocean
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BaseFarming, AttributeType = AttributeType.Attribute, Value = 1 });
                        break;
                    case 6://Terran
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BaseFarming, AttributeType = AttributeType.Attribute, Value = 2 });
                        break;
                    case 7://Gaia
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BaseFarming, AttributeType = AttributeType.Attribute, Value = 3 });
                        break;
                }

                //Production
                attr.Add(new OAttribute() { Attribute = ObjectAttribute.BaseProduction, AttributeType = AttributeType.Attribute, Value = 0.3 * (9 - p.OrbitNo) });

                //Research
                attr.Add(new OAttribute() { Attribute = ObjectAttribute.BaseResearch, AttributeType = AttributeType.Attribute, Value = HOO.Core.Model.Configuration.MrRandom.rnd.Next(6) / 2.0 });
            }

            if (OrbitalBody is AsteroidBelt)
            {
                AsteroidBelt a = (AsteroidBelt)OrbitalBody;

                switch ((int)a.Type)
                {
                    case 0://C_Type
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BaseCarbonMining, AttributeType = AttributeType.Attribute, Value = 0.3 * (int)a.Density * (9 - a.OrbitNo) });
                        break;
                    case 1://S_Type
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BaseSilicateMining, AttributeType = AttributeType.Attribute, Value = 0.3 * (int)a.Density * (9 - a.OrbitNo) });
                        break;
                    case 2://M_Type
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BaseMetalMining, AttributeType = AttributeType.Attribute, Value = 0.3 * (int)a.Density * (9 - a.OrbitNo) });
                        break;
                    case 3://V_Type
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BaseBasaltMining, AttributeType = AttributeType.Attribute, Value = 0.3 * (int)a.Density * (9 - a.OrbitNo) });
                        break;
                }
            }

            if (OrbitalBody is GasGiant)
            {
                GasGiant g = (GasGiant)OrbitalBody;

                switch ((int)g.Class)
                {
                    case 0://AmmoniaClouds
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BaseAmoniaExtraction, AttributeType = AttributeType.Attribute, Value = 0.3 * (int)g.Size * g.OrbitNo });
                        break;
                    case 1://WaterClouds
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BaseWaterExtraction, AttributeType = AttributeType.Attribute, Value = 0.3 * (int)g.Size * g.OrbitNo });
                        break;
                    case 2://Cloudless
                        //attr.Add(new OAttribute() { Attribute = ObjectAttribute.BaseAmoniaExtraction, AttributeType = AttributeType.Attribute, Value = 0.3 * (int)g.Size * g.OrbitNo });
                        break;
                    case 3://AlkaliMetals
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BaseCarbonExtraction, AttributeType = AttributeType.Attribute, Value = 0.1 * (int)g.Size * (9 - g.OrbitNo) });
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BaseMetalExtraction, AttributeType = AttributeType.Attribute, Value = 0.1 * (int)g.Size * (9 - g.OrbitNo) });
                        break;
                    case 4://SilicateClouds
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BaseSilicateExtraction, AttributeType = AttributeType.Attribute, Value = 0.1 * (int)g.Size * (9 - g.OrbitNo) });
                        attr.Add(new OAttribute() { Attribute = ObjectAttribute.BaseMetalExtraction, AttributeType = AttributeType.Attribute, Value = 0.1 * (int)g.Size * (9 - g.OrbitNo) });
                        break;
                }
            }
            
            //			this.OrbitalBody.Attributes.Load (attr);
            this.OrbitalBody.Attributes = attr;
			this.OrbitalBody.IsSaved = false;
		}

		public void Save()
		{
            log.Entry.MethodName = "Save";
            if (!this.OrbitalBody.IsSaved)
            {
                if (this.OrbitalBody.Attributes.Count == 0)
                {
                    InitDefaultParameters();
                }

                DBCommandResult res = _mdh.SaveOrbitalBody(this.OrbitalBody);
                if (res.ResultCode != 0)
                {
                    log.Error(new Exception(res.ResultMsg));
                    this.OrbitalBody = (StarOrbitalBody)res.Tag;
                }
            }
		}
	}
}

