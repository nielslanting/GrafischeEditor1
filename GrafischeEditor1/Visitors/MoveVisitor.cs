using GrafischeEditor1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1.Visitors
{
    class MoveVisitor : IVisitor
    {
        public int DX { get; set; }
        public int DY { get; set; }

        public MoveVisitor(int dx, int dy)
        {
            this.DX = dx;
            this.DY = dy;            
        }

        public void Visit(Group figure)
        {
            foreach (var fig in figure.Figures)
            {
                fig.X += this.DX;
                fig.Y += this.DY;
            }
        }

        public void Visit(Ellipsis figure)
        {
            figure.X += this.DX;
            figure.Y += this.DY;
        }

        public void Visit(Square figure)
        {
            figure.X += this.DX;
            figure.Y += this.DY;
        }
    }
}
