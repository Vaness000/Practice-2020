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
            int score = 0;
            Console.SetWindowSize(1, 1);
            Console.SetBufferSize(80, 25);
            Console.SetWindowSize(80, 25);

            Walls walls = new Walls(80, 25);
            walls.Draw();
            
            Point p = new Point(4, 5, '*');
            Snake snake = new Snake(p, 4, Direction.RIGHT);
            snake.Draw();

            FoodCreator foodCreator = new FoodCreator(80, 25, '&');
            Point food = foodCreator.CreateFood();
            food.Draw();

             while (true)
             {
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(32, 10);
                    Console.Write("GAME OVER!!!");
                    Console.SetCursorPosition(32, 11);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("SCORE: " + score);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
                 if (snake.Eat(food))
                 {
                     food = foodCreator.CreateFood();
                     food.Draw();
                     ++score;
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
                 Thread.Sleep(400);
                 snake.Move();
             }
            Console.ReadKey();
        }
    }
}
