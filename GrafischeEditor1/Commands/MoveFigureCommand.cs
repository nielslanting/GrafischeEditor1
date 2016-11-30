using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1.Commands
{
    class MoveFigureCommand : ICommand<Figure>
    {
        Figure Figure;
        Figure SelectedFigure;
        int X = 0, Y = 0;
        int oldX = 0, oldY = 0;

        public MoveFigureCommand()
        {
        }

        public MoveFigureCommand(int x, int y, Figure figure, Figure selected)
        {
            this.Figure = figure;
            this.SelectedFigure = selected;
            this.X = x;
            this.Y = y;
        }

        public Figure Execute()
        {
            if (this.SelectedFigure == null) return this.Figure;

            this.oldX = this.SelectedFigure.X;
            this.oldY = this.SelectedFigure.Y;

            this.SelectedFigure.Move(this.X, this.Y);

            return this.Figure;
        }

        public Figure Undo()
        {
            if (this.SelectedFigure == null) return this.Figure;

            this.SelectedFigure.Move(this.oldX, this.oldY);

            return this.Figure;
        }
    }
}
