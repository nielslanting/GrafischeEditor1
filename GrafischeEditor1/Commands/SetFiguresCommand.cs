using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1.Commands
{
    class SetFiguresCommand : ICommand<List<Figure>>
    {
        List<Figure> Figures;
        List<Figure> NewFigures;
        List<Figure> OldFigures;

        public SetFiguresCommand()
        {
            this.Figures = new List<Figure>();
            this.NewFigures = new List<Figure>();
            this.OldFigures = new List<Figure>();
        }

        public SetFiguresCommand(List<Figure> cf, List<Figure> nf)
        {
            this.Figures = cf;
            this.NewFigures = nf;
            this.OldFigures = new List<Figure>();
        }

        public List<Figure> Execute()
        {
            this.OldFigures = new List<Figure>(this.Figures);
            this.Figures = new List<Figure>(this.NewFigures);
            return this.Figures;
        }

        public List<Figure> Undo()
        {
            this.Figures = new List<Figure>(this.OldFigures);    
            return this.Figures;
        }
    }
}
