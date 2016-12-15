using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrafischeEditor1.Interfaces;
using System.Drawing;
using System.Text.RegularExpressions;

namespace GrafischeEditor1.Figures
{
    public enum OrientationEnum
    {
        TOP,
        BOTTOM,
        LEFT,
        RIGHT
    };

    public class Ornament : Figure
    {
        public Figure WrappedFigure { get; set; }

        public string Text { get; set; }
        public OrientationEnum Orientation { get; set; }

        public Ornament(string text, OrientationEnum orientation) : base(0, 0, null)
        {
            this.Text = text;
            this.Orientation = orientation;
        }

        public Ornament(Figure figure, string text, OrientationEnum orientation) : base(figure.X, figure.Y, figure.Strategy)
        {
            this.WrappedFigure = figure;
            this.Text = text;
            this.Orientation = orientation;
        }

        public override int X
        {
            get { return WrappedFigure.X; }
            set
            {
                if(WrappedFigure != null) WrappedFigure.X = value;
            }
        }

        public override int Y
        {
            get { return WrappedFigure.Y; }
            set
            {
                if (WrappedFigure != null) WrappedFigure.Y = value;
            }
        }

        public override bool Selected
        {
            get
            {
                return WrappedFigure.Selected;
            }

            set
            {
                if (WrappedFigure != null) WrappedFigure.Selected = value;
            }
        }

        public override IStrategy Strategy
        {
            get { return WrappedFigure.Strategy; }
            set
            {
                if (WrappedFigure != null) WrappedFigure.Strategy = value;
            }
        }

        public override int Height
        {
            get
            {
                return WrappedFigure.Height;
            }

            set
            {
                WrappedFigure.Height = value;
            }
        }

        public override void Draw(Graphics g)
        {
            WrappedFigure.Draw(g);

            if (!this.Visible) return;

            int x = -10, y = -10;
            var font = new System.Drawing.Font("Arial", 16);
            var brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            var format = new System.Drawing.StringFormat();

            switch (Orientation)
            {
                case OrientationEnum.TOP:
                    x = WrappedFigure.X + (WrappedFigure.Width / 2) - ((int)g.MeasureString(this.Text, font).Width / 2);
                    y = WrappedFigure.Y - (int)g.MeasureString(this.Text, font).Height;
                    break;
                case OrientationEnum.BOTTOM:
                    x = WrappedFigure.X + (WrappedFigure.Width / 2) - ((int)g.MeasureString(this.Text, font).Width / 2);
                    y = WrappedFigure.Y + WrappedFigure.Height;
                    break;
                case OrientationEnum.LEFT:
                    x = WrappedFigure.X - ((int)g.MeasureString(this.Text, font).Width);
                    y = WrappedFigure.Y + WrappedFigure.Height / 2 - ((int)g.MeasureString(this.Text, font).Height / 2); ;
                    break;
                case OrientationEnum.RIGHT:
                    x = WrappedFigure.X + WrappedFigure.Width;
                    y = WrappedFigure.Y + WrappedFigure.Height / 2 - ((int)g.MeasureString(this.Text, font).Height / 2); ;
                    break;
            }



            g.DrawString(this.Text, font, brush, x, y, format);

        }

        public override int Width
        {
            get
            {
                return WrappedFigure.Width;
            }

            set
            {
                WrappedFigure.Width = value;
            }
        }

        public override void Select()
        {
            WrappedFigure.Select();
        }
        public override Figure Select(int x, int y)
        {
            return WrappedFigure.Select(x, y);
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Visit(this);
            visitor.Visit(this.WrappedFigure);
        }

        public override string ToString()
        {
            return String.Format("ornament {0} \"{1}\"", this.Orientation.ToString().ToLower(), this.Text);
        }

        public override IEnumerable<Figure> Enumerate()
        {
            var result = new List<Figure>() { this, this.WrappedFigure };

            if (WrappedFigure is Ornament || WrappedFigure is Group)
                result.AddRange(WrappedFigure.Enumerate().Skip(1));
                    
            return result;
        }

        public static Ornament FromString(string input)
        {
            Regex r = new Regex("ornament (top|bottom|left|right) \"(.*?)\"+");

            if (r.Match(input).Value != input) return null;

            var splits = input.Split(' ');

            OrientationEnum? oe = null;

            switch (splits[1])
            {
                case "top": oe = OrientationEnum.TOP; break;
                case "bottom": oe = OrientationEnum.BOTTOM; break;
                case "left": oe = OrientationEnum.LEFT; break;
                case "right": oe = OrientationEnum.RIGHT; break;
            }

            var name = splits[2];
            name = name.Substring(1, name.Length - 2);

            return new Ornament(name, (OrientationEnum)oe);
        }
    }
}
