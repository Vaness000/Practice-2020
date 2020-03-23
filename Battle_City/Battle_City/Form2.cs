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
    public partial class Form2 : Form
    {
        public List<Tank> tanks { get; set; }
        public List<Apple> apples { get; set; }
        public Kolobok kolobok { get; set; }
        public Form2(List<Tank> tanks, List<Apple> apples, Kolobok kolobok)
        {
            InitializeComponent();
            this.tanks = tanks;
            this.apples = apples;
            this.kolobok = kolobok;
            dataGridView1.RowCount = 1 + apples.Count + tanks.Count;
            UpdateDGW();

        }
        public void UpdateDGW()
        {
            try
            {
                int row = 0;
                dataGridView1.Rows[row].Cells[0].Value = kolobok.ToString();
                dataGridView1.Rows[row].Cells[1].Value = kolobok.PositionX;
                dataGridView1.Rows[row].Cells[2].Value = kolobok.PositionY;
                row++;
                foreach (Tank tank in tanks)
                {
                    dataGridView1.Rows[row].Cells[0].Value = tank.ToString();
                    dataGridView1.Rows[row].Cells[1].Value = tank.PositionX;
                    dataGridView1.Rows[row].Cells[2].Value = tank.PositionY;
                    row++;
                }
                for (int i = 0; i < apples.Count; i++)
                {
                    dataGridView1.Rows[row].Cells[0].Value = apples[i].ToString() + i.ToString();
                    dataGridView1.Rows[row].Cells[1].Value = apples[i].PositionX;
                    dataGridView1.Rows[row].Cells[2].Value = apples[i].PositionY;
                    row++;
                }
            }
            catch
            {
                this.Close();
            }
        }
    }
}
