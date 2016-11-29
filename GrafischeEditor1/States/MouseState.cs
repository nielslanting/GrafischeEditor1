using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1
{
    public class MouseState
    {
        public event ChangedEventHandler Changed;
        public delegate void ChangedEventHandler(object sender);

        private int _sx = 0, _sy = 0, _ex = 0, _ey = 0;
        private bool _pressed = false;

        public void Reset()
        {
            this.SX = 0;
            this.SY = 0;
            this.EX = 0;
            this.EY = 0;
            this.Pressed = false;
        }

        public int SX {
            get { return _sx; }
            set
            {
                if(_sx != value)
                {
                    _sx = value;
                    if (Changed != null) Changed(this);
                }
            }
        }

        public int SY
        {
            get { return _sy; }
            set
            {
                if (_sy != value)
                {
                    _sy = value;
                    if (Changed != null) Changed(this);
                }
            }
        }

        public int EX
        {
            get { return _ex; }
            set
            {
                if (_ex != value)
                {
                    _ex = value;
                    if (Changed != null) Changed(this);
                }
            }
        }

        public int EY
        {
            get { return _ey; }
            set
            {
                if (_ey != value)
                {
                    _ey = value;
                    if (Changed != null) Changed(this);
                }
            }
        }

        public bool Pressed
        {
            get { return _pressed; }
            set
            {
                if (_pressed != value)
                {
                    _pressed = value;
                    if (Changed != null) Changed(this);
                }
            }
        }


        public override string ToString()
        {
            return String.Format("({0}, {1}) ({2}, {3}) pressed: {4}", _sx, _sy, _ex, _ey, _pressed);
        }
    }
}
