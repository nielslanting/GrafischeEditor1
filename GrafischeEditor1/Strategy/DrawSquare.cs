using GrafischeEditor1.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1.Strategy
{
    public class DrawSquare : IStrategy
    {
        private static DrawSquare _drawSquare = null;
        public static DrawSquare Instance {
            get
            {
                if (_drawSquare == null)
                    _drawSquare = new DrawSquare();

                return _drawSquare;
            }
        }

        protected DrawSquare()
        {
        }

        public void Draw(Graphics g, bool v, int x, int y, int w, int h)
        {
            if (!v) return;

            var brush = new SolidBrush(Color.Green);
            var rectangle = new System.Drawing.Rectangle(x, y, w, h);
            g.FillRectangle(brush, rectangle);
        }

        public string ToString(int x, int y, int w, int h)
        {
            return String.Format("rectangle {0} {1} {2} {3}", x, y, w, h);
        }
    }
}
