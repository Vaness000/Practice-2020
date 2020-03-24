using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
    public class Game
    {
        PackmanController controller;
        Random random = new Random();
        int speed;
        public Kolobok kolobok;
        public List<Wall> walls = new List<Wall>();
        public List<Apple> apples = new List<Apple>();
        public List<River> rivers = new List<River>();
        public List<Explosion> explosions = new List<Explosion>();
        public List<Bound> bounds;
        public List<Tank> tanks = new List<Tank>();
        public Game(int width,int height,int speed)
        {
            kolobok = new Kolobok(150, 150, speed);
            this.speed = speed;
            controller = new PackmanController(speed);
            bounds= new List<Bound>
            {
            new Bound(0,0, width,10),
            new Bound(0,0,10, height),
            new Bound(0, height-10, width,10),
            new Bound(width-10,0,10, height)
            };
        }
        
        public void CreateWalls()
        {
            walls.Add(new Wall(210, 300));
            walls.Add(new Wall(260, 300));
            walls.Add(new Wall(310, 100));
            walls.Add(new Wall(310, 150));
            walls.Add(new Wall(310, 200));
        }
        public void CreateRiver()
        {
            rivers.Add(new River(200, 50));
            rivers.Add(new River(400, 400));
        }
        public void CreateTanks(int tankCount)
        {
            int num = 0;
            tanks.Clear();
            while (tanks.Count < tankCount)
            {
                num++;
                Tank tank = NewTank(num);
                if (tank != null)
                {
                    tanks.Add(tank);
                }
                else
                {
                    continue;
                }
            }
        }
        public Tank NewTank(int numer)
        {
            int positionX;
            int positionY;
            positionX = random.Next(50, 600);
            positionY = random.Next(50, 600);
            Tank newTank = new Tank(positionX, positionY, numer, speed);
            foreach (Tank tank in tanks)
            {
                if (controller.CheckColisions(tank, newTank))
                {
                    return null;
                }
            }
            foreach (Wall wall in walls)
            {
                if (controller.CheckColisions(wall, newTank))
                {
                    return null;
                }
            }
            foreach (River river in rivers)
            {
                if (controller.CheckColisions(river, newTank))
                {
                    return null;
                }
            }
            if (controller.CheckColisions(newTank, kolobok))
            {
                return null;
            }
            return newTank;
        }
    }
}
