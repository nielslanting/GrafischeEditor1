﻿using GrafischeEditor1.Commands;
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
    public class MoveTool : IToolState
    {

        public void MouseClick(Figure figure, UndoRedoStack<Figure> figuresStack, MouseState mouseState)
        {
            List<Figure> selectedFigures = figure.GetSelected();
            if (selectedFigures == null || selectedFigures.Count <= 0) return;

            figure = figuresStack.Execute(new MoveFigureCommand(mouseState.SX, mouseState.SY, figure, selectedFigures.FirstOrDefault()), figure);
        }

        public void MouseDown(Figure figure, UndoRedoStack<Figure> figuresStack, MouseState mouseState)
        {
        }

        public void MouseMove(Figure figure, UndoRedoStack<Figure> figuresStack, MouseState mouseState)
        {
        }

        public void MouseUp(Figure figure, UndoRedoStack<Figure> figuresStack, MouseState mouseState)
        {
        }
    }
}
