﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
    public class Players : Entity
    {
        public Direction Direction { get; set; }
        public int Speed { get; protected set; }
        public Players(int posX, int posY,int speed) : base(posX, posY)
        {
            PositionX = posX;
            PositionY = posY;
            Speed = speed;
            Width = 40;
            Height = 40;
        }
    }
}
