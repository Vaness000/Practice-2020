using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(1, 1);
            Console.SetBufferSize(80, 25);
            Console.SetWindowSize(80, 25);

            //рисуем рамочку
            /*HorisontalLine upLine = new HorisontalLine(0, 78, 0, '-');
            HorisontalLine downLine = new HorisontalLine(0, 78, 24, '-');
            VerticalLine leftLine = new VerticalLine(0, 24, 0, '|');
            VerticalLine rightLine = new VerticalLine(0, 24, 78, '|');*/

            VerticalLine v1 = new VerticalLine(0, 10, 5, '%');
            Draw(v1);


            Point p = new Point(4, 5, '*');
            Figure fsnake = new Snake(p, 4, Direction.RIGHT);
            Draw(fsnake);
            Snake snake1 = (Snake)fsnake;

            HorisontalLine h1 = new HorisontalLine(0, 5, 6, '&');

            List<Figure> figures = new List<Figure>();
            figures.Add(fsnake);
            figures.Add(h1);
            figures.Add(v1);
            foreach (var f in figures)
            {
                f.Draw();
            }


            /* upLine.Draw();
             downLine.Draw();
             leftLine.Draw();
             rightLine.Draw();

             Point p = new Point(4, 5, '*');
             Snake snake = new Snake(p, 4, Direction.RIGHT);
             snake.Draw();

             FoodCreator foodCreator = new FoodCreator(80, 25, '&');
             Point food = foodCreator.CreateFood();
             food.Draw();

             while (true)
             {
                 if (snake.Eat(food))
                 {
                     food = foodCreator.CreateFood();
                     food.Draw();
                 }
                 else
                 {
                     snake.Move();
                 }
                 if (Console.KeyAvailable)
                 {
                     ConsoleKeyInfo key = Console.ReadKey();
                     snake.HandleKey(key.Key);
                 }
                 Thread.Sleep(100);
                 snake.Move();
             }*/
            Console.ReadKey();
        }
        static void Draw(Figure figure)
        {
            figure.Draw();
        }
    }
}
