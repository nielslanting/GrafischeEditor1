using GrafischeEditor1.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1.Strategy
{
    public class DrawEllipsis : IStrategy
    {
        private static DrawEllipsis _drawEllipsis = null;
        public static DrawEllipsis Instance {
            get
            {
                if (_drawEllipsis == null)
                    _drawEllipsis = new DrawEllipsis();

                return _drawEllipsis;
            }
        }

        protected DrawEllipsis()
        {
        }

        public void Draw(Graphics g, bool v, int x, int y, int w, int h)
        {
            if (!v) return;

            var brush = new SolidBrush(Color.Blue);
            var rectangle = new System.Drawing.Rectangle(x, y, w, h);
            g.FillEllipse(brush, rectangle);
        }

        public string ToString(int x, int y, int w, int h)
        {
            return String.Format("ellipse {0} {1} {2} {3}", x, y, w, h);
        }
    }
}
