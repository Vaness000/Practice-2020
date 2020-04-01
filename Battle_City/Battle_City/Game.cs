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
        public List<Block> blocks = new List<Block>();
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
            walls.Add(new Wall(210, 550));
            walls.Add(new Wall(160, 550));
            walls.Add(new Wall(110, 550));
            walls.Add(new Wall(260, 550));
            walls.Add(new Wall(310, 550));
            walls.Add(new Wall(360, 550));
            walls.Add(new Wall(80, 150));
            walls.Add(new Wall(80, 200));
        }
        public void CreateBlock()
        {
            blocks.Add(new Block(550, 120));
            blocks.Add(new Block(550, 170));
            blocks.Add(new Block(150, 420));
            blocks.Add(new Block(200, 420));
            blocks.Add(new Block(490, 520));
            blocks.Add(new Block(490, 570));
        }
        public void CreateRiver()
        {
            rivers.Add(new River(200, 100));
            rivers.Add(new River(400, 400));
        }
        
    }
}
