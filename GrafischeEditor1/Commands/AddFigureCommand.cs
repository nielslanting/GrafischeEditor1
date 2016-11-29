using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1.Commands
{
    class AddFigureCommand : ICommand<List<Figure>>
    {
        List<Figure> Figures;
        Figure FigureToAdd;

        public AddFigureCommand()
        {
            this.Figures = new List<Figure>();
        }

        public AddFigureCommand(Figure fta, List<Figure> figures)
        {
            this.Figures = figures;
            this.FigureToAdd = fta;
        }

        public List<Figure> Execute()
        {
            this.Figures.Add(this.FigureToAdd);
            return this.Figures;
        }

        public List<Figure> Undo()
        {
            this.Figures.Remove(this.FigureToAdd);
            return this.Figures;
        }
    }
}
