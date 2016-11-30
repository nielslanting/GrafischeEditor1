using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1.Commands
{
    class SelectFigureCommand : ICommand<Figure>
    {
        Figure Figure;
        Figure OldFigure;
        int X = 0;
        int Y = 0;

        public SelectFigureCommand()
        {
        }

        public SelectFigureCommand(int x, int y, Figure figure)
        {
            this.Figure = figure;
            this.X = x;
            this.Y = y;
        }

        public Figure Execute()
        {
            this.OldFigure = Figure.GetSelected();

            this.Figure.Unselect();
            this.Figure.Select(this.X, this.Y);

            return this.Figure;
        }

        public Figure Undo()
        {
            if (this.OldFigure == null) return this.Figure;

            this.Figure.Unselect();
            this.OldFigure.Selected = true;

            return this.Figure;
        }
    }
}
