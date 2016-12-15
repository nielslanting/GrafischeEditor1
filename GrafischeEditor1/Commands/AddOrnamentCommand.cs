using GrafischeEditor1.Figures;
using GrafischeEditor1.Interfaces;
using GrafischeEditor1.Strategy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1.Commands
{
    class AddOrnamentCommand : ICommand<Figure>
    {
        Figure Figure;
        Figure Selection;
        Ornament Ornament; 

        public AddOrnamentCommand()
        {
        }

        public AddOrnamentCommand(Figure figure, Figure selection, Ornament ornament)
        {
            this.Figure = figure;
            this.Selection = selection;
            this.Ornament = ornament;
        }

        public Figure Execute()
        {
            ((Group)this.Figure).Replace(this.Selection, this.Ornament);
            return this.Figure;
        }

        public Figure Undo()
        {
            ((Group)this.Figure).Replace(this.Ornament, this.Selection);
            return this.Figure;
        }

        public override string ToString()
        {
            var figure = String.Empty;
            if (this.Ornament != null)
                figure = this.Ornament.ToString();

            return String.Format("AddOrnament: {0}", figure);
        }
    }
}
