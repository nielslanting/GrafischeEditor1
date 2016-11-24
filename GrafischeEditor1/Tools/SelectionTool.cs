using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafischeEditor1.Tools
{
    public class SelectionTool
    {
        public static Figure FindSelected(int x, int y, List<Figure> figures)
        {
            Figure result = null;

            foreach (Figure f in figures.AsEnumerable().Reverse())
            {
                if (result == null && x >= f.X && x <= (f.X + f.Width) && y >= f.Y && y <= (f.Y + f.Height))               
                    result = f;
            }

            return result;
        }
    }
}
