using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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
                figure.Draw(g);
        }
    }
}
