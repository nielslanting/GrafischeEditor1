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
    public class ResizeTool : IToolState
    {
        private Figure GetSelectedFigure(List<Figure> figures)
        {
            foreach (var fig in figures)
            {
                var f = fig.GetSelected();
                if (f != null) return f;
            }

            return null;
        }

        public void MouseClick(List<Figure> figures, UndoRedoStack<List<Figure>> figuresStack, MouseState mouseState)
        {
            Figure selectedFigure = GetSelectedFigure(figures);
            if (selectedFigure == null) return;

            figures = figuresStack.Execute(new ResizeFigureCommand(mouseState.SX, mouseState.SY, figures, selectedFigure), figures);
        }

        public void MouseDown(List<Figure> figures, UndoRedoStack<List<Figure>> figuresStack, MouseState mouseState)
        {
        }

        public void MouseMove(List<Figure> figures, UndoRedoStack<List<Figure>> figuresStack, MouseState mouseState)
        {
        }

        public void MouseUp(List<Figure> figures, UndoRedoStack<List<Figure>> figuresStack, MouseState mouseState)
        {
        }
    }
}
