using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GrafischeEditor1
{
    public class Square : Figure
    {
        private int _width, _height;
        public override int Width
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

        public override int Height
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

        public Square(int x, int y, int width, int height) : base(x, y)
        {
            this.Width = width;
            this.Height = height;
        }

        public override void Draw(Graphics g)
        {
            if (!this.Visible) return;

            var brush = new SolidBrush(Color.Green);
            var rectangle = new System.Drawing.Rectangle(this.X, this.Y, this.Width, this.Height);
            g.FillRectangle(brush, rectangle);

            Figure.DrawSelection(g, this);
        }

        public override Figure Select(int x, int y)
        {          
            if (x >= this.X && x <= (this.X + this.Width) && y >= this.Y && y <= (this.Y + this.Height))
            {
                this.Selected = !this.Selected;
                return this;
            } else this.Selected = false;

            return null;  
        }

        public static Square FromString(string input)
        {
            Regex r = new Regex("rectangle [0-9]+ [0-9]+ [0-9]+ [0-9]+");

            if (r.Match(input).Value != input) return null;

            var splits = input.Split(' ');

            int x = Convert.ToInt32(splits[1]);
            int y = Convert.ToInt32(splits[2]);
            int w = Convert.ToInt32(splits[3]);
            int h = Convert.ToInt32(splits[4]);

            return new Square(x, y, w, h);
        }

        public override string ToString()
        {
            return String.Format("rectangle {0} {1} {2} {3}", this.X, this.Y, this.Width, this.Height);
        }
    }
}
