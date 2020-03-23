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
        public Kolobok(int posX,int posY, int speed) 
            : base(posX, posY, speed)
        {
            Width = 40;
            Height = 40;
            Image = new Bitmap(@"..\..\images\kolobok.png");
            Direction = Direction.LEFT;
            Speed = speed;
        }

        public Bullet Shoot()
        {
            return new Bullet(this.PositionX, this.PositionY, this.Direction, this,Speed);
        }
        public override string ToString()
        {
            return "Kolobok";
            
        }
    }
}
