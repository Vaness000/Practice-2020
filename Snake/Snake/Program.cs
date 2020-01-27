using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {

            Point p1 = new Point(1,3,'*');
            p1.Draw();

            Point p2 = new Point(4,5,'#');
            p2.Draw();

            List<int> numList = new List<int>();
            numList.Add(0);
            numList.Add(1);
            numList.Add(2);

            int x = numList[0];
            int y = numList[1];
            int z = numList[2];

            foreach(int i in numList)
            {
                Console.WriteLine(i);
            }
            numList.RemoveAt(0);

            List<Point> plist = new List<Point>();
            plist.Add(p1);
            plist.Add(p2);

            List<char> chars = new List<char>();
            chars.Add('@');
            chars.Add('&');
            chars.Add('$');
            chars.Add('!');
            foreach (char i in chars)
            {
                Console.WriteLine(i);
            }
            numList.RemoveAt(0);

            List<Point> plist2 = new List<Point>();
            plist2.Add(new Point(3, 6, '*'));
            plist2.Add(new Point(10, 10,'('));
            plist2.Add(new Point(12, 12,')'));
            foreach(Point p in plist2)
            {
                p.Draw();
            }


            Console.ReadKey();
        }
        static void Draw(int x, int y, char sym)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(sym);
        }
    }
}
