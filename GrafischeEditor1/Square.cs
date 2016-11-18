using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1
{
    public class Square : Figure
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Square(int x, int y, int width, int height) : base(x, y)
        {
            this.Width = width;
            this.Height = height;
        }

        public override void Draw(Graphics g)
        {
            var brush = new SolidBrush(Color.Green);
            var pen = new Pen(Color.Red);

            var rectangle = new System.Drawing.Rectangle(this.X, this.Y, this.Width, this.Height);

            g.FillRectangle(brush, rectangle);
            if(this.Selected) g.DrawRectangle(pen, rectangle);
        }
    }
}
