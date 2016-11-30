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
    public class SelectionTool : IToolState
    {
        public void MouseClick(Figure figure, UndoRedoStack<Figure> figuresStack, MouseState mouseState)
        {
            figure = figuresStack.Execute(new SelectFigureCommand(mouseState.SX, mouseState.SY, figure), figure);
        }

        public void MouseDown(Figure figures, UndoRedoStack<Figure> figuresStack, MouseState mouseState)
        {
        }

        public void MouseMove(Figure figures, UndoRedoStack<Figure> figuresStack, MouseState mouseState)
        {
        }

        public void MouseUp(Figure figures, UndoRedoStack<Figure> figuresStack, MouseState mouseState)
        {
        }
    }
}
