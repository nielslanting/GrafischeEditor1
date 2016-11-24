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
        Figure OldFigureToSelect;
        Figure FigureToSelect;

        public SelectFigureCommand()
        {
            this.Figures = new List<Figure>();
            this.OldFigureToSelect = null;
            this.FigureToSelect = null;
        }

        public SelectFigureCommand(List<Figure> figures, Figure fts)
        {
            this.Figures = figures;
            this.OldFigureToSelect = figures.Where(x => x.Selected == true).FirstOrDefault();
            this.FigureToSelect = fts;
        }

        public List<Figure> Execute()
        {
            // Unselect all other figures
            foreach (var f in this.Figures) f.Selected = false;

            // Select the selected figure
            var fig = this.Figures.Find(x => x == this.FigureToSelect);
            if (fig != null) fig.Selected = true;

            return this.Figures;
        }

        public List<Figure> Undo()
        {
            // Unselect all other figures
            foreach (var f in this.Figures) f.Selected = false;

            if (this.OldFigureToSelect == null) return this.Figures;

            // Select the selected figure
            this.Figures.Find(x => x == this.OldFigureToSelect).Selected = true;

            return this.Figures;
        }
    }
}
