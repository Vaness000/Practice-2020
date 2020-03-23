using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
    public class Bullet : Players
    {
        Players sender;
        public bool dangerous;
        public Bullet(int posX, int posY,Direction direction, Players player,int speed) : base(posX, posY,speed)
        {
            PositionX = posX + player.Width / 2;
            PositionY = posY + player.Height / 2;
            Speed = speed*4;
            Direction = direction;
            Width = 10;
            Height = 10;
            sender = player;
            Image = SetImage();
        }

        private Image SetImage()
        {
            Image img;
            if(sender is Kolobok)
            {
                this.dangerous = false;
                img = new Bitmap(@"..\..\images\KolobokBullet.png");
            }
            else
            {
                this.dangerous = true;
                img = new Bitmap(@"..\..\images\TankBullet.png");
            }
            return img;
            
        }
    }
}
