using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OJTI2014
{
    public partial class Grafic : Form
    {
        List<Point> points = new List<Point>();
        public Grafic(List<Point> points)
        {
            InitializeComponent();
            this.points = points;
            this.DoubleBuffered = true;
        }
        public void UpdatePoints(List<Point> pts)
        {
            points = new List<Point>(pts); 
            this.Invalidate(); 
        }

        private void Grafic_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;

            int originX = 50;        
            int originY = h / 2;     

            Pen axPen = new Pen(Color.Black, 2);

            g.DrawLine(axPen, originX, originY, w, originY);

            g.DrawLine(axPen, originX, 0, originX, h);
            if (points.Count < 2)
                return;

            int maxX = points.Max(p => p.X);
            int maxY = points.Max(p => Math.Abs(p.Y)); 

            float scaleX = (float)(w - originX - 20) / maxX;
            float scaleY = (float)(h / 2 - 20) / maxY;

            Pen graphPen = new Pen(Color.Red, 2);

            for (int i = 1; i < points.Count; i++)
            {
                Point p1 = points[i - 1];
                Point p2 = points[i];

                Point drawP1 = new Point(originX + (int)(p1.X * scaleX), originY - (int)(p1.Y * scaleY));

                Point drawP2 = new Point(originX + (int)(p2.X * scaleX), originY - (int)(p2.Y * scaleY));

                g.DrawLine(graphPen, drawP1, drawP2);
            }
        }
    }
}
