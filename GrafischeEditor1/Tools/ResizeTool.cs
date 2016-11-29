using GrafischeEditor1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafischeEditor1.Tools
{
    public class ResizeTool : IToolState
    {
        private Figure GetSelectedFigure(List<Figure> figures)
        {
            foreach (var fig in figures)
            {
                var f = fig.GetSelected();
                if (f != null) return f;
            }

            return null;
        }

        public void MouseClick(List<Figure> figures, MouseState mouseState)
        {
            Figure selectedFigure = GetSelectedFigure(figures);
            if (selectedFigure == null) return;

            int nw = 0, nh = 0;
            nw = mouseState.SX - selectedFigure.X;
            nh = mouseState.SY - selectedFigure.Y;

            selectedFigure.Resize(nw, nh);
        }

        public void MouseDown(List<Figure> figures, MouseState mouseState)
        {
        }

        public void MouseMove(List<Figure> figures, MouseState mouseState)
        {
        }

        public void MouseUp(List<Figure> figures, MouseState mouseState)
        {
        }
    }
}
