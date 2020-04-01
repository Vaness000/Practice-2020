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
        List<Entity> entities = new List<Entity>();
        InformationForm infoForm;
        Game game;
        int width;
        int height;
        int speed;
        int tankCount;
        int appleCount;
        int score;
        int removedNum;

        public Battle_City(int tankCount, int appleCount,int width, int height, int speed)
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(OnKeyPress1);
            this.tankCount = tankCount;
            this.appleCount = appleCount;
            this.width = width;
            this.height = height;
            this.game = new Game(width, height,speed);
            this.speed = speed;
            
            controller = new PackmanController(speed);
            Invalidate();
            
        }


        public void OnKeyPress1(object sender, KeyEventArgs e)
        {
            controller.KeyPress(e, game.kolobok);
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
            foreach (Tank tank in game.tanks)
            {
                tank.RotateTank(rnd.Next(1,5));
            }
        }
        public void ExplosionsClear(object sender, EventArgs e)
        {
            game.explosions.Clear();
            game.kolobok.canShot = true;
        }
        private EventHandler ClearExplosion()
        {
            return new EventHandler(ExplosionsClear);
        }

        public void ShootTanks(object sender, EventArgs e)
        {
            foreach (Tank tank in game.tanks)
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
            foreach (Tank tank in game.tanks)
            {
                if (controller.CheckColisions(tank, game.kolobok))
                {
                    return true;
                }
            }
            foreach (Bullet bullet in controller.bullets.Where(x => x.dangerous == true))
            {
                if (controller.CheckColisions(bullet, game.kolobok))
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
            timer2.Tick -= TankShootHandler();
            timer3.Stop();
            timer3.Enabled = false;
            timer3.Tick -= ClearExplosion();
            infoForm.Dispose();
            controller.bullets.Clear();
            game.tanks.Clear();
            game.apples.Clear();
            game.walls.Clear();
            game.rivers.Clear();
            game.explosions.Clear();
            game.blocks.Clear();
            entities.Clear();
            MessageBox.Show("Game Over!" + "\n" + "You score: " + score);
            label1.Text = "0";
            


        }
        private void NewGame()
        {
            game.kolobok.PositionX = 150;
            game.kolobok.PositionX = 150;
            game.CreateRiver();
            game.CreateWalls();
            game.CreateBlock();
            CreateTanks();
            CreateApples();
            entities.Add(game.kolobok);
            entities.AddRange(game.tanks);
            entities.AddRange(game.apples);
            
            KeyPreview = true;
            startButton.Enabled = false;

            timer1.Interval = 10;
            timer1.Tick += Handler();
            timer1.Enabled = true;
            timer1.Start();

            timer2.Interval = 3000;
            timer2.Tick += TankHandler();
            timer2.Tick += TankShootHandler();
            timer2.Enabled = true;
            timer2.Start();

            timer3.Interval = 1000;
            timer3.Enabled = true;
            
            timer3.Tick += ClearExplosion();
            timer3.Start();
            infoForm = new InformationForm(entities.Count);
            infoForm.Show();
            Focus();
            score = 0;
        }

        private void CreateTanks()
        {
            int num = 0;
            game.tanks.Clear();
            while (game.tanks.Count < tankCount)
            {
                num++;
                Tank tank = controller.NewTank(game.tanks, game.kolobok, num, game.walls, game.rivers,game.blocks);
                if (tank != null)
                {
                    game.tanks.Add(tank);
                }
                else
                {
                    continue;
                }
            }
        }
        private void CreateApples()
        {

            game.apples.Clear();
            while (game.apples.Count < appleCount)
            {
                Apple apple = controller.NewApple(game.apples, game.tanks, game.kolobok, game.walls, game.rivers,game.blocks);
                if (apple != null)
                {
                    game.apples.Add(apple);
                }
                else
                {
                    continue;
                }
            }

        }
        public void Unmarked()
        {
            foreach (Tank tank in game.tanks)
            {
                tank.marked = false;
            }
        }

        private void Update(object sender, EventArgs e)
        {
            Unmarked();
            
            Recovery();
            game.kolobok.Move();

            foreach (Bullet bullet1 in controller.bullets)
            { 
                bullet1.Move();
            }

            foreach (Tank tank in game.tanks)
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
            CheckBounds();
            pictureBox1.Invalidate();
        }

        public void CheckBounds()
        {
            foreach (Bound bound in game.bounds)
            {
                if (controller.CheckColisions(game.kolobok, bound))
                {
                    game.kolobok.StopKolobok();
                    break;
                    
                }
            }
            for (int i = 0; i < game.apples.Count; i++)
            {
                if (controller.CheckColisions(game.apples[i], game.kolobok))
                {
                    score++;
                    game.apples.RemoveAt(i);
                }
            }
            for (int i = 0; i < controller.bullets.Count; i++)
            {
                int bullX = controller.bullets[i].PositionX;
                int bullY = controller.bullets[i].PositionY;
                if (bullX < 10 || bullX > width-20 || bullY < 10 || bullY > height-20)
                {
                    game.explosions.Add(new Explosion(controller.bullets[i].PositionX, controller.bullets[i].PositionY));
                    controller.RemoveBullet(i);
                }
            }
            
            for (int i = 0; i < controller.bullets.Count; i++)
            {
                try
                {
                    for (int j = 0; j < game.walls.Count; j++)
                    {
                        if (controller.CheckColisions(game.walls[j], controller.bullets[i]))
                        {
                            game.explosions.Add(new Explosion(controller.bullets[i].PositionX, controller.bullets[i].PositionY));
                            controller.RemoveBullet(i);
                            game.walls.RemoveAt(j);
                        }
                    }
                    foreach(Block block in game.blocks)
                    {
                        if(controller.CheckColisions(block, controller.bullets[i]))
                        {
                            game.explosions.Add(new Explosion(controller.bullets[i].PositionX, controller.bullets[i].PositionY));
                            controller.RemoveBullet(i);
                        }
                    }
                }
                catch
                {
                    continue;
                }
            }


            for (int i = 0; i < game.tanks.Count; i++)
            {
                for (int j = 0; j < controller.bullets.Count; j++)
                {
                    try
                    {
                        if (controller.CheckColisions(controller.bullets[j], game.tanks[i]))
                        {
                            if (!controller.bullets[j].dangerous)
                            {
                                game.explosions.Add(new Explosion(controller.bullets[j].PositionX, controller.bullets[j].PositionY));
                                removedNum = game.tanks[i].numer;
                                game.tanks.RemoveAt(i);
                                controller.RemoveBullet(j);
                                score++;
                            }
                        }
                    }
                    catch { continue; }

                }
            }
            foreach(Wall wall1 in game.walls)
            {
                if (controller.CheckColisions(game.kolobok, wall1))
                {
                    game.kolobok.StopKolobok();
                    break;
                }
            }
            foreach(River river1 in game.rivers)
            {
                if (controller.CheckColisions(game.kolobok, river1))
                {
                    game.kolobok.StopKolobok();
                    break;
                }
            }
            foreach (Block block in game.blocks)
            {
                if (controller.CheckColisions(game.kolobok, block))
                {
                    game.kolobok.StopKolobok();
                    break;
                }
            }

            foreach (Tank tank in game.tanks)
            {
                foreach (Bound bound in game.bounds)
                {
                    if (controller.CheckColisions(tank, bound))
                    {
                        tank.RotateDirection();
                    }
                }
                foreach(Wall wall in game.walls)
                {
                    if (controller.CheckColisions(tank, wall))
                    {
                        tank.RotateDirection();
                        break;
                    }
                }
                foreach(River river in game.rivers)
                {
                    if (controller.CheckColisions(tank, river))
                    {
                        tank.RotateDirection();
                    }
                }
                foreach(Block block in game.blocks)
                {
                    if (controller.CheckColisions(tank, block))
                    {
                        tank.RotateDirection();
                        break;
                    }
                }
            }
            foreach (Tank tank1 in game.tanks)
            {
                foreach(Tank tank in game.tanks)
                {
                    
                    if (!tank.Equals(tank1))
                    {
                        if (controller.CheckColisions(tank, tank1)&&!tank.marked&&!tank.marked)
                        {
                            tank.RotateDirection();
                            tank1.RotateDirection();
                            tank.marked = true;
                            tank1.marked = true;
                        }
                    }
                }
            }
            
        }
        public void Recovery()
        {
            while (game.tanks.Count < tankCount)
            {
                var newTank = controller.NewTank(game.tanks, game.kolobok, removedNum, game.walls, game.rivers,game.blocks);
                if (newTank != null)
                {
                    game.tanks.Add(newTank);
                }
            }
            while (game.apples.Count < appleCount)
            {
                var newApple = controller.NewApple(game.apples, game.tanks, game.kolobok, game.walls, game.rivers,game.blocks);
                if (newApple != null)
                {
                    game.apples.Add(newApple);
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.DrawImage(game.kolobok.Image, game.kolobok.PositionX, game.kolobok.PositionY);
            foreach(Wall wall in game.walls)
            {
                graphics.DrawImage(wall.Image, wall.PositionX, wall.PositionY, wall.Width, wall.Height);
            }
            foreach(River river in game.rivers)
            {
                graphics.DrawImage(river.Image, river.PositionX, river.PositionY, river.Width, river.Height);
            }
            foreach (Apple apple in game.apples)
            {
                graphics.DrawImage(apple.Image, apple.PositionX, apple.PositionY, apple.Width, apple.Height);
            }
            foreach (Bound bound in game.bounds)
            {
                graphics.DrawImage(bound.Image, bound.PositionX, bound.PositionY, bound.Width, bound.Height);
            }
            foreach (Tank tank in game.tanks)
            {
                graphics.DrawImage(tank.Image, tank.PositionX, tank.PositionY, tank.Width, tank.Height);
            }
            foreach (Bullet bullet in controller.bullets)
            {
                graphics.DrawImage(bullet.Image, bullet.PositionX, bullet.PositionY, bullet.Width, bullet.Height);
            }
            foreach(Block block in game.blocks)
            {
                graphics.DrawImage(block.Image, block.PositionX, block.PositionY, block.Width, block.Height);
            }
            foreach(Explosion explosion in game.explosions)
            {
                graphics.DrawImage(explosion.Image, explosion.PositionX, explosion.PositionY, explosion.Width, explosion.Height);
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
