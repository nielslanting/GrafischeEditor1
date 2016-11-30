using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1.Commands
{
    class SelectFigureCommand : ICommand<List<Figure>>
    {
        List<Figure> Figures;
        Figure OldFigure;
        int X = 0;
        int Y = 0;

        public SelectFigureCommand()
        {
            this.Figures = new List<Figure>();
        }

        public SelectFigureCommand(int x, int y, List<Figure> figures)
        {
            this.Figures = figures;
            this.X = x;
            this.Y = y;
        }

        public List<Figure> Execute()
        {
            // Save to old selected figure
            foreach(Figure figure in this.Figures)
            {
                var selected = figure.GetSelected();
                if (selected != null)
                {
                    this.OldFigure = selected;
                    break;
                }
            }

            // Select the new figure
            var found = false;
            var reversedList = new List<Figure>(this.Figures);
            reversedList.Reverse();

            foreach (var f in reversedList)
            {
                f.Selected = false;
                if (!found && f.Select(this.X, this.Y) != null) found = true;
            }

            return this.Figures;
        }

        public List<Figure> Undo()
        {
            // Unselect all other figures
            foreach (var f in this.Figures) f.Selected = false;

            if (this.OldFigure == null) return this.Figures;

            // Select the selected figure
            var foundFigure = this.Figures.Find(x => x == this.OldFigure);
            if(foundFigure != null) foundFigure.Selected = true;

            return this.Figures;
        }
    }
}
