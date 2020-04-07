using System;
using System.Collections.Generic;
using System.IO;
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
        int width;
        int height;
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
            this.height = height;
            this.width = width;
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
        public void CreateLevel()
        {
            using (StreamReader reader = File.OpenText(@"../../level.txt"))
            {
                for(int i = 0; i < height; i += 50)
                {
                    for(int j = 0; j < width; j += 50)
                    {
                        char c = (char)reader.Read();
                        switch (c)
                        {
                            case 'r':
                                rivers.Add(new River(j, i));
                                break;
                            case 'w':
                                walls.Add(new Wall(j, i));
                                break;
                            case 'b':
                                blocks.Add(new Block(j, i));
                                break;
                            case ' ':
                                continue;
                            case '\n':
                                j = width;
                                break;
                        }
                    }
                }
            }
        }
        
    }
}
