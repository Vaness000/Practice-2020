using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
     public class Tank : Players
    {
        public int numer;
        int probability;
        Random rnd = new Random();
        public Tank(int posX, int posY, int num, int speed) : base(posX, posY,speed)
        {
            PositionX = posX;
            PositionY = posY;
            numer = num;
            Speed = speed;
            Image = new Bitmap(@"..\..\images\tank.jpg");
            Direction = SetDirection();
        }
        public Direction SetDirection()
        {
            
            probability = rnd.Next(1, 4);
            switch (probability) {
                case 1: return Direction.DOWN;               
                case 2: return Direction.UP;
                case 3: return Direction.RIGHT;
                case 4:return Direction.LEFT;
                default: return Direction.STOP;
            }
        }
        public Bullet Shoot()
        {
            return new Bullet(this.PositionX, this.PositionY, this.Direction, this,Speed);
        }

        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            Tank tank  = o as Tank;
            if(tank as Tank == null)
            {
                return false;
            }
            return tank.numer == this.numer;
        }
        public override string ToString()
        {
            return "Tank" + numer.ToString();
        }
    }
}
