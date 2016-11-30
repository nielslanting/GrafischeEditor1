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
            var stack = new List<Figure>();
            
            foreach(string raw in input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
            {
                string line = raw.Trim().Trim('\t');
                Figure lastFigure = stack.Count > 0 ? stack.Last() : null;

                // Get the active group
                Group group = null;
                if (lastFigure != null && lastFigure.GetType() == typeof(Group) 
                    && ((Group)lastFigure).Figures.Capacity != ((Group)lastFigure).Figures.Count)               
                    group = (Group)lastFigure;      

                Figure figure = null;

                if (line.Contains("group"))
                    figure = Group.FromString(line);

                else if (line.Contains("ellipse"))
                    figure = Ellipsis.FromString(line);

                else if (line.Contains("rectangle"))
                    figure = Square.FromString(line);

                if (figure != null)
                {
                    if (group != null)
                        group.Figures.Add(figure);
                    else
                        stack.Add(figure);
                }
            }

            var top = stack.FirstOrDefault();
            if (top is Group)
            {
                var figures = stack.Skip(1);
                foreach (Figure f in figures)
                    ((Group)(top)).Figures.Add(f);
            }
            else
            {
                top = new Group(0, 0, stack);
            }

            return top;
        }

        public static string FiguresToString(Figure figure)
        {
            string result = String.Empty;

            // Get all the lines
            string lines = String.Empty;

            lines = figure.ToString();
            /*foreach (Figure figure in figures)
                lines += figure.ToString() + Environment.NewLine;*/

            // Indent the lines correctly
            var indent = 0;
            foreach (string line in lines.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
            {
                if (line == String.Empty) continue;

                var indentation = String.Empty;
                for (int i = 0; i < indent; i++)
                    indentation += '\t';

                result += indentation + line + Environment.NewLine;
                if (line.Contains("group")) indent++;
            }

            return result.TrimEnd('\r', '\n'); ;
        }
    }
}
