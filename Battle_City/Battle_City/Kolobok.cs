using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
    class Kolobok : Players
    {
        public Kolobok(int posX,int posY) 
            : base(posX, posY)
        {
            Width = 40;
            Height = 40;
            Image = new Bitmap(@"..\..\images\kolobok.png");
            Direction = Direction.LEFT;
        }
    }
}
