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

		public void InitDefaultSettings()
		{
//			this.Player.Attributes.TotalCredits = 1000;
//			this.Player.Requisites.Capitol = 1;
//			this.Player.Effects.Income = 0.01;
		}

		public void Register(string userName, string password, string email)
		{
			InitDefaultSettings ();
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
			} else {
				throw new Exception (res.ResultMsg);
			}
		}
	}
}