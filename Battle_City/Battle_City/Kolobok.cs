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
            Image = Properties.Resources.kolobok;
            Direction = Direction.LEFT;
            Speed = speed;
            canShot = true;
        }

        public Bullet Shoot()
        {
            return new Bullet(this.PositionX, this.PositionY, this.Direction, this,Speed, Properties.Resources.KolobokBullet,false);
        }
        public override string ToString()
        {
            return "Kolobok";
        }
        
    }
}
