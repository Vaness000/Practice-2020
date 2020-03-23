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
        public Kolobok(int posX,int posY) 
            : base(posX, posY)
        {
            Width = 40;
            Height = 40;
            Image = new Bitmap(@"..\..\images\kolobok.png");
            Direction = Direction.LEFT;
        }

        public Bullet Shoot()
        {
            return new Bullet(this.PositionX, this.PositionY, this.Direction, this);
        }
        public override string ToString()
        {
            return "Kolobok";
            
        }
    }
}
