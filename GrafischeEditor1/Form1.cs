using GrafischeEditor1.Commands;
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
        public Bitmap Painting { get; set; }

        private MouseState mouseState;
        private IToolState toolState;

        public Form1()
        {
            InitializeComponent();

            this.Painting = new Bitmap(panel1.ClientSize.Width, panel1.ClientSize.Height);
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
            //Graphics g = e.Graphics;
            

            // Paint the items
            try
            {
                Graphics g = Graphics.FromImage(this.Painting);
                g.Clear(Color.White);

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

                e.Graphics.DrawImage(this.Painting, Point.Empty);
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
            File.WriteAllText(name, Parser.GroupToString((Group)this.Figure));
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

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exportFileDialog.ShowDialog();
        }

        private void exportFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            var name = exportFileDialog.FileName;

            try
            {
                this.Painting.Save(name);
            }
            catch(Exception er)
            {
                MessageBox.Show("The file could not be exported.");
            }
            
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

        public List<Figure> Flat { get; set; }
        #region TreeViewMethods
        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            var items = new List<string>();

            var flat = ((Group)this.Figure).Enumerate().ToList();
            if(flat == null || this.Flat == null || !flat.SequenceEqual(this.Flat))
            {
                this.Flat = flat;

                int indent = 0;
                List<int> count = new List<int>();

                foreach(Figure f in flat)
                {
                    bool current = false;

                    // Decrement group counter
                    if (count.Count > 0 && count.LastOrDefault() > 0)
                        count[count.Count - 1]--;

                    // Add counts if its a group
                    if(f is Group)
                    {
                        current = true;
                        indent++;
                        count.Add(((Group)f).Figures.Count + 1);
                    }

                    // Remove count if its zero
                    if (count.Count > 0 && count.LastOrDefault() == 0)
                    {
                        count.Remove(count.LastOrDefault());
                        indent--;
                    }
                        
                    // Prefix the items
                    string prefix = "";
                    var ind = indent;
                    if (current == true) ind--;
                    for (int i = 0; i < ind; i++)
                        prefix += "- ";

                    // Add the items
                    items.Add(prefix + f.ToString());
                }

                this.checkedListBoxFigures.Items.Clear();
                foreach (var item in items)
                    this.checkedListBoxFigures.Items.Add(item);
                
            }
        }

        private void checkedListBoxFigures_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBoxFigures.SelectedIndex >= 0 && checkedListBoxFigures.SelectedIndex <= this.Flat.Count)
            {
                var figureToSelect = this.Flat[checkedListBoxFigures.SelectedIndex];
                this.Figure = this.FiguresStack.Execute(new SelectFigureCommand(figureToSelect, this.Figure), this.Figure);
            }
        }

        private void checkedListBoxFigures_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.Flat[e.Index].ToggleVisibility();
        }

        #endregion

        private void buttonCreateGroup_Click(object sender, EventArgs e)
        {
            //Square s = new Square(50, 50, 100, 100);
            Group g = new Group(0, 0, new List<Figure>());
            ((Group)this.Figure).Figures.Add(g);
        }


    }
}
