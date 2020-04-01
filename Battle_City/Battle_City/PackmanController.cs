using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Battle_City
{
    class PackmanController
    {
        
        public PackmanController(int speed)
        {
            this.speed = speed;
        }
        Random random = new Random();
        int speed;
        public List<Bullet> bullets = new List<Bullet>();
        public void RemoveBullet(int index)
        {
            bullets.RemoveAt(index);
        }
        public void KeyPress(KeyEventArgs e, Kolobok entity)
        {
            
            switch (e.KeyCode.ToString())
            {
                case "Left":
                    entity.Direction = Direction.LEFT;
                    break;
                case "Right":

                    entity.Direction = Direction.RIGHT;
                    break;
                case "Up":

                    entity.Direction = Direction.UP;
                    break;
                case "Down":
                    entity.Direction = Direction.DOWN;

                    break;
                case "Space":
                    if (entity.canShot)
                    {
                        bullets.Add(entity.Shoot());
                        entity.canShot = false;
                    }
                    break;
                

            }
        }
        public void TankShoot(Tank tank)
        {
            bullets.Add(tank.Shoot());
        }
        public bool CheckColisions(Entity e1, Entity e2)
        {
            Rectangle r1 = new Rectangle(e1.PositionX, e1.PositionY, e1.Width, e1.Height);
            Rectangle r2 = new Rectangle(e2.PositionX, e2.PositionY, e2.Width, e2.Height);
            return r1.IntersectsWith(r2);
        }

        public Apple NewApple(List<Apple> apples, List<Tank> tanks, Kolobok kolobok, List<Wall> walls,List<River> rivers,List<Block> blocks)
        {
            int positionX;
            int positionY;
            positionX = random.Next(50, 600);
            positionY = random.Next(50, 600);
            Apple apple = new Apple(positionX, positionY);
            foreach (Tank tank in tanks)
            {
                if (CheckColisions(apple, tank))
                {
                    return null;
                }
            }
            foreach(Wall wall in walls)
            {
                if (CheckColisions(apple, wall))
                {
                    return null;
                }
            }
            foreach( River river in rivers)
            {
                if (CheckColisions(river, apple))
                {
                    return null;
                }
            }
            foreach (Block block in blocks)
            {
                if (CheckColisions(block, apple))
                {
                    return null;
                }
            }
            if (CheckColisions(apple, kolobok))
            {
                return null;
            }
            foreach (Apple apple1 in apples)
            {
                if (CheckColisions(apple, apple1))
                {
                    return null;
                }
            }
            return apple;
        }
        public Tank NewTank(List<Tank> tanks, Kolobok kolobok,int numer, List<Wall> walls, List<River> rivers, List<Block> blocks)
        {
            int positionX;
            int positionY;
            positionX = random.Next(50, 600);
            positionY = random.Next(50, 600);
            Tank newTank = new Tank(positionX, positionY, numer,speed);
            foreach(Tank tank in tanks)
            {
                if (CheckColisions(tank, newTank))
                {
                    return null;
                }
            }
            foreach(Wall wall in walls)
            {
                if (CheckColisions(wall, newTank))
                {
                    return null;
                }
            }
            foreach (River river in rivers)
            {
                if (CheckColisions(river, newTank))
                {
                    return null;
                }
            }
            foreach (Block block in blocks)
            {
                if (CheckColisions(block, newTank))
                {
                    return null;
                }
            }
            if (CheckColisions(newTank, kolobok))
            {
                return null;
            }
            return newTank;
        }

    }
}
