using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1.Commands
{
    class AddFigureCommand : ICommand<Figure>
    {
        Figure Figure;
        Figure FigureToAdd;

        public AddFigureCommand()
        {
        }

        public AddFigureCommand(Figure fta, Figure figure)
        {
            this.Figure = figure;
            this.FigureToAdd = fta;
        }

        public Figure Execute()
        {
            ((Group)(this.Figure)).Figures.Add(this.FigureToAdd);
            return this.Figure;
        }

        public Figure Undo()
        {
            ((Group)(this.Figure)).Figures.Remove(this.FigureToAdd);
            return this.Figure;
        }

        public override string ToString()
        {
            var figure = String.Empty;
            if (this.FigureToAdd != null)
                figure = this.FigureToAdd.ToString();

            return String.Format("AddFigure: {0}", figure);
        }
    }
}
