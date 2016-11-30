using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1.Commands
{
    class SetFiguresCommand : ICommand<Figure>
    {
        Figure Figure;
        Figure NewFigure;
        Figure OldFigure;

        public SetFiguresCommand()
        {
        }

        public SetFiguresCommand(Figure cf, Figure nf)
        {
            this.Figure = cf;
            this.NewFigure = nf;
        }

        public Figure Execute()
        {
            this.OldFigure = this.Figure;
            this.Figure = this.NewFigure;
            return this.Figure;
        }

        public Figure Undo()
        {
            this.Figure = this.OldFigure;    
            return this.Figure;
        }
    }
}
