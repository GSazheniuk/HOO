using System;
using HOO.Core.Model.Universe;
using HOO.Core.Configuration;
using HOO.DB;

namespace HOO.SvcLib.Helpers
{
	public class UniverseHelper
	{
		public Universe Universe { get; set; }
		private MySqlDBHelper _dh ;
		private bool _isLoaded = false;
		private bool _isSaved = false;

		public UniverseHelper ()
		{
			this._dh = new MySqlDBHelper(SensitiveData.ConnectionString);
			this.Universe = new Universe ();
		}

		public void Save()
		{
			if (_isLoaded && !_isSaved) {
				DBCommandResult res = _dh.SaveUniverse (Universe);
				_isSaved = true;
			}
		}

		public void Load()
		{
			DBCommandResult res = _dh.LoadUniverse (Universe);
			if (res.ResultCode == 0) {
				this.Universe = (Universe)res.Tag;
				_isLoaded = true;
				_isSaved = true;
			} else {
				throw new Exception (res.ResultMsg);
			}
		}

		public void Tick()
		{
			DBCommandResult res = new DBCommandResult ();
			if (_isLoaded) {
				res = _dh.EndTurn (this.Universe.Id);
				if (res.ResultCode == 0)
					_isSaved = false;
			}
		}
	}
}

