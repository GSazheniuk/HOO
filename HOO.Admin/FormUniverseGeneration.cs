﻿using HOO.Core.Model.Configuration;
using HOO.Core.Model.Universe;
using System;
using System.Windows.Forms;
using System.Linq;

namespace HOO.Admin
{
    public partial class FormUniverseGeneration : Form
    {
        Universe u;
        public FormUniverseGeneration()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            u = new Universe();
            u.Name = "Test universe";
            u.Descrip = "For testing purposes only...";
            for (int i = 0; i < nudGal.Value; i++)
            {
                Galaxy g = new Galaxy((int)nudGalX.Value, (int)nudGalY.Value, (int)nudGalZ.Value);
                g.Name = "Test Milky Way";
                int j = (int)nudStars.Value;
                while (j > 0)
                {
                    Star s = new Star(g);
                    s.StarSystemName = "Alpha Test";
                    if (g.AddStar(s, 10))
                    {
                        int orbits = MrRandom.rnd.Next(ConstantParameters.MaxOrbitalBodiesForStar);
                        for (int k = 0; k < orbits; k++)
                        {
                            int bodyType = MrRandom.rnd.Next(3);

                            switch (bodyType)
                            {
                                case 0:
                                    Planet p = new Planet(s);
                                    s.OrbitalBodies.Add(p);
                                    break;
                                case 1:
                                    AsteroidBelt a = new AsteroidBelt(s);
                                    s.OrbitalBodies.Add(a);
                                    break;
                                case 2:
                                    GasGiant gg = new GasGiant(s);
                                    s.OrbitalBodies.Add(gg);
                                    break;
                            }
                        }
                        j = j - 1;
                    }
                }
                u.Galaxies.Add(g);
            }
            ShowStats();
        }

        private void ShowStats()
        {
            lOrbital0.Text = u.Galaxies.Sum(x => x.Stars.Count(y => y.OrbitalBodies.Count == 0)).ToString();
            lOrbital12.Text = u.Galaxies.Sum(x => x.Stars.Count(y => y.OrbitalBodies.Count >= 1 && y.OrbitalBodies.Count <= 2)).ToString();
            lOrbital34.Text = u.Galaxies.Sum(x => x.Stars.Count(y => y.OrbitalBodies.Count >= 3 && y.OrbitalBodies.Count <= 4)).ToString();
            lOrbital57.Text = u.Galaxies.Sum(x => x.Stars.Count(y => y.OrbitalBodies.Count >= 5 && y.OrbitalBodies.Count <= 7)).ToString();
            lOrbital89.Text = u.Galaxies.Sum(x => x.Stars.Count(y => y.OrbitalBodies.Count >= 8 && y.OrbitalBodies.Count <= 9)).ToString();

            lTotalPlanets.Text = u.Galaxies.Sum(x => x.Stars.Sum(y => y.OrbitalBodies.Count(z => z is Planet))).ToString();
            lTotalBelts.Text = u.Galaxies.Sum(x => x.Stars.Sum(y => y.OrbitalBodies.Count(z => z is AsteroidBelt))).ToString();
            lTotalGiants.Text = u.Galaxies.Sum(x => x.Stars.Sum(y => y.OrbitalBodies.Count(z => z is GasGiant))).ToString();
            lTotalBodies.Text = u.Galaxies.Sum(x => x.Stars.Sum(y => y.OrbitalBodies.Count)).ToString();

            lPlanet0.Text =
                u.Galaxies.Sum(x => x.Stars.Count(y => y.OrbitalBodies.Count(z => z is Planet) == 0)).ToString();
            lPlanet12.Text = u.Galaxies.Sum(x => x.Stars.Count(y => y.OrbitalBodies.Count(z => z is Planet) >= 1 && y.OrbitalBodies.Count(z => z is Planet) <= 2)).ToString();
            lPlanet34.Text = u.Galaxies.Sum(x => x.Stars.Count(y => y.OrbitalBodies.Count(z => z is Planet) >= 3 && y.OrbitalBodies.Count(z => z is Planet) <= 4)).ToString();
            lPlanet57.Text = u.Galaxies.Sum(x => x.Stars.Count(y => y.OrbitalBodies.Count(z => z is Planet) >= 5 && y.OrbitalBodies.Count(z => z is Planet) <= 7)).ToString();
            lPlanet89.Text = u.Galaxies.Sum(x => x.Stars.Count(y => y.OrbitalBodies.Count(z => z is Planet) >= 8 && y.OrbitalBodies.Count(z => z is Planet) <= 9)).ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DateTime st = DateTime.Now;
            DBHelper dh = new DBHelper(tbConnStr.Text);
            DBCommandResult r = dh.AddUniverse(u);
            if (r.ResultCode < 0)
            {
                MessageBox.Show(r.ResultMsg, String.Format("Universe not saved - Code:{0}", r.ResultCode));
                return;
            }
            else
            {
                Universe ru = (Universe)r.Tag;
                foreach (Galaxy g in u.Galaxies)
                {
                    g.Universe = ru;
                    r = dh.AddGalaxy(g);
                    if (r.ResultCode < 0)
                    {
                        MessageBox.Show(r.ResultMsg, String.Format("Galaxy not saved - Code:{0}", r.ResultCode));
                        return;
                    }
                    else
                    {
                        Galaxy rg = (Galaxy)r.Tag;
                        foreach (Star s in g.Stars)
                        {
                            s.Galaxy = rg;
                            r = dh.AddStar(s);
                            if (r.ResultCode < 0)
                            {
                                MessageBox.Show(r.ResultMsg, String.Format("Star not saved - Code:{0}", r.ResultCode));
                                return;
                            }
                            else
                            {
                                Star rs = (Star)r.Tag;
                                rg.Stars.Add(rs);
                            }
                        }
                        ru.Galaxies.Add(rg);
                    }
                }
                u = ru;
            }
            MessageBox.Show((DateTime.Now - st).TotalMilliseconds.ToString(), "Data saved successfully");
            ShowStats();
        }
    }
}
