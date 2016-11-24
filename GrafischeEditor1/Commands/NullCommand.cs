using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1.Commands
{
    class NullCommand : ICommand<string>
    {
        public string Name { get; set; }

        public NullCommand(string name)
        {
            this.Name = name;
        }

        public string Execute()
        {
            throw new Exception(String.Format("Null Execute {0}", this.Name));
        }

        public string Undo()
        {
            throw new Exception(String.Format("Null Undo {0}", this.Name));
        }

        public string Redo()
        {
            throw new Exception(String.Format("Null Redo {0}", this.Name));
        }
    }
}
