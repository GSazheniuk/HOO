using System;
using System.Linq;
using HOO.Core.Model.Universe;
using HOO.Core.Configuration;
using HOO.DB;

namespace HOO.SvcLib.Helpers
{
	public class UniverseHelper
	{
		public Universe Universe { get; set; }
		private MySqlDBHelper _dh ;

		public UniverseHelper ()
		{
			this._dh = new MySqlDBHelper(SensitiveData.ConnectionString);
			this.Universe = new Universe ();
		}

		private void InitDefaultParameters(){

		}

		public void Save()
		{
			if (this.Universe.IsLoaded && !this.Universe.IsSaved) {
				DBCommandResult res = _dh.SaveUniverse (Universe);
				if (res.ResultCode == 0)
					this.Universe.IsSaved = true;
			}

			if (this.Universe.Galaxies.Exists (g => g.Stars.Exists (s => s.OrbitalBodies.Exists (ob => !ob.IsSaved)))) {
				Galaxy gal = this.Universe.Galaxies.First (g => g.Stars.Exists (s => s.OrbitalBodies.Exists (ob => !ob.IsSaved)));
				Star st = gal.Stars.First (s => s.OrbitalBodies.Exists (ob => !ob.IsSaved));
				StarOrbitalBody sob = st.OrbitalBodies.First (ob => !ob.IsSaved);
				StarOrbitalBodyHelper sobh = new StarOrbitalBodyHelper (sob);
				sobh.Save ();
			}
		}

		public void Load()
		{
			DBCommandResult res = _dh.LoadUniverse (Universe);
			if (res.ResultCode == 0) {
				this.Universe = (Universe)res.Tag;
				GalaxyHelper gh = new GalaxyHelper ();
				foreach (Galaxy g in this.Universe.Galaxies) {
					gh.Galaxy = g;
					gh.Load ();
				}
				this.Universe.IsLoaded =this.Universe.IsSaved = true;
				if (this.Universe.Attributes.TotalAttributes == 0)
					InitDefaultParameters ();
				//ELSE add missing attributes, if any exists.
			} else {
				throw new Exception (res.ResultMsg);
			}
		}

		public void Tick()
		{
			if (this.Universe.IsLoaded) {
				this.Universe.IsSaved = false;
				this.Universe.CurrentTick = (this.Universe.CurrentTick + 1) % 10;
				if (this.Universe.CurrentTick == 0) {
					this.Universe.CurrentTurn = (this.Universe.CurrentTurn + 1) % 100;
					if (this.Universe.CurrentTurn == 0) {
						this.Universe.CurrentPeriod++;
					}
					Save ();
				}
			}
		}
	}
}
