using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1.Commands
{
    class MoveFigureCommand : ICommand<List<Figure>>
    {
        List<Figure> Figures;
        Figure SelectedFigure;
        int X = 0, Y = 0;
        int oldX = 0, oldY = 0;

        public MoveFigureCommand()
        {
            this.Figures = new List<Figure>();
        }

        public MoveFigureCommand(int x, int y, List<Figure> figures, Figure selected)
        {
            this.Figures = figures;
            this.SelectedFigure = selected;
            this.X = x;
            this.Y = y;
        }

        public List<Figure> Execute()
        {
            if (this.SelectedFigure == null) return this.Figures;

            this.oldX = this.SelectedFigure.X;
            this.oldY = this.SelectedFigure.Y;

            // QUESTION: How do you get the property using a overriden get/setter without type checking?
            // Should I implement A IFigure interface?
            if(this.SelectedFigure is Group)
            {
                this.oldX = ((Group)this.SelectedFigure).X;
                this.oldY = ((Group)this.SelectedFigure).Y;
            }

            this.SelectedFigure.Move(this.X, this.Y);

            return this.Figures;
        }

        public List<Figure> Undo()
        {
            if (this.SelectedFigure == null) return this.Figures;

            this.SelectedFigure.Move(this.oldX, this.oldY);

            return this.Figures;
        }
    }
}
