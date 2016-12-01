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
        List<Figure> OldFigures;
        Figure ToSelect;

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

        public SelectFigureCommand(Figure ToSelect, Figure figure)
        {
            this.Figure = figure;
            this.ToSelect = ToSelect;
        }

        public Figure Execute()
        {
            this.OldFigures = Figure.GetSelected();

            this.Figure.Unselect();

            if (this.ToSelect != null)
                this.ToSelect.Select();
            else
                this.Figure.Select(this.X, this.Y);

            return this.Figure;
        }

        public Figure Undo()
        {
            if (this.OldFigures == null) return this.Figure;

            this.Figure.Unselect();
            //this.OldFigures.Selected = true;
            foreach(Figure f in this.OldFigures)
            {
                f.Selected = true;
            }

            return this.Figure;
        }
    }
}
