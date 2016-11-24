﻿using System;
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

        public new int X {
            get
            {
                if (this.Figures.Count <= 0) return 0;
                var fig = this.Figures.OrderBy(x => x.X).FirstOrDefault();
                if (fig == null) return 0;
                return fig.X;
            }
            set
            {
                foreach (var fig in Figures)
                    fig.X += value;
            }
        }

        public new int Y
        {
            get
            {
                if (this.Figures.Count <= 0) return 0;
                var fig = this.Figures.OrderBy(x => x.Y).FirstOrDefault();
                if (fig == null) return 0;
                return fig.Y;
            }
            set
            {
                foreach (var fig in Figures)
                    fig.Y += value;
            }
        }

        public new int Width
        {
            get
            {
                if (this.Figures.Count <= 0) return 0;
                if (this.Figures.Count == 1) return this.Figures[0].Width;

                var low = this.Figures.OrderBy(x => x.X + x.Width).FirstOrDefault();
                if (low == null) return 0;

                var high = this.Figures.OrderBy(x => x.X + x.Height).LastOrDefault();
                if (high == null) return 0;

                return (high.X + high.Width) - low.X;
            }
            set
            {
            }
        }

        public new int Height
        {
            get
            {
                if (this.Figures.Count <= 0) return 0;
                if (this.Figures.Count == 1) return this.Figures[0].Width;

                var low = this.Figures.OrderBy(x => x.X + x.Height).FirstOrDefault();
                if (low == null) return 0;

                var high = this.Figures.OrderBy(x => x.X + x.Height).LastOrDefault();
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
            foreach (Figure figure in this.Figures)
            {
                figure.Draw(g);
            }

            Figure preview = new Square(this.X, this.Y, this.Width, this.Height) { Selected = this.Selected };
            Figure.DrawSelection(g, preview);
        }

        public override void Select(int x, int y)
        {
            this.Selected = false;

            var found = false;
            foreach (var fig in this.Figures)
            {
                fig.Select(x, y);
                if (fig.Selected == true) found = true;
            }
                       
            if (found == false && x >= this.X && x <= (this.X + this.Width) && y >= this.Y && y <= (this.Y + this.Height))
                this.Selected = true;
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
            var result = String.Empty;
            result += String.Format("group {0}", this.Figures.Count) + Environment.NewLine;

            foreach (Figure figure in this.Figures)
                result += figure.ToString() + Environment.NewLine;

            return result;
        }
    }
}
