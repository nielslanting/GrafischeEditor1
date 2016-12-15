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
        public Figure _Figure { get; set; }

        public string Text { get; set; }
        public OrientationEnum Orientation { get; set; }

        public Ornament(string text, OrientationEnum orientation) : base(0, 0, null)
        {
            this.Text = text;
            this.Orientation = orientation;
        }

        public Ornament(Figure figure, string text, OrientationEnum orientation) : base(figure.X, figure.Y, figure.Strategy)
        {
            this._Figure = figure;
            this.Text = text;
            this.Orientation = orientation;
        }

        public override int X
        {
            get { return _Figure.X; }
            set
            {
                if(_Figure != null) _Figure.X = value;
            }
        }

        public override int Y
        {
            get { return _Figure.Y; }
            set
            {
                if (_Figure != null) _Figure.Y = value;
            }
        }

        public override bool Selected
        {
            get
            {
                return _Figure.Selected;
            }

            set
            {
                if (_Figure != null) _Figure.Selected = value;
            }
        }

        public override IStrategy Strategy
        {
            get { return _Figure.Strategy; }
            set
            {
                if (_Figure != null) _Figure.Strategy = value;
            }
        }

        public override int Height
        {
            get
            {
                return _Figure.Height;
            }

            set
            {
                _Figure.Height = value;
            }
        }

        public override void Draw(Graphics g)
        {
            _Figure.Draw(g);

            if (!this.Visible) return;

            int x = -10, y = -10;
            var font = new System.Drawing.Font("Arial", 16);
            var brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            var format = new System.Drawing.StringFormat();

            switch (Orientation)
            {
                case OrientationEnum.TOP:
                    x = _Figure.X + (_Figure.Width / 2) - ((int)g.MeasureString(this.Text, font).Width / 2);
                    y = _Figure.Y - (int)g.MeasureString(this.Text, font).Height;
                    break;
                case OrientationEnum.BOTTOM:
                    x = _Figure.X + (_Figure.Width / 2) - ((int)g.MeasureString(this.Text, font).Width / 2);
                    y = _Figure.Y + _Figure.Height;
                    break;
                case OrientationEnum.LEFT:
                    x = _Figure.X - ((int)g.MeasureString(this.Text, font).Width);
                    y = _Figure.Y + _Figure.Height / 2 - ((int)g.MeasureString(this.Text, font).Height / 2); ;
                    break;
                case OrientationEnum.RIGHT:
                    x = _Figure.X + _Figure.Width;
                    y = _Figure.Y + _Figure.Height / 2 - ((int)g.MeasureString(this.Text, font).Height / 2); ;
                    break;
            }



            g.DrawString(this.Text, font, brush, x, y, format);

        }

        public override int Width
        {
            get
            {
                return _Figure.Width;
            }

            set
            {
                _Figure.Width = value;
            }
        }

        public override void Select()
        {
            _Figure.Select();
        }
        public override Figure Select(int x, int y)
        {
            return _Figure.Select(x, y);
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Visit(this);
            visitor.Visit(this._Figure);
        }

        public override string ToString()
        {
            return String.Format("ornament {0} \"{1}\"", this.Orientation.ToString().ToLower(), this.Text);
        }

        public override IEnumerable<Figure> Enumerate()
        {
            var result = new List<Figure>() { this, this._Figure };

            if (_Figure is Ornament || _Figure is Group)
                result.AddRange(_Figure.Enumerate().Skip(1));
                    
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
