using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
    public class Kolobok : Players
    {
        Random random = new Random();
        public bool canShot;
        public Kolobok(int posX,int posY, int speed) 
            : base(posX, posY, speed)
        {
            Width = 40;
            Height = 40;
            Image = new Bitmap(@"..\..\images\kolobok.png");
            Direction = Direction.LEFT;
            Speed = speed;
            canShot = true;
        }

        public Bullet Shoot()
        {
            int dir = random.Next(1, 5);
            Direction direction = Direction;
            if(Direction == Direction.STOP)
            {
                switch (dir)
                {
                    case 1: direction = Direction.DOWN;
                        break;
                    case 2: direction = Direction.UP;
                        break;
                    case 3: direction = Direction.RIGHT;
                        break;
                    case 4: direction = Direction.LEFT;
                        break;
                }
            }
            return new Bullet(this.PositionX, this.PositionY, direction, this,Speed);
        }
        public override string ToString()
        {
            return "Kolobok";
        }
        public void StopKolobok()
        {
            switch (Direction)
            {
                case Direction.DOWN:
                    PositionY -=Speed+1;
                    Direction = Direction.STOP;
                    break;
                case Direction.UP:
                    PositionY += Speed + 1;
                    Direction = Direction.STOP;
                    break;
                case Direction.LEFT:
                    PositionX += Speed + 1;
                    Direction = Direction.STOP;
                    break;
                case Direction.RIGHT:
                    PositionX -= Speed + 1;
                    Direction = Direction.STOP;
                    break;
            }
        }
    }
}
