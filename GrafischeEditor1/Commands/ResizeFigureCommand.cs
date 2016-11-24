using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1.Commands
{
    class ResizeFigureCommand : ICommand<List<Figure>>
    {
        List<Figure> Figures;
        int X = 0, Y = 0;
        int oldWidth = 0, oldHeight = 0;
        Figure oldFigure = null;

        public ResizeFigureCommand()
        {
            this.Figures = new List<Figure>();
        }

        public ResizeFigureCommand(int x, int y, List<Figure> figures)
        {
            this.Figures = figures;
            this.X = x;
            this.Y = y;
        }

        public List<Figure> Execute()
        {
            var figure = this.Figures.Find(x => x.Selected == true);
            if (this.oldFigure != null) figure = this.oldFigure;
            if (figure == null) return this.Figures;

            int newWidth = this.X - figure.X;
            int newHeight = this.Y - figure.Y;

            this.oldFigure = figure;
            this.oldWidth = figure.Width;
            this.oldHeight = figure.Height;

            figure.Width = newWidth;
            figure.Height = newHeight;

            return this.Figures;
        }

        public List<Figure> Undo()
        {      
            this.oldFigure.Width = this.oldWidth;
            this.oldFigure.Height = this.oldHeight;

            return this.Figures;
        }
    }
}
