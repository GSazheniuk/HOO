using System;
using HOO.Core.Model;
using HOO.DB;
using System.Collections.Generic;
using System.Linq;

namespace HOO.SvcLib.Helpers
{
	public class PlayerHelper
	{
		public Player Player { get; set; }
		private MySqlDBHelper _dh ;
        private MongoDBHelper _mdh;

		public PlayerHelper ()
		{
            //this._dh = new MySqlDBHelper(SensitiveData.ConnectionString);
            this._mdh = new MongoDBHelper();
            this.Player = new Player();
		}

		public void InitDefaultParameters()
		{
            //			this.Player.Attributes.TotalCredits = 1000;
            //			this.Player.Requisites.Capitol = 1;
            //			this.Player.Effects.Income = 0.01;
            //			List<OAttribute> attrs = new OAttribute[3];
            this.Player.Attributes.Add(new OAttribute() { Attribute = ObjectAttribute.NativeCredits, AttributeType = AttributeType.Resource, Value = 1000M });
            this.Player.Attributes.Add(new OAttribute() { Attribute = ObjectAttribute.NativeCredits, AttributeType = AttributeType.ResourceFlatChange, Value = 1M });
            this.Player.Attributes.Add(new OAttribute() { Attribute = ObjectAttribute.Capitol, AttributeType = AttributeType.FiniteRequisite, Value = 1M });
			//this.Player.Attributes.Load (attrs);
			//this.Player.Attributes.AddAttribute (attrs [0]);
			//this.Player.Attributes.AddAttribute (attrs [1]);
			//this.Player.Attributes.AddAttribute (attrs [2]);
		}

        public List<Player> AllPlayers()
        {
            DBCommandResult res = _mdh.AllPlayers();

            if (res.ResultCode == 0)
            {
                return (List<Player>)res.Tag;
            }
            else
            {
                throw new Exception(res.ResultMsg);
            }
        }

		public void Register()
		{
			InitDefaultParameters ();
			DBCommandResult res = _mdh.AddNewPlayer (this.Player);

			if (res.ResultCode == 0) {
				this.Player = (Player)res.Tag;
				this.Player.IsLoaded = this.Player.IsSaved = true;
			} else {
				throw new Exception (res.ResultMsg);
			}
		}

		public void AuthUser(string userName, string password)
		{
			DBCommandResult res = _mdh.AuthPlayer (userName, password);

			if (res.ResultCode == 0) {
				this.Player = (Player)res.Tag;

				if (this.Player.Attributes.Count == 0)
					InitDefaultParameters ();
				//ELSE add missing attributes, if any exists.
			} else {
				throw new Exception (res.ResultMsg);
			}
		}

        public void Save()
        {
            DBCommandResult res = _mdh.SavePlayer(this.Player);

            if (res.ResultCode == 0)
            {
                this.Player.IsSaved = true;
            }
            else
            {
                throw new Exception(res.ResultMsg);
            }
        }

        public void Tick()
		{
			if (this.Player != null) {
                var resources = this.Player.Attributes.Where(x => x.AttributeType == AttributeType.Resource);
                var changes = this.Player.Attributes.Where(x => x.AttributeType == AttributeType.ResourceFlatChange);

                if (resources == null || changes == null)
                {
                    InitDefaultParameters();
                    return;
                }

                var r = resources.ToArray();

                for (int i = 0; i<r.Count();i++)
                {
                    if (changes.FirstOrDefault(x => x.Attribute == r[i].Attribute && x.Value != null) != null)
                    {
                        r[i].Value = Convert.ToDecimal(r[i].Value) + changes.Where(x => x.Attribute == r[i].Attribute && x.Value != null).Sum(x => Convert.ToDecimal(x.Value)) / 10;
                    }
                }
                this.Player.IsSaved = false;
				//foreach (OAttribute oa in this.Player.Attributes.ListByType(AttributeType.Resource)) {
				//	oa.Value = (double)oa.Value + ((double)this.Player.Attributes.ValueOf (ObjectAttribute.NativeCredits, AttributeType.ResourceFlatChange)) / 10;
				//}
			}
		}
	}
}