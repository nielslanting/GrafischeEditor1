using GrafischeEditor1.Visitors;
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

            var dx = this.X - this.SelectedFigure.X;
            var dy = this.Y - this.SelectedFigure.Y;

            this.oldX = dx * -1;
            this.oldY = dy * -1;

            MoveVisitor moveVisitor = new MoveVisitor(dx, dy);
            this.SelectedFigure.Visit(moveVisitor);

            return this.Figure;
        }

        public Figure Undo()
        {
            if (this.SelectedFigure == null) return this.Figure;

            MoveVisitor moveVisitor = new MoveVisitor(this.oldX, this.oldY);
            this.SelectedFigure.Visit(moveVisitor);

            return this.Figure;
        }

        public override string ToString()
        {
            var selected = String.Empty;
            if (this.SelectedFigure != null)
                selected = this.SelectedFigure.ToString();

            return String.Format("MoveFigure: {0}", selected);
        }
    }
}
