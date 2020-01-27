using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class HorisontalLine:Figure
    { 
        public HorisontalLine(int xLeft, int xRight, int y,char sym)
        {
            plist = new List<Point>();
            for(int x = xLeft; x <= xRight; x++)
            {
                plist.Add(new Point(x, y, sym));
            }
        }

        public override void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach(Point p in plist)
            {
                p.Draw();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
