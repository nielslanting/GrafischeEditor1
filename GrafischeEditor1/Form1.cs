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
        public List<Figure> Figures { get; set; }

        private MouseState mouseState;
        private ToolState toolState;

        private Figure drawn = null;

        public Form1()
        {
            InitializeComponent();

            this.Figures = new List<Figure>();

            mouseState = new MouseState();
            mouseState.Changed += mouseState_Changed;

            toolState = ToolState.Rectangle;
            handleToolChange();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            try
            {
                if (drawn != null)
                    drawn.Draw(g);

                foreach (Figure figure in this.Figures)
                    figure.Draw(g);
            }
            catch { }

        }

        private void mouseState_Changed(object sender)
        {
            this.label1.Text = sender.ToString();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            switch (toolState)
            {
                case ToolState.Selection:
                    bool found = false;

                    foreach (Figure figure in this.Figures)
                    {
                        if (figure.GetType() == typeof(Square))
                        {
                            Square f = (Square)figure;
                            if (!found && e.X >= f.X && e.X <= (f.X + f.Width) && e.Y >= f.Y && e.Y <= (f.Y + f.Height))
                            {
                                figure.Selected = true;
                                found = true;
                            }
                            else
                                figure.Selected = false;
                        }

                        if (figure.GetType() == typeof(Ellipsis))
                        {
                            Ellipsis f = (Ellipsis)figure;
                            if (!found && e.X >= f.X && e.X <= (f.X + f.Width) && e.Y >= f.Y && e.Y <= (f.Y + f.Height))
                            {
                                figure.Selected = true;
                                found = true;
                            }
                            else
                                figure.Selected = false;
                        }
                    }
            
                    break;
                case ToolState.Resize:
                    {
                        Figure figure = this.Figures.Where(x => x.Selected == true).FirstOrDefault();
                        if (figure == null) return;

                        int newWidth = e.X - figure.X;
                        int newHeight = e.Y - figure.Y;

                        if (figure.GetType() == typeof(Square))
                        {
                            Square square = (Square)figure;
                            square.Width = newWidth;
                            square.Height = newHeight;
                        }

                        if (figure.GetType() == typeof(Ellipsis))
                        {
                            Ellipsis ellipsis = (Ellipsis)figure;
                            ellipsis.Width = newWidth;
                            ellipsis.Height = newHeight;
                        }
                    }

                    break;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseState.SX = e.X;
            mouseState.SY = e.Y;
            mouseState.Pressed = true;

            switch (toolState)
            {
                case ToolState.Rectangle:
                    drawn = new Square(mouseState.SX, mouseState.SY, (mouseState.EX - mouseState.SX), (mouseState.EY - mouseState.SY));
                    break;
                case ToolState.Ellipsis:
                    drawn = new Ellipsis(mouseState.SX, mouseState.SY, (mouseState.EX - mouseState.SX), (mouseState.EY - mouseState.SY));
                    break;

            }
            
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            mouseState.EX = e.X;
            mouseState.EY = e.Y;

            switch (toolState)
            {
                case ToolState.Rectangle:
                    if (!mouseState.Pressed) return;


                    drawn = new Square(mouseState.SX, mouseState.SY, (mouseState.EX - mouseState.SX), (mouseState.EY - mouseState.SY));
                    break;
                case ToolState.Ellipsis:
                    if (!mouseState.Pressed) return;

                    drawn = new Ellipsis(mouseState.SX, mouseState.SY, (mouseState.EX - mouseState.SX), (mouseState.EY - mouseState.SY));
                    break;
                case ToolState.Move:
                    if (!mouseState.Pressed) return;

                    foreach (Figure f in this.Figures)
                    {
                        if (f.Selected == true)
                        {
                            f.X = e.X;
                            f.Y = e.Y;
                        }
                    }
                    break;
            }

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if(drawn != null)
                this.Figures.Add(drawn);

            drawn = null;
            mouseState.Reset();
        }

        private void timerDraw_Tick(object sender, EventArgs e)
        {
            this.panel1.Refresh();
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            this.toolState = ToolState.Selection;
            this.handleToolChange();
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            this.toolState = ToolState.Move;
            this.handleToolChange();
        }

        private void buttonResize_Click(object sender, EventArgs e)
        {
            this.toolState = ToolState.Resize;
            this.handleToolChange();
        }

        private void buttonRectangle_Click(object sender, EventArgs e)
        {
            this.toolState = ToolState.Rectangle;
            this.handleToolChange();
        }

        private void buttonEllipsis_Click(object sender, EventArgs e)
        {
            this.toolState = ToolState.Ellipsis;
            this.handleToolChange();
        }



        private void handleToolChange()
        {
            this.buttonSelect.BackColor = SystemColors.Control;
            this.buttonMove.BackColor = SystemColors.Control;
            this.buttonResize.BackColor = SystemColors.Control;
            this.buttonRectangle.BackColor = SystemColors.Control;
            this.buttonEllipsis.BackColor = SystemColors.Control;

            if(this.toolState == ToolState.Selection)
                this.buttonSelect.BackColor = Color.Green;

            if (this.toolState == ToolState.Move)
                this.buttonMove.BackColor = Color.Green;

            if (this.toolState == ToolState.Resize)
                this.buttonResize.BackColor = Color.Green;

            if (this.toolState == ToolState.Rectangle)
                this.buttonRectangle.BackColor = Color.Green;

            if (this.toolState == ToolState.Ellipsis)
                this.buttonEllipsis.BackColor = Color.Green;
        }

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
            string name = saveFileDialog.FileName;
            File.WriteAllText(name, Parser.FiguresToString(this.Figures));
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            string name = openFileDialog.FileName;
            this.Figures = Parser.StringToFigures(File.ReadAllText(name));
        }

    }
}
