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
        int X = 0, Y = 0;
        int oldX = 0, oldY = 0;

        public MoveFigureCommand()
        {
            this.Figures = new List<Figure>();
        }

        public MoveFigureCommand(int x, int y, List<Figure> figures)
        {
            this.Figures = figures;
            this.X = x;
            this.Y = y;
        }

        public List<Figure> Execute()
        {
            var figure = this.Figures.Find(x => x.Selected == true);
            if (figure == null) return this.Figures;

            this.oldX = figure.X;
            this.oldY = figure.Y;

            figure.X = this.X;
            figure.Y = this.Y;

            return this.Figures;
        }

        public List<Figure> Undo()
        {
            var figure = this.Figures.Find(x => x.Selected == true);
            if (figure == null) return this.Figures;

            figure.X = this.oldX;
            figure.Y = this.oldY;

            return this.Figures;
        }
    }
}
