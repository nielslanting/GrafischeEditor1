using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1.Interfaces
{
    public interface IVisitor
    {
        void Visit(Square figure);
        void Visit(Ellipsis figure);
        void Visit(Group figure);
    }
}
