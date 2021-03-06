﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
    public class Apple : Entity
    {
        public Apple(int posX, int posY) : base(posX, posY)
        {
            PositionX = posX;
            PositionY = posY;
            Image = new Bitmap(@"..\..\images\apple.png");
            Width = 40;
            Height = 40;
        }
        public override string ToString()
        {
            return "Apple";
        }
    }
}
