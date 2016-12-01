using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1
{
    class Parser
    {
        public static Figure StringToFigures(string input)
        {
            // Clean the input
            var splits = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            splits = splits.Select(x => x.Trim().Trim('\t')).ToArray();

            // Create root node
            Group root = splits.FirstOrDefault().Contains("group") ? Group.FromString(splits.FirstOrDefault()) : new Group(0, 0, new List<Figure>());

            List<Group> groupStack = new List<Group>();

            foreach (string line in splits.Skip(1))
            {
                if (line.Contains("ornament")) continue; // Skip ornaments for now

                // Generate the figure
                Figure figure = null;

                if (line.Contains("group"))
                    figure = Group.FromString(line);

                else if (line.Contains("ellipse"))
                    figure = Ellipsis.FromString(line);

                else if (line.Contains("rectangle"))
                    figure = Square.FromString(line);

                var last = groupStack.LastOrDefault();

                if(figure != null)
                {
                    if (last != null)
                    {
                        last.Figures.Add(figure);
                        if (last.Figures.Capacity == last.Figures.Count)
                            groupStack.Remove(last);
                    }
                    else
                        root.Figures.Add(figure);
                }

                if (figure is Group) groupStack.Add((Group)figure);
            }

            return root;
        }

        public static string GroupToString(Group root)
        {
            var result = String.Empty;
            var flat = root.Enumerate();

            int indent = 0;
            List<int> count = new List<int>();

            foreach (Figure f in flat)
            {
                bool current = false;

                // Decrement group counter
                if (count.Count > 0 && count.LastOrDefault() > 0)
                    count[count.Count - 1]--;

                // Remove count if its zero
                if (count.Count > 0 && count.LastOrDefault() == 0)
                {
                    count.Remove(count.LastOrDefault());
                    indent--;
                }


                // Add counts if its a group
                if (f is Group)
                {
                    current = true;
                    indent++;
                    count.Add(((Group)f).Figures.Count + 1);
                }

                // Prefix the items
                string prefix = "";
                var ind = indent;
                if (current == true) ind--;
                for (int i = 0; i < ind; i++)
                    prefix += "  ";

                // Add the items
                result += prefix + f.ToString() + Environment.NewLine;
            }

            return result;
        }
    }
}
