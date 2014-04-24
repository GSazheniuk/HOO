using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HOO.Admin
{
    public partial class Form1 : Form
    {
        Random rnd;
        List<Point3D> l = new List<Point3D>();
        int a = 0;
        int b = 0;
        int c = 0;

        public class Point3D
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Point3D p in l)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.Black), p.X + a, p.Y + b, 1, 1);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            foreach (Point3D p in l)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.Black), p.Y + b, p.Z + c, 1, 1);
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            foreach (Point3D p in l)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.Black), p.Z + c, p.X + a, 1, 1);
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            rnd = new Random();
            int n = int.Parse(tbN.Text);
            a = int.Parse(tbX.Text);
            b = int.Parse(tbY.Text);
            c = int.Parse(tbZ.Text);
            l = new List<Point3D>();

            while (n > 0)
            {
                int x = ((rnd.Next(2) == 0) ? 1 : -1) * rnd.Next(a);
                int y = ((rnd.Next(2) == 0) ? 1 : -1) * rnd.Next(b);
                int z = ((rnd.Next(2) == 0) ? 1 : -1) * rnd.Next(c);
                double d = Math.Pow(x, 2) / Math.Pow(a, 2) + Math.Pow(y, 2) / Math.Pow(b, 2) + Math.Pow(z, 2) / Math.Pow(c, 2);

                if (d <= 1)
                {
                    l.Add(new Point3D { X = x, Y = y, Z = z });
                    n--;
                }
            }

            panel1.Invalidate();
            panel2.Invalidate();
            panel3.Invalidate();
        }
    }
}
