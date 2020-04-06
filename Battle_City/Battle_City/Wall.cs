using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
    public class Wall : Borders
    {
        public Wall(int posX, int posY) : base(posX, posY)
        {
            PositionX = posX;
            PositionY = posY;
            Image = Properties.Resources.wall;
            Width = 50;
            Height = 50;
            Destructibility = true;
        }
    }
}
