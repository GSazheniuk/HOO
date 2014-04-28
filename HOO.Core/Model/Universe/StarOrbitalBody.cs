﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOO.Core.Model.Universe
{
    public class StarOrbitalBody
    {
        public int OrbitNo { get; set; }
        public Star Star { get; set; }

        public StarOrbitalBody(Star s)
        {
            this.Star = s;
        }
    }
}