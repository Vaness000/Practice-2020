using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battle_City
{
    public partial class Form1 : Form
    {
        PackmanController controller = new PackmanController();
        Kolobok kolobok = new Kolobok(150, 150);
        List<Apple> apples= new List<Apple>();
        List<Bound> bounds = new List<Bound>
        {
            new Bound(0,0,700,10),
            new Bound(0,0,10,700),
            new Bound(0,690,700,10),
            new Bound(690,0,10,700)
        };
        static Random rnd = new Random();
        List<Tank> tanks = new List<Tank>();
        int tankCount;
        int appleCount;
        int score;
        int removedNum;


        public Form1(int tankCount, int appleCount)
        {
            InitializeComponent();
            this.KeyDown+= new KeyEventHandler(OnKeyPress1);
            this.tankCount = tankCount;
            this.appleCount = appleCount;
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
                controller.RotateTank(tank);
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
            foreach(Tank tank in tanks)
            {
                if(controller.CheckColisions(tank, kolobok))
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

        }
        private void NewGame()
        {
            CreateTanks();
            CreateApples();
            controller.bullets.Clear();
            KeyPreview = true;
            startButton.Enabled = false;
            
            timer1.Interval = 8;
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

            score = 0;
        }

        private void CreateTanks()
        {
            int num = 0;
            tanks.Clear();
            while (tanks.Count < tankCount)
            {
                num++;
                Tank tank = controller.NewTank(tanks,kolobok,num);
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

        private void CreateApples()
        {
            
            apples.Clear();
            while(apples.Count< appleCount)
            {
                Apple apple = controller.NewApple(apples,tanks,kolobok);
                if(apple != null)
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
            foreach (Bound bound in bounds)
            {
                if (controller.CheckColisions(kolobok, bound))
                {
                    controller.StopNearBorders(kolobok.Direction, kolobok);
                }
            }
            controller.EntityMove(kolobok);

            foreach(Bullet bullet1 in controller.bullets)
            {
                controller.EntityMove(bullet1);
            }

            foreach(Tank tank in tanks)
            {
                
                controller.EntityMove(tank);
            }
            
            label1.Text = score.ToString();
            
            if (IsGameOver())
            {
                GameOver();
            }
            Invalidate();

        }

        public void CheckBounds()
        {
            bool hit = false;
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

            
            for(int i = 0; i < tanks.Count; i++)
            {
                for(int j = 0; j < controller.bullets.Count; j++)
                {
                    try
                    {
                        if (controller.CheckColisions(controller.bullets[j], tanks[i]) )
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
            foreach (Tank tank in tanks)
            {

                foreach (Bound bound in bounds)
                {
                    if (controller.CheckColisions(tank, bound))
                    {
                        controller.StopNearBorders(tank.Direction, tank);
                    }
                }
                foreach (Tank tank1 in tanks)
                {
                    if (controller.CheckColisions(tank, tank1))
                    {
                        if (!tank.Equals(tank1)) { 
                            controller.StopNearBorders(tank1.Direction, tank1);
                            controller.RotateTank(tank1,tank);
                        }
                    }
                }
                
            }

        }
        public void Recovery()
        {
            while (tanks.Count < tankCount)
            {
                 var newTank = controller.NewTank(tanks, kolobok, removedNum);
                if (newTank != null)
                {
                    tanks.Add(newTank);
                }
            }
            while (apples.Count < appleCount)
            {
                var newApple = controller.NewApple(apples, tanks, kolobok);
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
            foreach (Apple apple in apples)
            {
                graphics.DrawImage(apple.Image, apple.PositionX, apple.PositionY, apple.Width, apple.Height);
            }
            foreach (Bound bound in bounds)
            {
                graphics.DrawImage(bound.Image, bound.PositionX, bound.PositionY, bound.Width,bound.Height);
            }
            foreach(Tank tank in tanks)
            {
                graphics.DrawImage(tank.Image, tank.PositionX, tank.PositionY, tank.Width, tank.Height);
            }
            foreach(Bullet bullet in controller.bullets)
            {
                graphics.DrawImage(bullet.Image, bullet.PositionX, bullet.PositionY, bullet.Width, bullet.Height);
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Height = 750;
        }
    }
}
