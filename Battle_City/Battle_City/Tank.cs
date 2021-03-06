﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
     public class Tank : Players
    {
        public int numer;
        public bool marked;
        Random rnd = new Random();
        public Tank(int posX, int posY, int num, int speed) : base(posX, posY,speed)
        {
            marked = false;
            PositionX = posX;
            PositionY = posY;
            numer = num;
            Speed = speed;
            Image = Properties.Resources.tankUp;
            Direction = SetDirection(rnd.Next(1, 4));
        }
        public Direction SetDirection(int probability)
        {
            switch (probability) {
                case 1: return Direction.DOWN;               
                case 2: return Direction.UP;
                case 3: return Direction.RIGHT;
                case 4:return Direction.LEFT;
                default: return Direction.STOP;
            }
        }
        public Bullet Shoot()
        {
            return new Bullet(this.PositionX, this.PositionY, this.Direction, this,Speed, Properties.Resources.TankBullet,true);
        }

        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            Tank tank  = o as Tank;
            if(tank as Tank == null)
            {
                return false;
            }
            return tank.numer == this.numer;
        }
        public override string ToString()
        {
            return "Tank" + numer.ToString();
        }
        public void RotateTank(int probability)
        {
            
            switch (probability)
            {
                case 1:
                    this.Direction = Direction.UP;
                    break;
                case 2:
                    this.Direction = Direction.DOWN;
                    break;
                case 3:
                    this.Direction = Direction.LEFT;
                    break;
                case 4:
                    this.Direction = Direction.RIGHT;
                    break;
                default: break;
            }
        }
        public override void Move()
        {
            if (this.Direction == Direction.LEFT)
            {
                Image = Properties.Resources.tankLeft;
                this.PositionX -= this.Speed;
            }
            if (this.Direction == Direction.RIGHT)
            {
                Image = Properties.Resources.tankRight;
                this.PositionX += this.Speed;
            }
            if (this.Direction == Direction.UP)
            {
                Image = Properties.Resources.tankUp;
                this.PositionY -= this.Speed;
            }
            if (this.Direction == Direction.DOWN)
            {
                Image = Properties.Resources.tankDown;
                this.PositionY += this.Speed;
            }
        }
    }
}
