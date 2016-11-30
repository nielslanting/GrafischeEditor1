﻿using GrafischeEditor1.Commands;
using GrafischeEditor1.Helpers;
using GrafischeEditor1.Interfaces;
using GrafischeEditor1.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafischeEditor1
{
    public partial class Form1 : Form
    {
        public UndoRedoStack<Figure> FiguresStack;

        public Figure Figure { get; set; }

        private MouseState mouseState;
        private IToolState toolState;

        public Form1()
        {
            InitializeComponent();

            this.Figure = new Group(0, 0, new List<Figure>());
            this.FiguresStack = new UndoRedoStack<Figure>();

            mouseState = new MouseState();
            mouseState.Changed += mouseState_Changed;

            toolState = new RectangleTool();
            handleToolChange();
        }

        #region DrawMethods
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            try
            {
                this.Figure.Draw(g);

                if(this.toolState is RectangleTool)
                {
                    var preview = ((RectangleTool)this.toolState).Drawn;
                    if (preview != null) preview.Draw(g);
                }

                if(this.toolState is EllipsisTool)
                {
                    var preview = ((EllipsisTool)this.toolState).Drawn;
                    if (preview != null) preview.Draw(g);
                }
            }
            catch { }

        }

        private void timerDraw_Tick(object sender, EventArgs e)
        {
            this.panel1.Refresh();
        }
        #endregion

        #region MouseMethods
        private void mouseState_Changed(object sender)
        {
            this.label1.Text = sender.ToString();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            mouseState.SX = e.X;
            mouseState.SY = e.Y;

            this.toolState.MouseClick(this.Figure, this.FiguresStack, mouseState);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseState.SX = e.X;
            mouseState.SY = e.Y;
            mouseState.Pressed = true;

            this.toolState.MouseDown(this.Figure, this.FiguresStack, mouseState);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            mouseState.EX = e.X;
            mouseState.EY = e.Y;

            this.toolState.MouseMove(this.Figure, this.FiguresStack, mouseState);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            this.toolState.MouseUp(this.Figure, this.FiguresStack, mouseState);
            mouseState.Reset();
        }
        #endregion

        #region ToolMethods
        private void buttonSelect_Click(object sender, EventArgs e)
        {
            this.toolState = new SelectionTool();

            this.handleToolChange();
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            this.toolState = new MoveTool();
            this.handleToolChange();
        }

        private void buttonResize_Click(object sender, EventArgs e)
        {
            this.toolState = new ResizeTool();
            this.handleToolChange();
        }

        private void buttonRectangle_Click(object sender, EventArgs e)
        {
            this.toolState = new RectangleTool();
            this.handleToolChange();
        }

        private void buttonEllipsis_Click(object sender, EventArgs e)
        {
            this.toolState = new EllipsisTool();
            this.handleToolChange();
        }

        private void handleToolChange()
        {
            this.buttonSelect.BackColor = SystemColors.Control;
            this.buttonMove.BackColor = SystemColors.Control;
            this.buttonResize.BackColor = SystemColors.Control;
            this.buttonRectangle.BackColor = SystemColors.Control;
            this.buttonEllipsis.BackColor = SystemColors.Control;

            if (this.toolState is SelectionTool)
                this.buttonSelect.BackColor = Color.Green;

            if (this.toolState is MoveTool)
                this.buttonMove.BackColor = Color.Green;

            if (this.toolState is ResizeTool)
                this.buttonResize.BackColor = Color.Green;

            if (this.toolState is RectangleTool)
                this.buttonRectangle.BackColor = Color.Green;

            if (this.toolState is EllipsisTool)
                this.buttonEllipsis.BackColor = Color.Green;
        }
        #endregion

        #region IOMethods
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            var name = saveFileDialog.FileName;
            File.WriteAllText(name, Parser.FiguresToString(this.Figure));
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            var name = openFileDialog.FileName;
            Figure figure = null;

            try
            {
                figure = Parser.StringToFigures(File.ReadAllText(name));
            }
            catch(FileLoadException ex)
            {
                MessageBox.Show("The file could not be opened.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Corrupted file could not be loaded.");
            }

            if(figure != null)
                this.Figure = this.FiguresStack.Execute(new SetFiguresCommand(this.Figure, figure), this.Figure);
        }
        #endregion

        #region UndoRedoMethods
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Figure = this.FiguresStack.Undo(this.Figure);
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Figure = this.FiguresStack.Redo(this.Figure);
        }
        #endregion

        #region TreeViewMethods
        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            var items = new List<string>();

            items.AddRange(this.Figure.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None));
            bool changed = items.Count != this.listBoxFigures.Items.Count;

            if(changed == false)
            {
                for (var i = 0; i < this.listBoxFigures.Items.Count; i++)
                {
                    var item = this.listBoxFigures.Items[i];
                    if (item.ToString() != items[i])
                    {
                        changed = true;
                        break;
                    }
                }
            }

            if (changed == false) return;

            this.listBoxFigures.Items.Clear();
            foreach(var item in items)
            {
                this.listBoxFigures.Items.Add(item);
            }
        }

        private void listBoxFigures_SelectedValueChanged(object sender, EventArgs e)
        {

        }
        #endregion

        private void buttonCreateGroup_Click(object sender, EventArgs e)
        {
            Square s = new Square(50, 50, 100, 100);
            Group g = new Group(0, 0, new List<Figure> { s });
        }
    }
}
