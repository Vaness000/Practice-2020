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
        public Form1(int tankCount)
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(OnKeyPress);
            this.tankCount = tankCount;
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
            KeyPreview = true;
            startButton.Enabled = false;
            
            timer1.Interval = 8;
            timer1.Tick += Handler();
            timer1.Enabled = true;
            timer1.Start();

            timer2.Interval = 500;
            timer2.Tick += TankHandler();
            timer2.Enabled = true;
            timer2.Start();
        }

        private void CreateTanks()
        {
            int positionX = 0;
            int positionY = 0;
            tanks.Clear();
            for (int i = 0; i < tankCount; i++)
            {
                positionX = rnd.Next(50, 600);
                positionY = rnd.Next(50, 600);
                tanks.Add(new Tank(positionX, positionY, i));
                if (i == 0)
                {
                    continue;
                }
                else
                {
                    if (controller.CheckColisions(tanks[tanks.Count - 1], tanks[tanks.Count - 2]))
                    {
                        tanks.RemoveAt(tanks.Count - 1);
                        i--;
                    }
                }
                if (controller.CheckColisions(tanks[i], kolobok))
                {
                    tanks.RemoveAt(i);
                    i--;
                }
            }
        }

        private void Update(object sender, EventArgs e)
        {
            controller.EntityMove(kolobok);
            foreach (Bound bound in bounds)
            {
                if (controller.CheckColisions(kolobok, bound))
                {
                    controller.StopNearBorders(kolobok.Direction, kolobok);
                }
            }
            foreach(Tank tank in tanks)
            {
                controller.EntityMove(tank);
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
                            controller.RotateTank(tank1);
                        }
                    }
                }
            }
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
            foreach(Bound bound in bounds)
            {
                graphics.DrawImage(bound.Image, bound.PositionX, bound.PositionY, bound.Width,bound.Height);
            }
            foreach(Tank tank in tanks)
            {
                graphics.DrawImage(tank.Image, tank.PositionX, tank.PositionY, tank.Width, tank.Height);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Height = 750;
        }
    }
}
