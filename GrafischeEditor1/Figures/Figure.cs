using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1
{
    public abstract class Figure : IDrawable
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;

        private int _width, _height;

        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (value < 0) this.X += value;
                _width = Math.Abs(value);
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                if (value < 0) this.Y += value;
                _height = Math.Abs(value);
            }
        }

        public bool Selected { get; set; } = false;

        protected Figure(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public abstract void Draw(Graphics g);

        public static void DrawSelection(Graphics g, Figure f)
        {
            if (f.Selected == false) return;

            var pen = new Pen(Color.Red);
            var brush = new SolidBrush(Color.Red);
            const int SELECTION_WIDTH = 5;

            var rectangle = new System.Drawing.Rectangle(f.X, f.Y, f.Width, f.Height);
            var tl = new Rectangle(f.X, f.Y, SELECTION_WIDTH, SELECTION_WIDTH);
            var tr = new Rectangle(f.X + f.Width - SELECTION_WIDTH + 1, f.Y, SELECTION_WIDTH, SELECTION_WIDTH);
            var bl = new Rectangle(f.X, f.Y + f.Height - SELECTION_WIDTH + 1, SELECTION_WIDTH, SELECTION_WIDTH);
            var br = new Rectangle(f.X + f.Width - SELECTION_WIDTH + 1, f.Y + f.Height - SELECTION_WIDTH + 1, SELECTION_WIDTH, SELECTION_WIDTH);

            g.DrawRectangle(pen, rectangle);
            g.FillRectangle(brush, tl);
            g.FillRectangle(brush, tr);
            g.FillRectangle(brush, bl);
            g.FillRectangle(brush, br);
        }
    }
}
