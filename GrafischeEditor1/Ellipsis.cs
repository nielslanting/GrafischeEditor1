using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1
{
    public class Ellipsis : Figure
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Ellipsis(int x, int y, int width, int height) : base(x, y)
        {
            this.Width = width;
            this.Height = height;
        }

        public override void Draw(Graphics g)
        {
            var brush = new SolidBrush(Color.Blue);
            var pen = new Pen(Color.Red);

            var rectangle = new System.Drawing.Rectangle(this.X, this.Y, this.Width, this.Height);

            g.FillEllipse(brush, rectangle);
            if (this.Selected) g.DrawEllipse(pen, rectangle);
        }
    }
}
