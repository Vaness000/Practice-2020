using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Battle_City
{
    public partial class Battle_City : Form
    {
        PackmanController controller;
        Random rnd = new Random();
        List<Wall> walls = new List<Wall>();
        List<Apple> apples = new List<Apple>();
        List<River> rivers = new List<River>();
        List<Entity> entities = new List<Entity>();
        InformationForm infoForm;
        int width;
        int height;
        int speed;
        List<Bound> bounds;
        List<Tank> tanks = new List<Tank>();
        int tankCount;
        int appleCount;
        int score;
        int removedNum;
        Kolobok kolobok;

        public Battle_City(int tankCount, int appleCount,int width, int height, int speed)
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(OnKeyPress1);
            this.tankCount = tankCount;
            this.appleCount = appleCount;
            this.width = width;
            this.height = height;
            this.speed = speed;
            bounds = new List<Bound>
            {
            new Bound(0,0,width,10),
            new Bound(0,0,10,height),
            new Bound(0,height-10,width,10),
            new Bound(width-10,0,10,height)
            };
            kolobok = new Kolobok(150, 150, speed);
            controller = new PackmanController(speed);
            Invalidate();
            
        }


        public void OnKeyPress1(object sender, KeyEventArgs e)
        {
            controller.KeyPress(e, kolobok);
            e.Handled = true;
        }
        private EventHandler Handler()
        {
            return new EventHandler(Update);
        }
        private EventHandler TankHandler()
        {
            return new EventHandler(RotateTanks);
        }
        private EventHandler TankShootHandler()
        {
            return new EventHandler(ShootTanks);
        }
        public void RotateTanks(object sender, EventArgs e)
        {
            foreach (Tank tank in tanks)
            {
                tank.RotateTank(rnd.Next(1,5));
            }
        }
        public void ShootTanks(object sender, EventArgs e)
        {
            foreach (Tank tank in tanks)
            {
                controller.TankShoot(tank);
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            NewGame();
        }
        public bool IsGameOver()
        {
            foreach (Tank tank in tanks)
            {
                if (controller.CheckColisions(tank, kolobok))
                {
                    return true;
                }
            }
            foreach (Bullet bullet in controller.bullets.Where(x => x.dangerous == true))
            {
                if (controller.CheckColisions(bullet, kolobok))
                {
                    return true;
                }
            }
            return false;

        }
        public void GameOver()
        {
            timer1.Stop();
            startButton.Enabled = true;
            KeyPreview = false;
            timer1.Enabled = false;
            timer1.Tick -= Handler();
            timer2.Stop();
            timer2.Enabled = false;
            timer2.Tick -= TankHandler();
            timer3.Stop();
            timer3.Enabled = false;
            timer3.Tick -= TankShootHandler();
            infoForm.Dispose();
            controller.bullets.Clear();
            tanks.Clear();
            apples.Clear();
            walls.Clear();
            rivers.Clear();
            entities.Clear();
            MessageBox.Show("Game Over!" + "\n" + "You score: " + score);
            label1.Text = "0";


        }
        private void NewGame()
        {
            kolobok.PositionX = 150;
            kolobok.PositionX = 150;

            CreateRiver();
            CreateWalls();
            CreateTanks();
            CreateApples();
            entities.Add(kolobok);
            entities.AddRange(tanks);
            entities.AddRange(apples);
            
            KeyPreview = true;
            startButton.Enabled = false;

            timer1.Interval = 1;
            timer1.Tick += Handler();
            timer1.Enabled = true;
            timer1.Start();

            timer2.Interval = 1000;
            timer2.Tick += TankHandler();
            timer2.Enabled = true;
            timer2.Start();

            timer3.Interval = 1500;
            timer3.Enabled = true;
            timer3.Tick += TankShootHandler();
            timer3.Start();
            infoForm = new InformationForm(entities.Count);
            infoForm.Show();
            Focus();
            score = 0;
        }

        private void CreateTanks()
        {
            int num = 0;
            tanks.Clear();
            while (tanks.Count < tankCount)
            {
                num++;
                Tank tank = controller.NewTank(tanks, kolobok, num,walls,rivers);
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
        private void CreateWalls()
        {
            walls.Add(new Wall(210, 300));
            walls.Add(new Wall(260, 300));
            walls.Add(new Wall(310, 100));
            walls.Add(new Wall(310, 150));
            walls.Add(new Wall(310, 200));
        }
        private void CreateRiver()
        {
            rivers.Add(new River(200, 50));
            rivers.Add(new River(400, 400));
        }

        private void CreateApples()
        {

            apples.Clear();
            while (apples.Count < appleCount)
            {
                Apple apple = controller.NewApple(apples, tanks, kolobok,walls,rivers);
                if (apple != null)
                {
                    apples.Add(apple);
                }
                else
                {
                    continue;
                }
            }

        }


        private void Update(object sender, EventArgs e)
        {
            
            CheckBounds();
            Recovery();
            kolobok.Move();

            foreach (Bullet bullet1 in controller.bullets)
            { 
                bullet1.Move();
            }

            foreach (Tank tank in tanks)
            {
                tank.Move();
            }

            label1.Text = score.ToString();
            
            for( int i = 0; i < entities.Count; i++)
            {
                infoForm.UpdateDGW(entities[i].ToString(),entities[i].PositionX,entities[i].PositionY,i);
            }
            if (IsGameOver())
            {
                GameOver();
            }
            pictureBox1.Invalidate();
           
            
        }

        public void CheckBounds()
        {
            foreach (Bound bound in bounds)
            {
                if (controller.CheckColisions(kolobok, bound))
                {
                    
                    kolobok.StopNearBorders();
                }
            }
            for (int i = 0; i < apples.Count; i++)
            {
                if (controller.CheckColisions(apples[i], kolobok))
                {
                    score++;
                    apples.RemoveAt(i);
                }
            }
            for (int i = 0; i < controller.bullets.Count; i++)
            {
                int bullX = controller.bullets[i].PositionX;
                int bullY = controller.bullets[i].PositionY;
                if (bullX < 10 || bullX > 670 || bullY < 10 || bullY > 670)
                {
                    controller.RemoveBullet(i);
                }

            }
            
            for (int i = 0; i < controller.bullets.Count; i++)
            {
                try
                {
                    for (int j = 0; j < walls.Count; j++)
                    {
                        if (controller.CheckColisions(walls[j], controller.bullets[i]))
                        {
                            controller.RemoveBullet(i);
                            walls.RemoveAt(j);
                        }
                    }
                }
                catch
                {
                    continue;
                }
            }


            for (int i = 0; i < tanks.Count; i++)
            {
                for (int j = 0; j < controller.bullets.Count; j++)
                {
                    try
                    {
                        if (controller.CheckColisions(controller.bullets[j], tanks[i]))
                        {
                            if (!controller.bullets[j].dangerous)
                            {
                                removedNum = tanks[i].numer;
                                tanks.RemoveAt(i);
                                controller.RemoveBullet(j);
                                score++;
                            }
                        }
                    }
                    catch { continue; }

                }
            }
            foreach(Wall wall1 in walls)
            {
                if (controller.CheckColisions(kolobok, wall1))
                {
                    
                    kolobok.StopNearBorders();
                }
            }
            foreach(River river1 in rivers)
            {
                if (controller.CheckColisions(kolobok, river1))
                {
                    
                    kolobok.StopNearBorders();
                }
            }

            foreach (Tank tank in tanks)
            {

                foreach (Bound bound in bounds)
                {
                    if (controller.CheckColisions(tank, bound))
                    {
                       
                        tank.StopNearBorders();
                    }
                }
                foreach(Wall wall in walls)
                {
                    if (controller.CheckColisions(tank, wall))
                    {
                        
                        tank.StopNearBorders();
                    }
                }
                foreach(River river in rivers)
                {
                    if (controller.CheckColisions(tank, river))
                    {
                        
                        tank.StopNearBorders();
                    }
                }
                foreach (Tank tank1 in tanks)
                {
                    if (controller.CheckColisions(tank, tank1))
                    {
                        if (!tank.Equals(tank1))
                        {
                            tank1.StopNearBorders();
                            tank.StopNearBorders();

                        }
                    }
                }

            }
            

        }
        public void Recovery()
        {
            while (tanks.Count < tankCount)
            {
                var newTank = controller.NewTank(tanks, kolobok, removedNum,walls,rivers);
                if (newTank != null)
                {
                    tanks.Add(newTank);
                }
            }
            while (apples.Count < appleCount)
            {
                var newApple = controller.NewApple(apples, tanks, kolobok,walls, rivers);
                if (newApple != null)
                {
                    apples.Add(newApple);
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.DrawImage(kolobok.Image, kolobok.PositionX, kolobok.PositionY);
            foreach(Wall wall in walls)
            {
                graphics.DrawImage(wall.Image, wall.PositionX, wall.PositionY, wall.Width, wall.Height);
            }
            foreach(River river in rivers)
            {
                graphics.DrawImage(river.Image, river.PositionX, river.PositionY, river.Width, river.Height);
            }
            foreach (Apple apple in apples)
            {
                graphics.DrawImage(apple.Image, apple.PositionX, apple.PositionY, apple.Width, apple.Height);
            }
            foreach (Bound bound in bounds)
            {
                graphics.DrawImage(bound.Image, bound.PositionX, bound.PositionY, bound.Width, bound.Height);
            }
            foreach (Tank tank in tanks)
            {
                graphics.DrawImage(tank.Image, tank.PositionX, tank.PositionY, tank.Width, tank.Height);
            }
            foreach (Bullet bullet in controller.bullets)
            {
                graphics.DrawImage(bullet.Image, bullet.PositionX, bullet.PositionY, bullet.Width, bullet.Height);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Height = 750;
        }

        private void infoButton_Click(object sender, EventArgs e)
        {
            infoForm.Show();

        }

        private void Battle_City_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (infoForm != null)
            {
                infoForm.Dispose();
            }
        }

        
    }
}
