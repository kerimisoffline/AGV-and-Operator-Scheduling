using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Operator_And_AGV
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            textBox1.Text = veri_cek(1);
            textBox2.Text = veri_cek(2);
            String veri_cek(int a)
            {
                string b;
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source= veri_baglanti.accdb"); con.Open(); //accdb dosyaları için

                OleDbCommand cmd = new OleDbCommand("Select Alan1 from tablo1 where Kimlik="+a, con);
                OleDbDataReader dr = cmd.ExecuteReader();

                dr.Read();

                b = (dr["Alan1"].ToString());


                con.Close();
                return b;
            }
        }
    }
}
