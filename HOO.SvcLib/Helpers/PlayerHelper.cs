using System;
using HOO.Core.Model;
using HOO.Core.Model.Universe;
using HOO.Core.Configuration;
using HOO.DB;

namespace HOO.SvcLib.Helpers
{
	public class PlayerHelper
	{
		public Player Player { get; set; }
		private MySqlDBHelper _dh ;

		public PlayerHelper ()
		{
			this._dh = new MySqlDBHelper(SensitiveData.ConnectionString);
			this.Player = new Player ();
		}

		public void InitDefaultParameters()
		{
//			this.Player.Attributes.TotalCredits = 1000;
//			this.Player.Requisites.Capitol = 1;
//			this.Player.Effects.Income = 0.01;
			OAttribute[] attrs = new OAttribute[3];
			attrs [0] = new OAttribute (ObjectAttribute.NativeCredits, AttributeTypes.Resource, 1000);
			attrs [1] = new OAttribute (ObjectAttribute.NativeCredits, AttributeTypes.ResourceFlatChange, 1);
			attrs [2] = new OAttribute (ObjectAttribute.Capitol, AttributeTypes.FiniteRequisite, 1);
			this.Player.Attributes.Load (attrs);
			this.Player.Attributes.AddAttribute (attrs [0]);
			this.Player.Attributes.AddAttribute (attrs [1]);
			this.Player.Attributes.AddAttribute (attrs [2]);
		}

		public void Register(string userName, string password, string email)
		{
			InitDefaultParameters ();
			DBCommandResult res = _dh.AddNewPlayer (userName, password, email, this.Player);

			if (res.ResultCode == 0) {
				this.Player = (Player)res.Tag;
				this.Player.IsLoaded = this.Player.IsSaved = true;
			} else {
				throw new Exception (res.ResultMsg);
			}
		}

		public void AuthUser(string userName, string password)
		{
			DBCommandResult res = _dh.AuthPlayer (userName, password);

			if (res.ResultCode == 0) {
				this.Player = (Player)res.Tag;
				this.Player.IsLoaded = this.Player.IsSaved = true;
				if (this.Player.Attributes.TotalAttributes == 0)
					InitDefaultParameters ();
				//ELSE add missing attributes, if any exists.
			} else {
				throw new Exception (res.ResultMsg);
			}
		}

		public void Tick()
		{
			if (this.Player != null) {
				foreach (OAttribute oa in this.Player.Attributes.ListByType(AttributeTypes.Resource)) {
					oa.Value = (double)oa.Value + ((double)this.Player.Attributes.ValueOf (ObjectAttribute.NativeCredits, AttributeTypes.ResourceFlatChange)) / 10;
				}
			}
		}
	}
}