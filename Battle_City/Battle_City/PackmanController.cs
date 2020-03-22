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
                player.PositionX -= player.Speed;
            }
            if (player.Direction == Direction.RIGHT)
            {
                player.PositionX += player.Speed;
            }
            if (player.Direction == Direction.UP)
            {
                player.PositionY -= player.Speed;
            }
            if (player.Direction == Direction.DOWN)
            {
                player.PositionY += player.Speed;
            }
        }

        public bool CheckColisions(Entity e1, Entity e2)
        {
            Rectangle r1 = new Rectangle(e1.PositionX, e1.PositionY, e1.Width, e1.Height);
            Rectangle r2 = new Rectangle(e2.PositionX, e2.PositionY, e2.Width, e2.Height);
            return r1.IntersectsWith(r2);


        }

        public void ChangeDirection(Direction direction,Players player)
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
      
    }
}
