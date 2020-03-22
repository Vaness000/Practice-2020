using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
    public abstract class Borders:Entity
    {
        public Borders(int posX, int posY) : base(posX, posY)
        {
            PositionX = posX;
            PositionY = posY;
        }

        public bool Destructibility { get; protected set; }
        public bool Perforating { get; protected set; }
    }
}
