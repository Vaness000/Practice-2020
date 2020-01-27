using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class VerticalLine:Figure
    {
        public VerticalLine(int yTop, int yBot, int x, char sym)
        {
            plist = new List<Point>();
            for (int y = yTop; y <= yBot; y++)
            {
                plist.Add(new Point(x, y, sym));
            }

        }
    }
}
