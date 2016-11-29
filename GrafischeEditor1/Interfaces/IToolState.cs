using GrafischeEditor1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafischeEditor1.Interfaces
{
    public interface IToolState
    {
        void MouseClick(List<Figure> figures, UndoRedoStack<List<Figure>> figuresStack, MouseState mouseState);
        void MouseDown(List<Figure> figures, UndoRedoStack<List<Figure>> figuresStack, MouseState mouseState);
        void MouseMove(List<Figure> figures, UndoRedoStack<List<Figure>> figuresStack, MouseState mouseState);
        void MouseUp(List<Figure> figures, UndoRedoStack<List<Figure>> figuresStack, MouseState mouseState);
    }
}
