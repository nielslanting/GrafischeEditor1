using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1.Helpers
{
    public class UndoRedoStack<T>
    {
        public Stack<ICommand<T>> UndoStack { get; }
        public Stack<ICommand<T>> RedoStack { get; }

        public UndoRedoStack()
        {
            this.UndoStack = new Stack<ICommand<T>>();
            this.RedoStack = new Stack<ICommand<T>>();
        }

        public T Execute(ICommand<T> command, T input)
        {
            T output = command.Execute();

            this.UndoStack.Push(command);
            this.RedoStack.Clear();

            return output;
        }

        public T Undo(T input)
        {
            if (this.UndoStack.Count <= 0) return input;

            ICommand<T> command = this.UndoStack.Pop();
            T output = command.Undo();
            this.RedoStack.Push(command);
            return output;
        }

        public T Redo(T input)
        {
            if (this.RedoStack.Count <= 0) return input;

            ICommand<T> command = this.RedoStack.Pop();
            T output = command.Execute();
            this.UndoStack.Push(command);
            return output;
        }
    }
}
