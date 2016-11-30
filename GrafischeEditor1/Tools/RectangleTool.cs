using GrafischeEditor1.Commands;
using GrafischeEditor1.Helpers;
using GrafischeEditor1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafischeEditor1.Tools
{
    class RectangleTool : IToolState
    {
        public Figure Drawn { get; set; }

        public void MouseClick(Figure figure, UndoRedoStack<Figure> figuresStack, MouseState mouseState)
        {
        }

        public void MouseDown(Figure figure, UndoRedoStack<Figure> figuresStack, MouseState mouseState)
        {
            this.Drawn = new Square(mouseState.SX, mouseState.SY, (mouseState.EX - mouseState.SX), (mouseState.EY - mouseState.SY));
        }

        public void MouseMove(Figure figure, UndoRedoStack<Figure> figuresStack, MouseState mouseState)
        {
            if (!mouseState.Pressed) return;
            this.Drawn = new Square(mouseState.SX, mouseState.SY, (mouseState.EX - mouseState.SX), (mouseState.EY - mouseState.SY));
        }

        public void MouseUp(Figure figure, UndoRedoStack<Figure> figuresStack, MouseState mouseState)
        {
            if (this.Drawn != null && this.Drawn.Width > 0 && this.Drawn.Height > 0)
            {
                figure = figuresStack.Execute(new AddFigureCommand(this.Drawn, figure), figure);
                this.Drawn = null;
            }       
        }
    }
}
