using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
    public abstract class Entity
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public Image Image { get; set; }
        


        public Entity(int posX, int posY)
        {
            PositionX = posX;
            PositionY = posY;
            
        }
    }
}
