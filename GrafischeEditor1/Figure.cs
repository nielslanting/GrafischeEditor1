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
        public bool Selected { get; set; } = false;

        protected Figure(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public abstract void Draw(Graphics g);
    }
}
