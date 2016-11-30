using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1.Commands
{
    class ResizeFigureCommand : ICommand<List<Figure>>
    {
        List<Figure> Figures;
        Figure SelectedFigure;
        int X = 0, Y = 0;
        int oldWidth = 0, oldHeight = 0, oldX = 0, oldY = 0;

        public ResizeFigureCommand()
        {
            this.Figures = new List<Figure>();
        }

        public ResizeFigureCommand(int x, int y, List<Figure> figures, Figure selectedFigure)
        {
            this.Figures = figures;
            this.SelectedFigure = selectedFigure;
            this.X = x;
            this.Y = y;
        }

        public List<Figure> Execute()
        {
            if (SelectedFigure == null) return this.Figures;

            this.oldX = this.SelectedFigure.X;
            this.oldY = this.SelectedFigure.Y;

            int newWidth = this.X - this.SelectedFigure.X;
            int newHeight = this.Y - this.SelectedFigure.Y;

            this.oldWidth = this.SelectedFigure.Width;
            this.oldHeight = this.SelectedFigure.Height;

            /*if (this.SelectedFigure is Group)
            {
                this.oldX = ((Group)(this.SelectedFigure)).X;
                this.oldY = ((Group)(this.SelectedFigure)).Y;
                newWidth = this.X - ((Group)(this.SelectedFigure)).X;
                newHeight = this.Y - ((Group)(this.SelectedFigure)).Y;
            }*/

            this.SelectedFigure.Resize(newWidth, newHeight);

            return this.Figures;
        }

        public List<Figure> Undo()
        {
            /*this.SelectedFigure.X = this.oldX;
            this.SelectedFigure.Y = this.oldY;
            this.SelectedFigure.Width = this.oldWidth;
            this.SelectedFigure.Height = this.oldHeight;*/

            this.SelectedFigure.Move(this.oldX, this.oldY);

            /*if(this.SelectedFigure is Group)
                ((Group)(this.SelectedFigure)).Resize(this.oldWidth, this.oldHeight);
            else*/
            this.SelectedFigure.Resize(this.oldWidth, this.oldHeight);

            

            return this.Figures;
        }
    }
}
