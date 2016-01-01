using System;
using HOO.Core.Model;
using HOO.DB;
using System.Collections.Generic;

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
			this.Player = new Player ();
		}

		public void InitDefaultParameters()
		{
            //			this.Player.Attributes.TotalCredits = 1000;
            //			this.Player.Requisites.Capitol = 1;
            //			this.Player.Effects.Income = 0.01;
            //			List<OAttribute> attrs = new OAttribute[3];
            this.Player.Attributes.Add(new OAttribute() { Attribute = ObjectAttribute.NativeCredits, AttributeType = AttributeType.Resource, Value = 1000 });
            this.Player.Attributes.Add(new OAttribute() { Attribute = ObjectAttribute.NativeCredits, AttributeType = AttributeType.ResourceFlatChange, Value = 1 });
            this.Player.Attributes.Add(new OAttribute() { Attribute = ObjectAttribute.Capitol, AttributeType = AttributeType.FiniteRequisite, Value = 1 });
			//this.Player.Attributes.Load (attrs);
			//this.Player.Attributes.AddAttribute (attrs [0]);
			//this.Player.Attributes.AddAttribute (attrs [1]);
			//this.Player.Attributes.AddAttribute (attrs [2]);
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

		public void Tick()
		{
			if (this.Player != null) {
				//foreach (OAttribute oa in this.Player.Attributes.ListByType(AttributeType.Resource)) {
				//	oa.Value = (double)oa.Value + ((double)this.Player.Attributes.ValueOf (ObjectAttribute.NativeCredits, AttributeType.ResourceFlatChange)) / 10;
				//}
			}
		}
	}
}