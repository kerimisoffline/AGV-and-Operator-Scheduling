using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Operator_And_AGV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
            
        }
        
       
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click_1(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            MessageBox.Show("Beta versiyondur. Çizelgeleme sayfası butonuna gidiniz. Daha fazla bilgi için mail: kerimyldrm.16@gmail.com");
        }

        public void button12_Click(object sender, EventArgs e)
        {
            FormA formA = new FormA();
            formA.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            FormB formB = new FormB();
            MessageBox.Show("Beta versiyondur. Çizelgeleme sayfası butonuna gidiniz. Daha fazla bilgi için mail: kerimyldrm.16@gmail.com");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            FormHat formH = new FormHat();
            formH.Show();
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Beta versiyondur. Çizelgeleme sayfası butonuna gidiniz. Daha fazla bilgi için mail: kerimyldrm.16@gmail.com");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
