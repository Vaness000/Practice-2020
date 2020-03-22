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


        public Form1(int tankCount, int appleCount)
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(OnKeyPress);
            this.tankCount = tankCount;
            this.appleCount = appleCount;
            Invalidate();
        }


        public void OnKeyPress(object sender, KeyEventArgs e)
        {
            controller.KeyPress(e, kolobok);
        }
        private EventHandler Handler()
        {
            return new EventHandler(Update);
        }
        private EventHandler TankHandler()
        {
            return new EventHandler(RotateTanks);
        }
        public void RotateTanks(object sender, EventArgs e)
        {
            foreach (Tank tank in tanks)
            {
                controller.RotateTank(tank);
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
            return false;
        }
        public void GameOver()
        {
            timer1.Stop();
            startButton.Enabled = true;
            KeyPreview = false;
            timer1.Enabled = false;
            timer1.Tick -= Handler();
        }
        private void NewGame()
        {
            CreateTanks();
            CreateApples();
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
                    num--;
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
            
            foreach (Bound bound in bounds)
            {
                if (controller.CheckColisions(kolobok, bound))
                {
                    controller.StopNearBorders(kolobok.Direction, kolobok);
                }
            }
            controller.EntityMove(kolobok);
            Bullet removedBullet = null;
            Tank removedTank = null;
            foreach (Bullet bullet in controller.bullets)
            {
                foreach(Bound bound in bounds)
                {
                    if (controller.CheckColisions(bullet, bound))
                    {
                        removedBullet = bullet;
                    }
                }
                foreach(Tank tank in tanks)
                {
                    if (controller.CheckColisions(tank, bullet))
                    {
                        removedBullet = bullet;
                        removedTank = tank;
                    }
                }
            }
            if (removedBullet != null)
            {
                controller.bullets.Remove(removedBullet);
            }
            if (removedTank != null)
            {
                tanks.Remove(removedTank);
            }
            
            foreach(Bullet bullet1 in controller.bullets)
            {
                controller.EntityMove(bullet1);
            }

            foreach(Tank tank in tanks)
            {
                
                foreach (Bound bound in bounds)
                {
                    if (controller.CheckColisions(tank, bound))
                    {
                        controller.StopNearBorders(tank.Direction, tank);
                        controller.RotateTank(tank);
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
                controller.EntityMove(tank);
            }
            Apple removedApple = null;
            foreach (Apple apple1 in apples)
            {
                if (controller.CheckColisions(apple1, kolobok))
                {
                    score++;
                    removedApple = apple1;
                }
            }
            if(removedApple != null)
            {
                apples.Remove(removedApple);
                while (apples.Count < appleCount)
                {
                    var newApple = controller.NewApple(apples, tanks, kolobok);
                    if (newApple != null)
                    {
                        apples.Add(newApple);
                    }
                }

            }
            
            label1.Text = score.ToString();
            
            if (IsGameOver())
            {
                GameOver();
            }
            Invalidate();

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
