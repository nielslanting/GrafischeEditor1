using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GrafischeEditor1
{    
    public class Group : Figure
    {

        public List<Figure> Figures { get; set; }

        public override int X {
            get
            {
                if (this.Figures == null || this.Figures.Count <= 0) return 0;

                var fig = this.Figures.OrderBy(x => x.X).FirstOrDefault();
                if (fig == null) return 0;
                return fig.X;
            }
            set
            {
                if (Figures == null) return;
                
                foreach (var fig in Figures)
                    fig.X += value;
            }
        }

        public override int Y
        {
            get
            {
                if (this.Figures == null || this.Figures.Count <= 0) return 0;
                var fig = this.Figures.OrderBy(x => x.Y).FirstOrDefault();
                if (fig == null) return 0;
                return fig.Y;
            }
            set
            {
                if (this.Figures == null) return;

                foreach (var fig in Figures)
                    fig.Y += value;
            }
        }

        public override int Width
        {
            get
            {
                if (this.Figures.Count <= 0) return 0;
                if (this.Figures.Count == 1) return this.Figures[0].Width;

                var low = this.Figures.OrderBy(x => x.X).FirstOrDefault();
                if (low == null) return 0;

                var high = this.Figures.OrderBy(x => x.X + x.Width).LastOrDefault();
                if (high == null) return 0;

                return (high.X + high.Width) - low.X;
            }
            set
            {
            }
        }

        public override int Height
        {
            get
            {
                if (this.Figures.Count <= 0) return 0;
                if (this.Figures.Count == 1) return this.Figures[0].Height;

                var low = this.Figures.OrderBy(x => x.Y).FirstOrDefault();
                if (low == null) return 0;

                var high = this.Figures.OrderBy(x => x.Y + x.Height).LastOrDefault();
                if (high == null) return 0;

                return (high.Y + high.Height) - low.Y;
            }
            set
            {
            }
        }

        public Group(int x, int y, List<Figure> figures) : base(x, y)
        {
            this.Figures = figures;
        }

        public override void Draw(Graphics g)
        {
            if (!this.Visible) return;

            foreach (Figure figure in this.Figures)
            {
                figure.Draw(g);
            }

            Figure preview = new Square(this.X, this.Y, this.Width, this.Height) { Selected = this.Selected };
            Figure.DrawSelection(g, preview);
        }

        public override Figure Select(int x, int y)
        {            
            var found = false;
            var reversedFigures = new List<Figure>(this.Figures);
            reversedFigures.Reverse();
            
            foreach (var fig in reversedFigures)
            {
                if (fig.Select(x, y) != null) return fig;
            }
                       
            if (found == false && x >= this.X && x <= (this.X + this.Width) && y >= this.Y && y <= (this.Y + this.Height))
            {
                this.Selected = !this.Selected;
                return this;
            } else this.Selected = false;

            return null;
        }

        public override void Unselect()
        {
            this.Selected = false;
            foreach (Figure f in this.Figures) f.Unselect();
        }

        public override Figure GetSelected()
        {
            if (this.Selected == true) return this;

            var result = new List<Figure>();
            foreach (var fig in this.Figures)
            {
                var s = fig.GetSelected();
                if(s != null)
                    result.Add(s);
            }

            if (result.Count == 1) return result.FirstOrDefault();
            else if (result.Count > 1) return new Group(0, 0, result);

            return null;
        }

        public override void Move(int rx, int ry)
        {
            foreach (var fig in this.Figures)
            {
                fig.X += rx;
                fig.Y += ry;
                //fig.Move(rx, ry);
            }          
        }

        public override void Resize(int nw, int nh)
        {
            var tx = 0;
            var ty = 0;

            if (nw < 0)
            {
                tx = nw;
                nw = Math.Abs(nw);
            }

            if (nh < 0)
            {
                ty = nh;
                nh = Math.Abs(nh);
            }

            var x = this.X;
            var y = this.Y;

            var h = (double)(this.Height);
            var w = (double)(this.Width);

            double wratio = (double)nw / w;
            double hratio = (double)nh / h;

            foreach (Figure f in this.Figures)
            {
                f.Width = (int)((double)f.Width * wratio);
                f.Height = (int)((double)f.Height * hratio);

                f.X = (int)(nw * ((f.X - x) / w)) + x + tx;
                f.Y = (int)(nh * ((f.Y - y) / h)) + y + ty;
            }

            //base.Resize(nw, nh);
        }

        public static Group FromString(string input)
        {
            Regex r = new Regex("group [0-9]+");

            if (r.Match(input).Value != input) return null;

            int count = Convert.ToInt32(input.Split(' ').LastOrDefault());

            return new Group(0, 0, new List<Figure>(count));
        }

        public override string ToString()
        {
            return String.Format("group {0}", this.Figures.Count);
        }

        public IEnumerable<Figure> Enumerate()
        {
            var flat = new List<Figure>() { this };

            foreach(Figure f in this.Figures)
            {
                flat.Add(f);

                if (f is Group)
                    flat.AddRange(((Group)f).Enumerate().Skip(1));
            }

            return flat;
        }

        /// <summary>
        /// Removes an object from a group and returns it parent
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public Group Remove(Figure f)
        {
            if (this.Figures.Contains(f))
            {
                this.Figures.Remove(f);
                return this;
            }

            foreach(Figure figure in this.Figures)
            {
                if (figure is Group)
                    return ((Group)figure).Remove(f);
            }

            return null;
        }

        public void Replace(Figure f, Figure n)
        {
            if (this.Figures.Contains(f))
            {
                this.Figures.Remove(f);
                this.Figures.Add(n);
            }

            foreach (Figure figure in this.Figures)
            {
                if (figure is Group)
                    ((Group)figure).Remove(f);
            }
        }
    }
}
