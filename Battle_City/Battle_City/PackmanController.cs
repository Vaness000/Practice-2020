using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        int probability;
        int speed;
        public List<Bullet> bullets = new List<Bullet>();
        public void RemoveBullet(int index)
        {
            bullets.RemoveAt(index);
        }
        public void KeyPress(KeyEventArgs e, Kolobok entity)
        {
            if(e.KeyCode.ToString() == "Left")
            {
                entity.Direction = Direction.LEFT;
            }
            if(e.KeyCode.ToString() == "Right")
            {
                entity.Direction = Direction.RIGHT;
            }
            if (e.KeyCode.ToString() == "Up")
            {
                entity.Direction = Direction.UP;
            }
            if (e.KeyCode.ToString() == "Down")
            {
                entity.Direction = Direction.DOWN;
            }
            if (e.KeyCode.ToString() == "Space")
            {
                bullets.Add(entity.Shoot());
            }

        }

        public void TankShoot(Tank tank)
        {
            bullets.Add(tank.Shoot());
        }
        
        public void EntityMove(Players player)
        {
            if(player.Direction == Direction.LEFT)
            {
                if (player is Tank)
                {
                    ImageRotate((Tank)player, Direction.LEFT);
                }
                player.PositionX -= player.Speed;
            }
            if (player.Direction == Direction.RIGHT)
            {
                if (player is Tank)
                {
                    ImageRotate((Tank)player, Direction.RIGHT);
                }
                player.PositionX += player.Speed;
            }
            if (player.Direction == Direction.UP)
            {
                if (player is Tank)
                {
                    ImageRotate((Tank)player, Direction.UP);
                }
                player.PositionY -= player.Speed;
            }
            if (player.Direction == Direction.DOWN)
            {
                if (player is Tank)
                {
                    ImageRotate((Tank)player, Direction.DOWN);
                }
                player.PositionY += player.Speed;
            }
        }

        public bool CheckColisions(Entity e1, Entity e2)
        {
            Rectangle r1 = new Rectangle(e1.PositionX, e1.PositionY, e1.Width, e1.Height);
            Rectangle r2 = new Rectangle(e2.PositionX, e2.PositionY, e2.Width, e2.Height);
            return r1.IntersectsWith(r2);


        }

        public void StopNearBorders(Direction direction,Players player)
        {
            if(direction == Direction.LEFT)
            {
                //player.PositionX += player.Speed;
                player.Direction = Direction.RIGHT;
            }
            if (direction == Direction.RIGHT)
            {
                player.Direction = Direction.LEFT;
                //player.PositionX -= player.Speed;
            }
            if (direction == Direction.UP)
            {
                player.Direction = Direction.DOWN;
                //player.PositionY += player.Speed;
            }
            if (direction == Direction.DOWN)
            {
                player.Direction = Direction.UP;
                //player.PositionY -= player.Speed;
            }
        }

        public void RotateTank(Tank tank)
        {
           
            probability = random.Next(1, 5);
            switch (probability)
            {
                case 1: tank.Direction = Direction.UP;
                    break;
                case 2: tank.Direction = Direction.DOWN;
                    break;
                case 3: tank.Direction = Direction.LEFT;
                    break;
                case 4: tank.Direction = Direction.RIGHT;
                    break;
                default: break;
            }
        }

        public void RotateTank(Tank tank, Tank tank1)
        {

            switch (tank.Direction)
            {
                case Direction.DOWN:
                    tank.Direction = Direction.UP;
                    break;
                case Direction.UP:
                    tank.Direction = Direction.DOWN;
                    break;
                case Direction.RIGHT:
                    tank.Direction = Direction.LEFT;
                    break;
                case Direction.LEFT:
                    tank.Direction = Direction.RIGHT;
                    break;
                default: break;
            }
            switch (tank1.Direction)
            {
                case Direction.DOWN:
                    tank1.Direction = Direction.UP;
                    break;
                case Direction.UP:
                    tank1.Direction = Direction.DOWN;
                    break;
                case Direction.RIGHT:
                    tank1.Direction = Direction.LEFT;
                    break;
                case Direction.LEFT:
                    tank1.Direction = Direction.RIGHT;
                    break;
                default: break;
            }
        }

        public void ImageRotate(Tank tank, Direction direction)
        {
            switch (direction)
            {
                case Direction.RIGHT:
                    tank.Image = new Bitmap(@"..\..\images\tank.jpg");
                    tank.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case Direction.DOWN:
                    tank.Image = new Bitmap(@"..\..\images\tank.jpg");
                    tank.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case Direction.LEFT:
                    tank.Image = new Bitmap(@"..\..\images\tank.jpg");
                    tank.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
                case Direction.UP:
                    tank.Image = new Bitmap(@"..\..\images\tank.jpg");
                    break;
            }  
        }
        public Apple NewApple(List<Apple> apples, List<Tank> tanks, Kolobok kolobok)
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
        public Tank NewTank(List<Tank> tanks, Kolobok kolobok,int numer)
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
            if (CheckColisions(newTank, kolobok))
            {
                return null;
            }
            return newTank;
        }

    }
}
