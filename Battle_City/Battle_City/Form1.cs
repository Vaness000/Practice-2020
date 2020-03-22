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
        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
            this.KeyDown += new KeyEventHandler(OnKeyPress);
            Invalidate();
        }
        public void OnKeyPress(object sender, KeyEventArgs e)
        {
            controller.KeyPress(e, kolobok);
        }
       

        private void startButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            timer1.Interval = 8;
            timer1.Tick += new EventHandler(Update);
            timer1.Enabled = true;
            timer1.Start();
        }

        private void Update(object sender, EventArgs e)
        {
            controller.EntityMove(kolobok);
            foreach (Bound bound in bounds)
            {
                if (controller.CheckColisions(kolobok, bound))
                {
                    controller.ChangeDirection(kolobok.Direction, kolobok);
                }
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Height = 750;
        }
    }
}
