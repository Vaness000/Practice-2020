using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
    public class Block : Borders
    {
        public Block(int posX, int posY) : base(posX, posY)
        {
            PositionX = posX;
            PositionY = posY;
            Destructibility = false;
            Image = Properties.Resources.block;
            Width = 50;
            Height = 50;
        }
    }
}
