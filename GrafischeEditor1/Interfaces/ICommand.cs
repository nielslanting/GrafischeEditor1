using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1
{
    public interface ICommand<T>
    {
        T Execute();
        T Undo();
    }
}
