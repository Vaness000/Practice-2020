using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
    public class Explosion : Entity
    {
        public Explosion(int posX, int posY) : base(posX, posY)
        {
            PositionX = posX;
            PositionY = posY;
            Width = 35;
            Height = 35;
            Image = Properties.Resources.explosion;
        }
    }
}
