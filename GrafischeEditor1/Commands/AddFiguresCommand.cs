using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1.Commands
{
    class AddFiguresCommand : ICommand<List<Figure>>
    {
        List<Figure> Figures;
        List<Figure> FiguresToAdd;

        public AddFiguresCommand()
        {
            this.Figures = new List<Figure>();
            this.FiguresToAdd = new List<Figure>();
        }

        public AddFiguresCommand(List<Figure> figures, List<Figure> fta)
        {
            this.Figures = figures;
            this.FiguresToAdd = fta;
        }

        public List<Figure> Execute()
        {
            this.Figures = new List<Figure>(this.Figures);
            this.Figures.AddRange(this.FiguresToAdd);
            return this.Figures;
        }

        public List<Figure> Undo()
        {
            this.Figures = this.Figures.Where(x => !this.FiguresToAdd.Contains(x)).ToList();
            return this.Figures;
        }
    }
}
