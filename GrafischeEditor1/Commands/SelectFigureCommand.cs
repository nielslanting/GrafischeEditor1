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
        Figure ToSelect;

        int X = 0;
        int Y = 0;
        bool MultiSelect = false;

        public SelectFigureCommand()
        {
        }

        public SelectFigureCommand(int x, int y, bool multiSelect, Figure figure)
        {
            this.Figure = figure;
            this.X = x;
            this.Y = y;
            this.MultiSelect = multiSelect;
        }

        public SelectFigureCommand(Figure ToSelect, Figure figure)
        {
            this.Figure = figure;
            this.ToSelect = ToSelect;
        }

        public Figure Execute()
        {
            this.OldFigure = Figure.GetSelected();

            var oldSelectedState = true;
            if(this.OldFigure != null)
                oldSelectedState = this.OldFigure.Selected;

            if (!this.MultiSelect)
                this.Figure.Unselect();

            if (this.ToSelect != null)
                this.ToSelect.Select();
            else
                this.Figure.Select(this.X, this.Y);

            if(this.OldFigure != null)
            {
                if (this.MultiSelect)
                    this.OldFigure.Selected = true;
                else
                    this.OldFigure.Selected = !oldSelectedState;
            }

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
