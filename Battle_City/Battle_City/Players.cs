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
        public virtual void Move()
        {
            {
                if (this.Direction == Direction.LEFT)
                {
                    this.PositionX -= this.Speed;
                }
                if (this.Direction == Direction.RIGHT)
                {
                    this.PositionX += this.Speed;
                }
                if (this.Direction == Direction.UP)
                {
                    this.PositionY -= this.Speed;
                }
                if (this.Direction == Direction.DOWN)
                {
                    this.PositionY += this.Speed;
                }
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
