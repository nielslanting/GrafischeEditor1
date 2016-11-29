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
        Figure SelectedFigure;
        int X = 0, Y = 0;
        int oldWidth = 0, oldHeight = 0, oldX = 0, oldY = 0;

        public ResizeFigureCommand()
        {
            this.Figures = new List<Figure>();
        }

        public ResizeFigureCommand(int x, int y, List<Figure> figures, Figure selectedFigure)
        {
            this.Figures = figures;
            this.SelectedFigure = selectedFigure;
            this.X = x;
            this.Y = y;
        }

        public List<Figure> Execute()
        {
            if (SelectedFigure == null) return this.Figures;

            this.oldX = this.SelectedFigure.X;
            this.oldY = this.SelectedFigure.Y;

            int newWidth = this.X - this.SelectedFigure.X;
            int newHeight = this.Y - this.SelectedFigure.Y;

            this.oldWidth = this.SelectedFigure.Width;
            this.oldHeight = this.SelectedFigure.Height;

            this.SelectedFigure.Width = newWidth;
            this.SelectedFigure.Height = newHeight;

            return this.Figures;
        }

        public List<Figure> Undo()
        {
            this.SelectedFigure.X = this.oldX;
            this.SelectedFigure.Y = this.oldY;
            this.SelectedFigure.Width = this.oldWidth;
            this.SelectedFigure.Height = this.oldHeight;

            return this.Figures;
        }
    }
}
