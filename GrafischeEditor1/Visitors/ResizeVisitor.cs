using GrafischeEditor1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1.Visitors
{
    class ResizeVisitor : IVisitor
    {
        public int NewWidth { get; set; }
        public int NewHeight { get; set; }

        public ResizeVisitor(int newWidth, int newHeight)
        {
            this.NewWidth = newWidth;
            this.NewHeight = newHeight;
        }

        public void Visit(Group figure)
        {
            var tx = 0;
            var ty = 0;

            if (this.NewWidth < 0)
            {
                tx = this.NewWidth;
                this.NewWidth = Math.Abs(this.NewWidth);
            }

            if (this.NewHeight < 0)
            {
                ty = this.NewHeight;
                this.NewHeight = Math.Abs(this.NewHeight);
            }

            var x = figure.X;
            var y = figure.Y;

            var h = (double)(figure.Height);
            var w = (double)(figure.Width);

            double wratio = (double)this.NewWidth / w;
            double hratio = (double)this.NewHeight / h;

            foreach (Figure f in figure.Figures)
            {
                f.Width = (int)((double)f.Width * wratio);
                f.Height = (int)((double)f.Height * hratio);

                f.X = (int)(this.NewWidth * ((f.X - x) / w)) + x + tx;
                f.Y = (int)(this.NewHeight * ((f.Y - y) / h)) + y + ty;
            }
        }

        public void Visit(Ellipsis figure)
        {
            figure.Width = this.NewWidth;
            figure.Height = this.NewHeight;
        }

        public void Visit(Square figure)
        {
            figure.Width = this.NewWidth;
            figure.Height = this.NewHeight;
        }
    }
}
