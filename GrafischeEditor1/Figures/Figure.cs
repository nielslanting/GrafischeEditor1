using GrafischeEditor1.Interfaces;
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

        public virtual IStrategy Strategy { get; set; }

        public bool Visible { get; set; } = true;

        public void ToggleVisibility()
        {
            this.Visible = !this.Visible;
        }

        public virtual bool Selected { get; set; } = false;

        public Figure(int x, int y, IStrategy strategy)
        {
            this.X = x;
            this.Y = y;
            this.Strategy = strategy;
        }

        public virtual void Draw(Graphics g)
        {
            this.Strategy.Draw(g, this.Visible, this.X, this.Y, this.Width, this.Height);
            Figure.DrawSelection(g, this);
        }

        public abstract Figure Select(int x, int y);

        public virtual void Select()
        {
            this.Selected = true;
        }

        public virtual void Select(Figure f)
        {
            if(f.X == this.X && f.Y == this.Y && 
                f.Width == this.Width && f.Height == this.Height && 
                f.GetType() == this.GetType())
            {
                this.Selected = true;
            }               
        }

        public virtual void Unselect()
        {
            this.Selected = false;
        }

        public virtual Figure GetSelected()
        {
            var result = new List<Figure>();

            if (this.Selected == true)
                result.Add(this);

            if (result.Count == 1) return result.FirstOrDefault();
            else if(result.Count > 1) return new Group(0, 0, result);

            return null;
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

        public abstract void Visit(IVisitor visitor);

        public virtual IEnumerable<Figure> Enumerate()
        {
            return new List<Figure>() { this };
        }        
    }
}
