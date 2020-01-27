using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Snake : Figure
    {
        public Direction direction;

        public Snake(Point tail, int length, Direction _direction)
        {
            direction = _direction;
            plist = new List<Point>();
            for (int i = 0;i<length; i++)
            {
                Point p = new Point(tail);
                p.Move(i, direction);
                plist.Add(p);
            }
        }
        public void Move()
        {
            Point tail = plist.First();
            plist.Remove(tail);
            Point head = GetNextPoint();
            plist.Add(head);

            tail.Clear();
            head.Draw();
        }
        public Point GetNextPoint()
        {
            Point head = plist.Last();
            Point nextPoint = new Point(head);
            nextPoint.Move(1, direction);
            return nextPoint;
        }
        public void HandleKey(ConsoleKey key)
        {
            if (key == ConsoleKey.LeftArrow && direction!=Direction.RIGHT)
                direction = Direction.LEFT;
            if (key== ConsoleKey.RightArrow && direction != Direction.LEFT)
                direction = Direction.RIGHT;
            if (key== ConsoleKey.DownArrow && direction != Direction.UP)
                direction = Direction.DOWN;
            if (key == ConsoleKey.UpArrow && direction != Direction.DOWN)
                direction = Direction.UP;
        }

        public bool Eat(Point food)
        {
            Point head = GetNextPoint();
            if (head.IsHit(food))
            {
                food.sym = head.sym;
                plist.Add(food);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsHitTail()
        {
            var head = plist.Last();
            for(int i = 0; i < plist.Count - 2; i++)
            {
                if (head.IsHit(plist[i]))
                    return true;
            }
            return false;
        }


    }
}
