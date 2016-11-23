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

            Figure.DrawSelection(g, this);
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
