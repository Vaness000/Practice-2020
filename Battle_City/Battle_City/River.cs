﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
    class River : Borders
    {
        public River(int posX, int posY) : base(posX, posY)
        {
            PositionX = posX;
            PositionY = posY;
            Height = 50;
            Width = 200;
            Image = new Bitmap(@"..\..\images\river.jpg");
        }
    }
}
