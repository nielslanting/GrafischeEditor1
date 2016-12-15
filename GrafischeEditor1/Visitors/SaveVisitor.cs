using GrafischeEditor1.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1.Visitors
{
    class SaveVisitor : IVisitor
    {
        public string PathName { get; set; }

        private string Result = String.Empty;

        public SaveVisitor(string pathName)
        {
            this.PathName = pathName;
        }

        public void Visit(Group figure)
        {
            this.Result += Parser.GroupToString(figure);
        }

        public void Visit(Ellipsis figure)
        {
            this.Result += figure.ToString();
        }

        public void Visit(Square figure)
        {
            this.Result += figure.ToString();
        }

        public void Save()
        {
            File.WriteAllText(this.PathName, this.Result);
        }
    }
}
