using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
    public class Bound : Borders
    {
        public Bound(int posX, int posY, int width,int height) : base(posX, posY)
        {
            this.Height = height;
            this.Width = width;
            this.Perforating = false;
            this.Destructibility = false;
            this.Image = Properties.Resources.bound;
        }
    }

}
