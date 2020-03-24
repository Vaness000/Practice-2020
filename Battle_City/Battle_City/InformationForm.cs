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
    public partial class InformationForm : Form
    {
        public InformationForm(int count)
        {
            InitializeComponent();
            dataGridView1.RowCount = count;
        }
        public void UpdateDGW(string obj, int posX, int posY, int row)
        {
            dataGridView1.Rows[row].Cells[0].Value = obj;
            dataGridView1.Rows[row].Cells[1].Value = posX;
            dataGridView1.Rows[row].Cells[2].Value = posY;
        }

        private void InformationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataGridView1.Dispose();
        }
    }
}
