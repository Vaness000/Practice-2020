using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battle_City
{
    class PackmanController
    {
        Random random = new Random();
        int probability;
        public void KeyPress(KeyEventArgs e, Kolobok entity)
        {
            if(e.KeyCode.ToString() == "Left")
            {
                entity.Direction = Direction.LEFT;
            }
            if(e.KeyCode.ToString() == "Right")
            {
                entity.Direction = Direction.RIGHT;
            }
            if (e.KeyCode.ToString() == "Up")
            {
                entity.Direction = Direction.UP;
            }
            if (e.KeyCode.ToString() == "Down")
            {
                entity.Direction = Direction.DOWN;
            }
        }
        
        public void EntityMove(Players player)
        {
            if(player.Direction == Direction.LEFT)
            {
                if (player is Tank)
                {
                    ImageRotate((Tank)player, Direction.LEFT);
                }
                player.PositionX -= player.Speed;
            }
            if (player.Direction == Direction.RIGHT)
            {
                if (player is Tank)
                {
                    ImageRotate((Tank)player, Direction.RIGHT);
                }
                player.PositionX += player.Speed;
            }
            if (player.Direction == Direction.UP)
            {
                if (player is Tank)
                {
                    ImageRotate((Tank)player, Direction.UP);
                }
                player.PositionY -= player.Speed;
            }
            if (player.Direction == Direction.DOWN)
            {
                if (player is Tank)
                {
                    ImageRotate((Tank)player, Direction.DOWN);
                }
                player.PositionY += player.Speed;
            }
        }

        public bool CheckColisions(Entity e1, Entity e2)
        {
            Rectangle r1 = new Rectangle(e1.PositionX, e1.PositionY, e1.Width, e1.Height);
            Rectangle r2 = new Rectangle(e2.PositionX, e2.PositionY, e2.Width, e2.Height);
            return r1.IntersectsWith(r2);


        }

        public void StopNearBorders(Direction direction,Players player)
        {
            if(direction == Direction.LEFT)
            {
                player.PositionX += player.Speed;
            }
            if (direction == Direction.RIGHT)
            {
                
                player.PositionX -= player.Speed;
            }
            if (direction == Direction.UP)
            {
                
                player.PositionY += player.Speed;
            }
            if (direction == Direction.DOWN)
            {
                
                player.PositionY -= player.Speed;
            }
        }

        public void RotateTank(Tank tank)
        {
           
            probability = random.Next(1, 5);
            switch (probability)
            {
                case 1: tank.Direction = Direction.UP;
                    break;
                case 2: tank.Direction = Direction.DOWN;
                    break;
                case 3: tank.Direction = Direction.LEFT;
                    break;
                case 4: tank.Direction = Direction.RIGHT;
                    break;
                default: break;
            }
        }

        public void ImageRotate(Tank tank, Direction direction)
        {
            switch (direction)
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
      
    }
}
