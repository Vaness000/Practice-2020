using System;
using System.Collections.Generic;
using System.Drawing;
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
        public void Move()
        {
            {
                if (this.Direction == Direction.LEFT)
                {
                    if (this is Tank)
                    {
                        ImageRotate((Tank)this);
                    }
                    this.PositionX -= this.Speed;
                }
                if (this.Direction == Direction.RIGHT)
                {
                    if (this is Tank)
                    {
                        ImageRotate((Tank)this);
                    }
                    this.PositionX += this.Speed;
                }
                if (this.Direction == Direction.UP)
                {
                    if (this is Tank)
                    {
                        ImageRotate((Tank)this);
                    }
                    this.PositionY -= this.Speed;
                }
                if (this.Direction == Direction.DOWN)
                {
                    if (this is Tank)
                    {
                        ImageRotate((Tank)this);
                    }
                    this.PositionY += this.Speed;
                }
            }
        }
        public void ImageRotate(Tank tank)
        {
            switch (this.Direction)
            {
                case Direction.RIGHT:
                    tank.Image = new Bitmap(@"..\..\images\tank.jpg");
                    tank.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case Direction.DOWN:
                    tank.Image = new Bitmap(@"..\..\images\tank.jpg");
                    tank.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case Direction.LEFT:
                    tank.Image = new Bitmap(@"..\..\images\tank.jpg");
                    tank.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
                case Direction.UP:
                    tank.Image = new Bitmap(@"..\..\images\tank.jpg");
                    break;
            }
        }
        public void RotateDirection()
        {
            switch (Direction)
            {
                case Direction.DOWN:
                    PositionY -= Speed + 2;
                    Direction = Direction.UP;
                    break;
                case Direction.UP:
                    PositionY += Speed + 2;
                    Direction = Direction.DOWN;
                    break;
                case Direction.LEFT:
                    PositionX += Speed + 2;
                    Direction = Direction.RIGHT;
                    break;
                case Direction.RIGHT:
                    PositionX -= Speed + 2;
                    Direction = Direction.LEFT;
                    break;
            }
        }
    }
}
