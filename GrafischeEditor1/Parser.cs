using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafischeEditor1
{
    class Parser
    {
        public static List<Figure> StringToFigures(string input)
        {
            Stack<Figure> stack = new Stack<Figure>();
            
            foreach(string line in input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
            {
                Figure lastFigure = stack.Count > 0 ? stack.Peek() : null;

                // Get the active group
                Group group = null;
                if (lastFigure != null && lastFigure.GetType() == typeof(Group) 
                    && ((Group)lastFigure).Figures.Capacity != ((Group)lastFigure).Figures.Count)               
                    group = (Group)lastFigure;
                

                Figure figure = null;

                if (line.Contains("group"))
                    figure = Group.FromString(line);

                if (line.Contains("ellipse"))
                    figure = Ellipsis.FromString(line);

                if (line.Contains("rectangle"))
                    figure = Square.FromString(line);

                if (figure != null)
                {
                    if (group != null)
                        group.Figures.Add(figure);
                    else
                        stack.Push(figure);
                }
            }

            return stack.ToList();
        }

        public static string FiguresToString(List<Figure> figures)
        {
            string result = String.Empty;

            foreach (Figure figure in figures)
                result += figure.ToString() + Environment.NewLine;

            return result;
        }
    }
}
