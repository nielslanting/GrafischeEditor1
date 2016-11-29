﻿using GrafischeEditor1.Interfaces;
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
        public void MouseClick(List<Figure> figures, MouseState mouseState)
        {
            foreach (var fig in figures) fig.Select(mouseState.SX, mouseState.SY);
        }

        public void MouseDown(List<Figure> figures, MouseState mouseState)
        {
        }

        public void MouseMove(List<Figure> figures, MouseState mouseState)
        {
        }

        public void MouseUp(List<Figure> figures, MouseState mouseState)
        {
        }
    }
}
