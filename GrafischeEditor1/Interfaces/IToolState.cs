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
        void MouseClick(Figure figure, UndoRedoStack<Figure> figuresStack, MouseState mouseState);
        void MouseDown(Figure figure, UndoRedoStack<Figure> figuresStack, MouseState mouseState);
        void MouseMove(Figure figure, UndoRedoStack<Figure> figuresStack, MouseState mouseState);
        void MouseUp(Figure figure, UndoRedoStack<Figure> figuresStack, MouseState mouseState);
    }
}
