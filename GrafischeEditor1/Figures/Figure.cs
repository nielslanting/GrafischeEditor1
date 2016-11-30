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
        public virtual int X { get; set; } = 0;
        public virtual int Y { get; set; } = 0;

        public abstract int Width { get; set; }
        public abstract int Height { get; set; }

        public virtual bool Selected { get; set; } = false;

        protected Figure(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public abstract void Draw(Graphics g);

        public abstract Figure Select(int x, int y);

        public virtual void Unselect()
        {
            this.Selected = false;
        }

        public virtual Figure GetSelected()
        {
            if (this.Selected == true) return this;
            return null;
        }

        public virtual void Move(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public virtual void Resize(int nw, int nh)
        {
            this.Width = nw;
            this.Height = nh;
        }

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
