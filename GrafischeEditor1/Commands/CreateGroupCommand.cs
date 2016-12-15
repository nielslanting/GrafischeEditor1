using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1.Commands
{
    enum GroupOperation
    {
        Remove,
        Add,
        Replace
    }

    class CreateGroupCommand : ICommand<Figure>
    {
        public Figure Figure { get; set; }
        public Figure Selected { get; set; }

        public List<Tuple<GroupOperation, Figure, Figure>> Changed = new List<Tuple<GroupOperation, Figure, Figure>>();

        public CreateGroupCommand()
        {
        }

        public CreateGroupCommand(Figure selected, Figure figure)
        {
            this.Figure = figure;
            this.Selected = selected;
        }

        public Figure Execute()
        {
            // Create the new group
            var selection = ((Group)this.Selected);
            var selected = selection;

            var group = ((Group)this.Figure);

            foreach (Figure f in selected.Figures)
            {
                var parent = group.Remove(f);
                this.Changed.Add(new Tuple<GroupOperation, Figure, Figure>(GroupOperation.Remove, parent, f));
            }

            this.Changed.Add(new Tuple<GroupOperation, Figure, Figure>(GroupOperation.Add, group, selected));
            group.Figures.Add(selected);

            // Remove empty groups
            foreach(Figure f in ((Group)this.Figure).Enumerate())
            {
                if (f is Group && ((Group)f).Figures.Count == 0)
                {
                    var parent = ((Group)this.Figure).Remove(f);
                    this.Changed.Add(new Tuple<GroupOperation, Figure, Figure>(GroupOperation.Remove, parent, f));
                }                   
                    
            }
                
            return this.Figure;
        }

        public Figure Undo()
        {
            foreach(var change in this.Changed)
            {
                if(change.Item1 == GroupOperation.Add)                
                    ((Group)change.Item2).Remove(change.Item3);
                else if(change.Item1 == GroupOperation.Remove)
                    ((Group)change.Item2).Figures.Add(change.Item3);

            }

            return this.Figure;
        }

        public override string ToString()
        {
            var selected = String.Empty;
            if (this.Selected != null)
                selected = this.Selected.ToString();

            return String.Format("CreateGroup: {0}", this.Selected.ToString());
        }
    }
}
