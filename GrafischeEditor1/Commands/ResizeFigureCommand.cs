using GrafischeEditor1.Visitors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1.Commands
{
    class ResizeFigureCommand : ICommand<Figure>
    {
        Figure Figure;
        Figure SelectedFigure;
        int X = 0, Y = 0;
        int oldWidth = 0, oldHeight = 0, oldX = 0, oldY = 0;

        public ResizeFigureCommand()
        {
        }

        public ResizeFigureCommand(int x, int y, Figure figure, Figure selectedFigure)
        {
            this.Figure = figure;
            this.SelectedFigure = selectedFigure;
            this.X = x;
            this.Y = y;
        }

        public Figure Execute()
        {
            if (SelectedFigure == null) return this.Figure;


            int newWidth = this.X - this.SelectedFigure.X;
            int newHeight = this.Y - this.SelectedFigure.Y;

            this.oldWidth = this.SelectedFigure.Width;
            this.oldHeight = this.SelectedFigure.Height;

            int bx = this.SelectedFigure.X;
            int by = this.SelectedFigure.Y;

            ResizeVisitor resizeVisitor = new ResizeVisitor(newWidth, newHeight);
            this.SelectedFigure.Visit(resizeVisitor);

            this.oldX = this.SelectedFigure.X - bx;
            this.oldY = this.SelectedFigure.Y - by;

            return this.Figure;
        }

        public Figure Undo()
        {
            ResizeVisitor resizeVisitor = new ResizeVisitor(this.oldWidth, this.oldHeight);
            this.SelectedFigure.Visit(resizeVisitor);

            MoveVisitor moveVisitor = new MoveVisitor(this.oldX * -1, this.oldY * -1);
            this.SelectedFigure.Visit(moveVisitor);

            return this.Figure;
        }

        public override string ToString()
        {
            var selected = String.Empty;
            if(this.SelectedFigure != null) 
                selected = this.SelectedFigure.ToString();

            return String.Format("ResizeFigure: {0}", selected);
        }
    }
}
