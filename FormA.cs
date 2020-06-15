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
    public partial class FormA : Form
    {
        public FormA()
        {
            InitializeComponent();
        }


        //richtextbox gecikince  renklendirme kırmızı
        
        int kontrol;
         private void checkBox29_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox29.Checked)
            {
                checkBox1.CheckState = CheckState.Checked;
                checkBox5.CheckState = CheckState.Checked;
                checkBox9.CheckState = CheckState.Checked;
                checkBox13.CheckState = CheckState.Checked;
                checkBox17.CheckState = CheckState.Checked;
                checkBox21.CheckState = CheckState.Checked;
                checkBox25.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox1.CheckState = CheckState.Unchecked;
                checkBox5.CheckState = CheckState.Unchecked;
                checkBox9.CheckState = CheckState.Unchecked;
                checkBox13.CheckState = CheckState.Unchecked;
                checkBox17.CheckState = CheckState.Unchecked;
                checkBox21.CheckState = CheckState.Unchecked;
                checkBox25.CheckState = CheckState.Unchecked;
            }
        }
        private void checkBox30_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox30.Checked)
            {
                checkBox2.CheckState = CheckState.Checked;
                checkBox6.CheckState = CheckState.Checked;
                checkBox10.CheckState = CheckState.Checked;
                checkBox14.CheckState = CheckState.Checked;
                checkBox18.CheckState = CheckState.Checked;
                checkBox22.CheckState = CheckState.Checked;
                checkBox26.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox2.CheckState = CheckState.Unchecked;
                checkBox6.CheckState = CheckState.Unchecked;
                checkBox10.CheckState = CheckState.Unchecked;
                checkBox14.CheckState = CheckState.Unchecked;
                checkBox18.CheckState = CheckState.Unchecked;
                checkBox22.CheckState = CheckState.Unchecked;
                checkBox26.CheckState = CheckState.Unchecked;
            }
        }
        private void checkBox31_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox31.Checked)
            {
                checkBox3.CheckState = CheckState.Checked;
                checkBox7.CheckState = CheckState.Checked;
                checkBox11.CheckState = CheckState.Checked;
                checkBox15.CheckState = CheckState.Checked;
                checkBox19.CheckState = CheckState.Checked;
                checkBox23.CheckState = CheckState.Checked;
                checkBox27.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox3.CheckState = CheckState.Unchecked;
                checkBox7.CheckState = CheckState.Unchecked;
                checkBox11.CheckState = CheckState.Unchecked;
                checkBox15.CheckState = CheckState.Unchecked;
                checkBox19.CheckState = CheckState.Unchecked;
                checkBox23.CheckState = CheckState.Unchecked;
                checkBox27.CheckState = CheckState.Unchecked;
            }
        }
        private void checkBox32_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox32.Checked)
            {
                checkBox4.CheckState = CheckState.Checked;
                checkBox8.CheckState = CheckState.Checked;
                checkBox12.CheckState = CheckState.Checked;
                checkBox16.CheckState = CheckState.Checked;
                checkBox20.CheckState = CheckState.Checked;
                checkBox24.CheckState = CheckState.Checked;
                checkBox28.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox4.CheckState = CheckState.Unchecked;
                checkBox8.CheckState = CheckState.Unchecked;
                checkBox12.CheckState = CheckState.Unchecked;
                checkBox16.CheckState = CheckState.Unchecked;
                checkBox20.CheckState = CheckState.Unchecked;
                checkBox24.CheckState = CheckState.Unchecked;
                checkBox28.CheckState = CheckState.Unchecked;
            }
        }
        //********************************************************************************************************
        



        bool[] pawon = new bool[108];
        //     pallet area: working or not
        int[] pft = new int[108];
        //     pft : pallet filling times
        double[] dpa = new double[108];
        //     dpa : distance of pallet areas
        bool[,,] ws = new bool[108, 7, 4];
        //     ws : weekly shifts
        int tfpe = 60;
        //     tfpe : taking full (pallet) and putting empty
        int ptw = 45;
        //     ptw : passed time in warehouse
        int[] at = new int[108];
        //     at : arrival time
        int[] tt = new int[108];
        //     tt : total time

        int atro = 59;

      

        /* TAKING DATA FROM USER */
        // Veri tabanında ki ilgili hatları güncellemek ve yeni hat verisi eklemek için
        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source= veri_baglanti.accdb"); con.Open(); //accdb dosyaları için
            try
            {
               
                byte area;
                area = Convert.ToByte(textBox1.Text);
              
                pawon[area] = true;

                int time;
                time = Convert.ToInt32(Convert.ToDouble(textBox2.Text));
                pft[area] = time;

                double distance;
                distance = Convert.ToDouble(textBox3.Text);
                dpa[area] = distance;

                // veri tabanına ekleme (methdoları çağırıyor)
                status(area);
                update_distance(area);
                update_sure(area);

                at[area] = Convert.ToInt32(dpa[area] / 1.83);
                //     1.83 : coefficient of how many metre can be carried by operator an empty palet in one second 
                tt[area] = Convert.ToInt32(at[area] + tfpe + (Convert.ToInt32(dpa[area] / 1.55)) + ptw);
                //     1.55 : coefficient of how many metre can be carried by operator an full palet in one second 




            // Veri Tabanı İşlemleri

            double update_distance(int a)
            {

                double b;

                OleDbCommand cmd = new OleDbCommand("UPDATE Tablo2 Set uzaklik='" + distance + "' where index=" + a, con);

                cmd.ExecuteNonQuery();
                
                b = distance;

                return b;

            }

            double update_sure(int a)
            {
                double b;

                OleDbCommand cmd = new OleDbCommand("UPDATE Tablo2 Set sure='" + time + "' where index=" + a, con);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Veri başarıyla eklendi" + Environment.NewLine);
                b = time;

                return b;
            }
            

            bool status(int a)
            {
                Boolean b;

                OleDbCommand cmd = new OleDbCommand("UPDATE Tablo2 Set kayit='" + 1 + "' where index=" + a, con);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Veri başarıyla eklendi" + Environment.NewLine);
                b = pawon[a];

                return b;

            }



            /* SHIFTS INFORMATION WILL BE TAKEN FROM CHECKBOXES */
            /*
            if (checkBox1.Checked)
            {
                ws[area, 0, 0] = true;
            }
            if (checkBox2.Checked)
            {
                ws[area, 0, 1] = true;
            }
            if (checkBox3.Checked)
            {
                ws[area, 0, 2] = true;
            }
            if (checkBox4.Checked)
            {
                ws[area, 0, 3] = true;
            }
            if (checkBox5.Checked)
            {
                ws[area, 1, 0] = true;
            }
            if (checkBox6.Checked)
            {
                ws[area, 1, 1] = true;
            }
            if (checkBox7.Checked)
            {
                ws[area, 1, 2] = true;
            }
            if (checkBox8.Checked)
            {
                ws[area, 1, 3] = true;
            }
            if (checkBox9.Checked)
            {
                ws[area, 2, 0] = true;
            }
            if (checkBox10.Checked)
            {
                ws[area, 2, 1] = true;
            }
            if (checkBox11.Checked)
            {
                ws[area, 2, 2] = true;
            }
            if (checkBox12.Checked)
            {
                ws[area, 2, 3] = true;
            }
            if (checkBox13.Checked)
            {
                ws[area, 3, 0] = true;
            }
            if (checkBox14.Checked)
            {
                ws[area, 3, 1] = true;
            }
            if (checkBox15.Checked)
            {
                ws[area, 3, 2] = true;
            }
            if (checkBox16.Checked)
            {
                ws[area, 3, 3] = true;
            }
            if (checkBox17.Checked)
            {
                ws[area, 4, 0] = true;
            }
            if (checkBox18.Checked)
            {
                ws[area, 4, 1] = true;
            }
            if (checkBox19.Checked)
            {
                ws[area, 4, 2] = true;
            }
            if (checkBox20.Checked)
            {
                ws[area, 4, 3] = true;
            }
            if (checkBox21.Checked)
            {
                ws[area, 5, 0] = true;
            }
            if (checkBox22.Checked)
            {
                ws[area, 5, 1] = true;
            }
            if (checkBox23.Checked)
            {
                ws[area, 5, 2] = true;
            }
            if (checkBox24.Checked)
            {
                ws[area, 5, 3] = true;
            }
            if (checkBox25.Checked)
            {
                ws[area, 6, 0] = true;
            }
            if (checkBox26.Checked)
            {
                ws[area, 6, 1] = true;
            }
            if (checkBox27.Checked)
            {
                ws[area, 6, 2] = true;
            }
            if (checkBox28.Checked)
            {
                ws[area, 6, 3] = true;
            }
            textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = "";
            */
            // vardiya veri tabanı güncelleme

            /*
            bool status_shift(int a)
            {
                Boolean b;

                OleDbCommand cmd = new OleDbCommand("UPDATE Tablo3 Set kayit='" + 1 + "' where index=" + a, con);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Veri başarıyla eklendi" + Environment.NewLine);
                b = pawon[a];

                return b;

            }
            */
            con.Close();

            }
            catch (Exception)
            {

                MessageBox.Show("Hatalı veri girişi");

            }

        }

        // veri girişi bitiş...



        /* SCHEDULING ALGORITHM: */
        private void button2_Click(object sender, EventArgs e)
        {

          

            richTextBox1.Clear();
            richTextBox2.Clear();
            if (kontrol==0)
            {
                MessageBox.Show("Lütfen Önce veri giriniz!");
            }
            
          for (int i=1; i < 108; i++)
            {
               if (pawon[i] == true)
                {
                   if (i <= 27)
                    {
                    
                        at[i] = Convert.ToInt32(dpa[i] / 1.15);
                        tt[i] = Convert.ToInt32(at[i] + tfpe + (atro - dpa[i]) * 10 + ptw);

                    }
                    else
                    {
                        at[i] = Convert.ToInt32(dpa[i] / 1.83);
                        tt[i] = Convert.ToInt32(at[i] + tfpe + (Convert.ToInt32(dpa[i] / 0.55)) + ptw);
                    }
                }

            }

            // TOPLAM AGV YOLU 59 metre  atro = 59, TOPLAM DOLU YOLCULUK
            // area 8
            /*
            pawon[8] = true;
            pft[8] = Convert.ToInt32(144.1 * 60);
            dpa[8] = 2;
            at[8] = Convert.ToInt32(dpa[8] / 0.15);
            tt[8] = Convert.ToInt32(at[8] + tfpe + (atro - dpa[8]) * 10 + ptw);

            // area 9
            pawon[9] = true;
            pft[9] = Convert.ToInt32(150 * 60);
            dpa[9] = 3.5;
            at[9] = Convert.ToInt32(dpa[9] / 0.15);
            tt[9] = Convert.ToInt32(at[9] + tfpe + (atro - dpa[9]) * 10 + ptw);
            //area 11
            pawon[11] = true;
            pft[11] = Convert.ToInt32(346 * 60);
            dpa[11] = 5.5;
            at[11] = Convert.ToInt32(dpa[11] / 0.15);
            tt[11] = Convert.ToInt32(at[11] + tfpe + (atro - dpa[11]) * 10 + ptw);
            //area 12
            pawon[12] = true;
            pft[12] = Convert.ToInt32(120 * 60);
            dpa[12] = 6.5;
            at[12] = Convert.ToInt32(dpa[12] / 0.15);
            tt[12] = Convert.ToInt32(at[12] + tfpe + (atro - dpa[12]) * 10 + ptw);

            // area 13
            pawon[13] = true;
            pft[13] = Convert.ToInt32(130 * 60);
            dpa[13] = 7;
            at[13] = Convert.ToInt32(dpa[13] / 0.15);
            tt[13] = Convert.ToInt32(at[13] + tfpe + (atro - dpa[13]) * 10 + ptw);
            // area 14
            pawon[14] = true;
            pft[14] = Convert.ToInt32(500 * 60);
            dpa[14] = 7.5;
            at[14] = Convert.ToInt32(dpa[14] / 0.15);
            tt[14] = Convert.ToInt32(at[14] + tfpe + (atro - dpa[14]) * 10 + ptw);
            // area 17
            pawon[17] = true;
            pft[17] = Convert.ToInt32(280 * 60);
            dpa[17] = 12.7;
            at[17] = Convert.ToInt32(dpa[17] / 0.15);
            tt[17] = Convert.ToInt32(at[17] + tfpe + (atro - dpa[17]) * 10 + ptw);
            // area 23
            pawon[23] = true;
            pft[23] = Convert.ToInt32(733.3 * 60);
            dpa[23] = 24;
            at[23] = Convert.ToInt32(dpa[23] / 0.15);
            tt[23] = Convert.ToInt32(at[23] + tfpe + (atro - dpa[23]) * 10 + ptw);
            // area 24
            pawon[24] = true;
            pft[24] = Convert.ToInt32(503.3 * 60);
            dpa[24] = 24.5;
            at[24] = Convert.ToInt32(dpa[24] / 0.15);
            tt[24] = Convert.ToInt32(at[24] + tfpe + (atro - dpa[24]) * 10 + ptw);

            pawon[38] = true;
            pft[38] = Convert.ToInt32(346 * 60);
            dpa[38] = 9.3;
            at[38] = Convert.ToInt32(dpa[38] / 1.83); 
            tt[38] = Convert.ToInt32(at[38] + tfpe + (Convert.ToInt32(dpa[38] / 1.55)) + ptw);

            pawon[41] = true;
            pft[41] = Convert.ToInt32(120 * 60);
            dpa[41] = 11.9;
            at[41] = Convert.ToInt32(dpa[41] / 1.83);
            tt[41] = Convert.ToInt32(at[41] + tfpe + (Convert.ToInt32(dpa[41] / 1.55)) + ptw);

            pawon[46] = true;
            pft[46] = Convert.ToInt32(553.3 * 60);
            dpa[46] = 17.5;
            at[46] = Convert.ToInt32(dpa[46] / 1.83);
            tt[46] = Convert.ToInt32(at[46] + tfpe + (Convert.ToInt32(dpa[46] / 1.55)) + ptw);

            pawon[48] = true;
            pft[48] = Convert.ToInt32(960 * 60);
            dpa[48] = 20;
            at[48] = Convert.ToInt32(dpa[48] / 1.83);
            tt[48] = Convert.ToInt32(at[48] + tfpe + (Convert.ToInt32(dpa[48] / 1.55)) + ptw);

            pawon[57] = true;
            pft[57] = Convert.ToInt32(768 * 60);
            dpa[57] = 4.7;
            at[57] = Convert.ToInt32(dpa[57] / 1.83);
            tt[57] = Convert.ToInt32(at[57] + tfpe + (Convert.ToInt32(dpa[57] / 1.55)) + ptw);

            pawon[60] = true;
            pft[60] = Convert.ToInt32(466.6 * 60);
            dpa[60] = 8.3;
            at[60] = Convert.ToInt32(dpa[60] / 1.83);
            tt[60] = Convert.ToInt32(at[60] + tfpe + (Convert.ToInt32(dpa[60] / 1.55)) + ptw);

            pawon[65] = true;
            pft[65] = Convert.ToInt32(240 * 60);
            dpa[65] = 12.7;
            at[65] = Convert.ToInt32(dpa[65] / 1.83);
            tt[65] = Convert.ToInt32(at[65] + tfpe + (Convert.ToInt32(dpa[65] / 1.55)) + ptw);

            pawon[67] = true;
            pft[67] = Convert.ToInt32(175 * 60);
            dpa[67] = 14.9;
            at[67] = Convert.ToInt32(dpa[67] / 1.83);
            tt[67] = Convert.ToInt32(at[67] + tfpe + (Convert.ToInt32(dpa[67] / 1.55)) + ptw);

            pawon[73] = true;
            pft[73] = Convert.ToInt32(236.6 * 60);
            dpa[73] = 20;
            at[73] = Convert.ToInt32(dpa[73] / 1.83);
            tt[73] = Convert.ToInt32(at[73] + tfpe + (Convert.ToInt32(dpa[73] / 1.55)) + ptw);

            pawon[76] = true;
            pft[76] = Convert.ToInt32(115.2 * 60);
            dpa[76] = 22.7;
            at[76] = Convert.ToInt32(dpa[76] / 1.83);
            tt[76] = Convert.ToInt32(at[76] + tfpe + (Convert.ToInt32(dpa[76] / 1.55)) + ptw);

            pawon[78] = true;
            pft[78] = Convert.ToInt32(185.4 * 60);
            dpa[78] = 24.5;
            at[78] = Convert.ToInt32(dpa[78] / 1.83);
            tt[78] = Convert.ToInt32(at[78] + tfpe + (Convert.ToInt32(dpa[78] / 1.55)) + ptw);

            pawon[82] = true;
            pft[82] = Convert.ToInt32(768 * 60);
            dpa[82] = 3.5;
            at[82] = Convert.ToInt32(dpa[82] / 1.83);
            tt[82] = Convert.ToInt32(at[82] + tfpe + (Convert.ToInt32(dpa[82] / 1.55)) + ptw);

            pawon[90] = true;
            pft[90] = Convert.ToInt32(240 * 60);
            dpa[90] = 11.5;
            at[90] = Convert.ToInt32(dpa[90] / 1.83);
            tt[90] = Convert.ToInt32(at[90] + tfpe + (Convert.ToInt32(dpa[90] / 1.55)) + ptw);

            
            */
            
            /* SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK */


            
            
            int pftbs = 0;
            //     pftbs : pallet filling times based on shifts
            int[] rt = new int[108];
            //     rt : remaining times
            int[] fpi = new int[0];
            //     fpi : filled pallet index 
            int[] fps = new int[0];
            //     fpt : filled pallet schedule;
            int[] olt = new int[0];
            //     olt : operator leaving times
            int[] oct = new int[0];
            //     oct : operator comeback time
            int aic = 0;
            //     aic : array index counter
            int haftalikGecikme = 0;
            // senaryo için
            int senaryo = 0;



            /* MAIN SCHEDULING */

            for (byte dow = 0; dow < 7; dow++)
            // dow : days of a week
            {
                for (byte pi = 0; pi < 108; pi++)
                // pi : pallet index
                {
                    if (pawon[pi] == true)
                    /* if any value entered to pallet index */
                    {
                        for (byte sod = 0; sod < 4; sod++)
                        // sod : shifts of the day
                        {
                            if (ws[pi, dow, sod] == true)
                            {
                                int ets = 0;
                                // es : end time of the shift
                                if (sod == 0)
                                {
                                    pftbs = 0;
                                    ets = 28800;
                                    // ets = 26100;
                                }
                                else if (sod == 1)
                                {
                                    pftbs = 28800;
                                    // pftbs = 26100;
                                    ets = 57600;
                                    // ets = 52200;
                                }
                                else if (sod == 2)
                                {
                                    pftbs = 57600;
                                    //  pftbs = 52200;
                                    ets = 86400;
                                    // ets = 78300;
                                }
                                else if (sod == 3)
                                {
                                    pftbs = 28800;
                                    // pftbs = 28800;
                                    ets = 72000;
                                    // ets = 67500;
                                }
                                pftbs -= rt[pi];
                                while (pftbs + pft[pi] <= ets)
                                {
                                    pftbs += pft[pi];
                                    Array.Resize(ref fpi, aic + 1);
                                    fpi[aic] = pi;
                                    Array.Resize(ref fps, aic + 1);
                                    fps[aic] = pftbs;
                                    Array.Resize(ref olt, aic + 1);
                                    olt[aic] = pftbs - at[fpi[aic]];
                                    Array.Resize(ref oct, aic + 1);
                                    oct[aic] = olt[aic] + tt[fpi[aic]];
                                    aic += 1;
                                }
                                rt[pi] = ets - pftbs;
                            }
                        }
                    }
                }
                aic = 0;



                
                /* DAY HEADERS IN SCHEDULE */

                if (dow == 0)
                {
                    richTextBox1.SelectionColor = Color.DarkBlue;
                    richTextBox1.AppendText("----------\tPAZARTESİ\t----------\n");
                    richTextBox2.AppendText("----------\tPAZARTESİ\t----------\n" + Environment.NewLine);
                    richTextBox2.AppendText("Çıkış Zamanı:\t   "+ "Kaçıncı Palet:\t\n" + Environment.NewLine);
                    richTextBox3.AppendText("----------\tPAZARTESİ\t----------\n" + Environment.NewLine);
                    richTextBox3.AppendText("Çıkış Zamanı:\t    " + "Kaçıncı Palet:\t\n" + Environment.NewLine);
                    richTextBox4.AppendText("----------\tPAZARTESİ\t----------\n");
                }
                else if (dow == 1)
                {
                    richTextBox1.SelectionColor = Color.DarkBlue;
                    richTextBox1.AppendText("\n\n----------\tSALI\t----------\n");
                    richTextBox2.AppendText("\n\n----------\tSALI\t----------\n" + Environment.NewLine);
                    richTextBox2.AppendText("Çıkış Zamanı:\t    " + "Kaçıncı Palet:\t\n" + Environment.NewLine);
                    richTextBox3.AppendText("\n\n----------\tSALI\t----------\n" + Environment.NewLine);
                    richTextBox3.AppendText("Çıkış Zamanı:\t    " + "Kaçıncı Palet:\t\n" + Environment.NewLine);
                    richTextBox4.AppendText("\n\n----------\tSALI\t----------\n");
                }
                else if (dow == 2)
                {
                    richTextBox1.SelectionColor = Color.DarkBlue;
                    richTextBox1.AppendText("\n\n----------\tÇARŞAMBA\t----------\n");
                    richTextBox2.AppendText("\n\n----------\tÇARŞAMBA\t----------\n" + Environment.NewLine);
                    richTextBox2.AppendText("Çıkış Zamanı:\t   " + "Kaçıncı Palet:\t\n" + Environment.NewLine);
                    richTextBox3.AppendText("\n\n----------\tÇARŞAMBA\t----------\n" + Environment.NewLine);
                    richTextBox3.AppendText("Çıkış Zamanı:\t    " + "Kaçıncı Palet:\t\n" + Environment.NewLine);
                    richTextBox4.AppendText("\n\n----------\tÇARŞAMBA\t----------\n");
                }
                else if (dow == 3)
                {
                    richTextBox1.SelectionColor = Color.DarkBlue;
                    richTextBox1.AppendText("\n\n----------\tPERŞEMBE\t----------\n");
                    richTextBox2.AppendText("\n\n----------\tPERŞEMBE\t----------\n" + Environment.NewLine);
                    richTextBox2.AppendText("Çıkış Zamanı:\t   " + "Kaçıncı Palet:\t\n" + Environment.NewLine);
                    richTextBox3.AppendText("\n\n----------\tPERŞEMBE\t----------\n" + Environment.NewLine);
                    richTextBox3.AppendText("Çıkış Zamanı:\t    " + "Kaçıncı Palet:\t\n" + Environment.NewLine);
                    richTextBox4.AppendText("\n\n----------\tPERŞEMBE\t----------\n");
                }
                else if (dow == 4)
                {
                    richTextBox1.SelectionColor = Color.DarkBlue;
                    richTextBox1.AppendText("\n\n----------\tCUMA\t----------\n");
                    richTextBox2.AppendText("\n\n----------\tCUMA\t----------\n" + Environment.NewLine);
                    richTextBox2.AppendText("Çıkış Zamanı:\t   " + "Kaçıncı Palet:\t\n" + Environment.NewLine);
                    richTextBox3.AppendText("\n\n----------\tCUMA\t----------\n" + Environment.NewLine);
                    richTextBox3.AppendText("Çıkış Zamanı:\t    " + "Kaçıncı Palet:\t\n" + Environment.NewLine);
                    richTextBox4.AppendText("\n\n----------\tCUMA\t----------\n");
                } 
                else if (dow == 5)
                {
                    richTextBox1.SelectionColor = Color.DarkBlue;
                    richTextBox1.AppendText("\n\n----------\tCUMARTESİ\t----------\n");
                    richTextBox2.AppendText("\n\n----------\tCUMARTESİ\t----------\n" + Environment.NewLine);
                    richTextBox2.AppendText("Çıkış Zamanı:\t   " + "Kaçıncı Palet:\t\n" + Environment.NewLine);
                    richTextBox3.AppendText("\n\n----------\tCUMARTESİ\t----------\n" + Environment.NewLine);
                    richTextBox3.AppendText("Çıkış Zamanı:\t    " + "Kaçıncı Palet:\t\n" + Environment.NewLine);
                    richTextBox4.AppendText("\n\n----------\tCUMARTESİ\t----------\n");
                }
                else if (dow == 6)
                {
                    richTextBox1.SelectionColor = Color.DarkBlue;
                    richTextBox1.AppendText("\n\n----------\tPAZAR\t----------\n");
                    richTextBox2.AppendText("\n\n----------\tPAZAR\t----------\n" + Environment.NewLine);
                    richTextBox2.AppendText("Çıkış Zamanı:\t   " + "Kaçıncı Palet:\t\n" + Environment.NewLine);
                    richTextBox3.AppendText("\n\n----------\tPAZAR\t----------\n" + Environment.NewLine);
                    richTextBox3.AppendText("Çıkış Zamanı:\t    " + "Kaçıncı Palet:\t\n" + Environment.NewLine);
                    richTextBox4.AppendText("\n\n----------\tPAZAR\t----------\n");
                }             




                /* olt[] SORTING */

                for (int i = 0; i < olt.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < (olt.GetUpperBound(0) - 1); j++)
                    {
                        if (olt[j] > olt[j + 1])
                        {
                            int ram;

                            ram = fpi[j];
                            fpi[j] = fpi[j + 1];
                            fpi[j + 1] = ram;

                            ram = olt[j];
                            olt[j] = olt[j + 1];
                            olt[j + 1] = ram;

                            ram = fps[j];
                            fps[j] = fps[j + 1];
                            fps[j + 1] = ram;

                            ram = oct[j];
                            oct[j] = oct[j + 1];
                            oct[j + 1] = ram;
                        }
                    }
                }



                
                /* OPERATOR AND AGV ASSINGMENT */
                
                bool operatoronebusyoridle = false;
                int operatoronewentto = 0;
                bool agvbusyoridle = false;
                int agvwentto = 0;

               
                int counter = 0;
                int reasonabledelay;
                int unreasonabledelay;

                int totalreasonabledelay=0;
                int totalunreasonabledelay=0;
                int totaldelay=0;

                // saniyeyi dakikaya çevirmek için
                long secoder;
                TimeSpan timer;
                
                
                bool delaying = false;
                int delaytime=0;
                int sumDelay;
                int util = 0;
                int zararsızUtil = 0;
                // olt süresi tutmak için
                int[] olter = new int[200];
                //molalar
                /*
                bool cay_molaB = false;
                bool yemek_molaB = false;
                int cay_mola = 900;
                int yemek_mola = 1800;
                */

                //     saniyeyi dakikaya çevirmek için
                

                for (int i = 0; i < 90000; i++) 
                
                {
                   // molalar
                   /*
                    if (i <= 7200)
                    {
                        cay_molaB = false;
                        yemek_molaB = false;
                    }
                    else if(i>7200 && i<8100)
                    {
                        cay_molaB = true;
                        yemek_molaB = false;
                    }
                    else if(i<=14400)
                    {
                        cay_molaB = false;
                        yemek_molaB = false;
                    }
                    else if(i > 14400 && i < 16200)
                    {
                        cay_molaB = false;
                        yemek_molaB = true;
                    }
                    else if(i <= 43200)
                    {
                        cay_molaB = false;
                        yemek_molaB = false;
                    }
                    else if (i > 43200 && i < 44100)
                    {
                        cay_molaB = true;
                        yemek_molaB = false;
                    }
                    else if (i <= 50400)
                    {
                        cay_molaB = false;
                        yemek_molaB = false;
                    }
                    else if (i > 50400 && i < 52200)
                    {
                        cay_molaB = false;
                        yemek_molaB = true;
                    }
                    else if (i <= 72000)
                    {
                        cay_molaB = false;
                        yemek_molaB = false;
                    }
                    else if (i > 72000 && i < 72900)
                    {
                        cay_molaB = true;
                        yemek_molaB = false;
                    }
                    else if (i <= 79200)
                    {
                        cay_molaB = false;
                        yemek_molaB = false;
                    }
                    else if (i > 79200 && i < 81000)
                    {
                        cay_molaB = false;
                        yemek_molaB = true;
                    }
                    else
                    {
                        cay_molaB = false;
                        yemek_molaB = false;
                    }

                    */
                    // birinci operatör meşgulse ve birinci operatörün dönüş süresi geldiyse; 
                    if (operatoronebusyoridle == true && oct[operatoronewentto] < i)
                    {
                        // birinci operatörün durumunu müsait olarak değiştir.
                        operatoronebusyoridle = false;
                    }

                    // oct[agvwentto]
                    if (agvbusyoridle == true && oct[agvwentto] < i )
                    {
                        // agv boşa alınır, 
                        agvbusyoridle = false;
                      
                    }



                    try
                    {
                        // eğer operatörün i saniyeside yola çıkması gerekiyorsa,
                        if (olt[counter] == i)
                        {
                            // taşıma için gecikme zamanı kabul edilebilir mi kontrol eder. 16 kasa 1 palet eder,

                             

                            // eğer bu taşıma işlemi günün son işlemi değilse,
                            if (counter != olt.GetUpperBound(0))
                            {
                                // eğer bu taşıma için warehouse tan çıkış zamanı bir sonraki taşıma için warehouse tan çıkış zamanıyla aynı zamandaysa,
                                if (olt[counter] == olt[counter + 1])
                                {
                                    // i. saniyeyi bir eksilt. böylece program bir sonraki taşıma işleminin zamanına eşit olmadığı için olt[counter + 1] i atlamayacak.
                                    i -= 1;
                                }
                            }

                            reasonabledelay = 0;
                            unreasonabledelay = 0;

                            // eğer bahsi geçen palet operatörün sorumluluk alanındaysa;
                            if (fpi[counter] > 27)
                            {
                                
                                // eğer birinci operatör atıl durumdaysa;
                                if (operatoronebusyoridle == false)
                                {
                                    // birinci operatörü ata ve durumunu meşgule çevir.
                                    operatoronebusyoridle = true;

                                    // birinci operatörün kaçıncı işlemi yapıyor olduğunu aklında tut.
                                    operatoronewentto = counter;

                                    // tolerans aralığındaki gecikmeyi hesapla.
                                    reasonabledelay = 0;

                                    // tolerans aralığındaki gecikmelerin toplamına ekle.
                                    totalreasonabledelay += reasonabledelay;

                                    // tolerans aralığı dışındaki gecikmeyi hesapla.
                                    unreasonabledelay = 0;

                                    // tolerans aralığı dışındaki gecikmelerin toplamına ekle.
                                    totalunreasonabledelay += unreasonabledelay;
                                    delaying = false;

                                    
                                    // atammayı form a yazdır.
                                    richTextBox1.AppendText((counter + 1) + "\t" + olt[counter] + "\t\tOPERATOR\tPALLET INDEX : " + fpi[counter] + Environment.NewLine);
                                    
                                   
                                        secoder = Convert.ToInt64(olt[counter]);
                                        timer = TimeSpan.FromSeconds(secoder).Add(TimeSpan.FromMinutes(-7));
                                        string saat = timer.ToString(@"hh\:mm");
                                        richTextBox3.AppendText(saat + "   \t\tPALLET INDEX : " + fpi[counter] + Environment.NewLine);
                                    

                                }

                                //  operatör  meşgulse;
                                else if (operatoronebusyoridle == true)
                                {
                                    // gecikme süresini hesapla;
                                    delaytime = oct[operatoronewentto] - (olt[counter] + pft[fpi[counter]] / 16);
                                    
                                    // eğer gecikme miktarı tolerans sınırlarının dışındaysa (unreasonabledelay);
                                    if (delaytime > 0)
                                    {

                                        // işe operatörü ata ve operatörün durumunu meşgule çevir.
                                        operatoronebusyoridle = true;

                                        // operatörün hangi işle meşgul olduğunu hafızada tut.
                                        operatoronewentto = counter;

                                        // tolerans aralığındaki gecikmeyi hesapla.
                                        reasonabledelay = pft[fpi[counter]] / 16;
                                        // tolerans aralığındaki gecikmelerin toplamına ekle.
                                        totalreasonabledelay += reasonabledelay;

                                        // tolerans aralığı dışındaki gecikmeyi hesapla.
                                        unreasonabledelay = delaytime;
                                        // tolerans aralığı dışındaki gecikmelerin toplamına ekle.
                                        totalunreasonabledelay += unreasonabledelay;

                                        // tüm gecikmelerin toplamına ekle.
                                        totaldelay += reasonabledelay + unreasonabledelay;

                                        // yola çıkmadaki gecikmeyi dönüşe de yansıt.
                                        oct[counter] += reasonabledelay + unreasonabledelay;

                                        
                                        richTextBox1.SelectionColor = Color.White;
                                        richTextBox1.SelectionBackColor = Color.Red;

                                        richTextBox1.AppendText((counter + 1) + "\t" + (olt[counter] + unreasonabledelay + reasonabledelay) + "\t\tOPERATOR\tPALLET INDEX : " + fpi[counter] + "\t İstenmeyen Gecikme: " + unreasonabledelay+ Environment.NewLine);
                                       
                                        // gecikme yazdırma
                                        richTextBox4.AppendText(fpi[counter] + " .Hat Alanı Gecikme " + unreasonabledelay + " sn" + Environment.NewLine);

                                        secoder = Convert.ToInt64(olt[counter]+unreasonabledelay+reasonabledelay);
                                        timer = TimeSpan.FromSeconds(secoder).Add(TimeSpan.FromMinutes(-7));
                                        string merci = timer.ToString(@"hh\:mm");
                                        richTextBox3.AppendText(merci + "   \t\tPALLET INDEX : " + fpi[counter] + Environment.NewLine);
                                    }
                                    else
                                    {


                                        // birinci operatörün durumunu meşgule çevir.
                                        operatoronebusyoridle = true;

                                        // birinci operatörün hangi işlemle meşgul olduğunu hafızada tut.
                                        operatoronewentto = counter;

                                        // içerde delaytime 0 dan küçük

                                        delaytime = oct[operatoronewentto] - (olt[counter]);

                                        // tolerans aralığındaki gecikmeyi hesapla.
                                        reasonabledelay = delaytime + (pft[fpi[counter]] / 16);
                                        // tolerans aralığındaki gecikmelerin toplamına ekle.
                                        totalreasonabledelay += reasonabledelay;

                                        // tolerans aralığı dışındaki gecikme olmadığı için aralık dışındaki gecikmeler eklenmez.
                                        unreasonabledelay = 0;

                                        // tüm gecikmelerin toplamına ekle.
                                        totaldelay += reasonabledelay;

                                        // yola çıkmadaki gecikmeyi dönüşe de yansıt.
                                        oct[counter] += reasonabledelay;


                                        secoder = Convert.ToInt64(olt[counter] + reasonabledelay);
                                        timer = TimeSpan.FromSeconds(secoder).Add(TimeSpan.FromMinutes(-7));
                                        string merci = timer.ToString(@"hh\:mm");
                                        richTextBox3.AppendText(merci + "   \t\tPALLET INDEX : " + fpi[counter] + Environment.NewLine);

                                        richTextBox1.AppendText((counter + 1) + "\t" + (olt[counter]+reasonabledelay) + "\t\tOPERATÖR\tPALLET INDEX : " + fpi[counter] + "\t Kabul Edilebilir Gecikme: " + (reasonabledelay) +" sn"  + Environment.NewLine);

                                    }
                                    // atamayı forma gecikme zamanıyla birlikte yazdır.
                                   
                                    
                                   
                                }
                                // yapılacak işlem indeksini bir arttır.
                                counter += 1;
                            }
                            // eğer bahsi geçen palet agv nin sorumluluk alanındaysa;
                            else
                            {
                               
                                if (agvbusyoridle == false)
                                {
                                    // AGV ata ve durumunu meşgule çevir.
                                    agvbusyoridle = true;
                                    

                                    // AGV kaçıncı işlemi yapıyor olduğunu aklında tut.
                                    agvwentto = counter;

                                    reasonabledelay = 0;
                                    unreasonabledelay = 0;

                                    // atanmayı form a yazdır.
                                    richTextBox1.AppendText((counter + 1) + "\t" + (olt[counter]) + "\t\tAGV\t\t  PALLET INDEX : " + fpi[counter] + Environment.NewLine);
                                    
                                    
                                    secoder = Convert.ToInt64(olt[counter]);
                                    timer = TimeSpan.FromSeconds(secoder).Add(TimeSpan.FromMinutes(-7));
                                    string saat = timer.ToString(@"hh\:mm");
                                    richTextBox2.AppendText(saat + "   \t\tPALLET INDEX : " + fpi[counter] + Environment.NewLine);
                                   // string.Format("{0,D2} Dakika", timer.Minutes)
                                }
                                // AGV meşgul, operatör atılsa;
                                else if (agvbusyoridle == true && operatoronebusyoridle == false)
                                {
                                    // ikinci operatörü ata ve durumunu meşgule çevir.
                                    operatoronebusyoridle = true;

                                    // ikinci operatörün kaçıncı işlemi yapıyor olduğunu aklında tut.
                                    operatoronewentto = counter;

                                    reasonabledelay = 0;
                                    unreasonabledelay = 0;

                                    // atamayı form a yazdır.
                                    richTextBox1.AppendText((counter + 1) + "\t" + olt[counter] + "\t\tOPERATÖR\tPALLET INDEX : " + fpi[counter] + Environment.NewLine);
                                   
                                    secoder = Convert.ToInt64(olt[counter]);
                                    timer = TimeSpan.FromSeconds(secoder).Add(TimeSpan.FromMinutes(-7));
                                    string merci = timer.ToString(@"hh\:mm");
                                    richTextBox3.AppendText(merci + "   \t\tPALLET INDEX : " + fpi[counter] + Environment.NewLine);

                                }
                                // hem AGV hem de operatör meşgulse en yakın uygunu ata;
                                else if (operatoronebusyoridle == true && agvbusyoridle == true)
                                {
                                  
                                    // eğer operatörün işinin bitiş agv'nin işinin bitiş süresinden daha  erkense ya da işleri aynı saniyede bitiyorsa;
                                    if (oct[operatoronewentto] <= oct[agvwentto])
                                    {
                                        // taşıma için gecikme zamanını hesapla.
                                        delaytime = oct[operatoronewentto] - (olt[counter] + pft[fpi[counter]] / 16);

                                        // birinci operatörün durumunu meşgule çevir.
                                        operatoronebusyoridle = true;

                                        // birinci operatörün hangi işlemle meşgul olduğunu hafızada tut.
                                        operatoronewentto = counter;
                                        if (delaytime > 0)
                                        {
                                            // işe operatörü ata ve operatörün durumunu meşgule çevir.
                                            operatoronebusyoridle = true;

                                            // operatörün hangi işle meşgul olduğunu hafızada tut.
                                            operatoronewentto = counter;
                                            
                                            // tolerans aralığındaki gecikmeyi hesapla.
                                            reasonabledelay = pft[fpi[counter]] / 16;
                                            // tolerans aralığındaki gecikmelerin toplamına ekle.
                                            totalreasonabledelay += reasonabledelay;

                                            // tolerans aralığı dışındaki gecikmeyi hesapla.
                                            unreasonabledelay = delaytime;
                                            // tolerans aralığı dışındaki gecikmelerin toplamına ekle.
                                            totalunreasonabledelay += unreasonabledelay;

                                            // gecikmeleri, gecikmeler toplamına ekle.
                                            totaldelay += reasonabledelay + unreasonabledelay;

                                            // yola çıkmadaki gecikmeyi dönüşe de yansıt.
                                            oct[counter] += reasonabledelay + unreasonabledelay;

                                            richTextBox1.SelectionColor = Color.White;
                                            richTextBox1.SelectionBackColor = Color.Red;
                                            
                                            
                                            richTextBox1.AppendText((counter + 1) + "\t" + (olt[counter]+reasonabledelay+unreasonabledelay) + "\t\tOPERATÖR\tPALLET INDEX : " + fpi[counter] +"\t İstenmeyen Gecikme" + unreasonabledelay+ Environment.NewLine);
                                            // gecikme yazdırma
                                            richTextBox4.AppendText(fpi[counter] + " .Hat Alanı Gecikme " + unreasonabledelay + " sn" + Environment.NewLine);

                                            secoder = Convert.ToInt64(olt[counter] + unreasonabledelay+reasonabledelay);
                                            timer = TimeSpan.FromSeconds(secoder).Add(TimeSpan.FromMinutes(-7));
                                            string merci = timer.ToString(@"hh\:mm");
                                            richTextBox3.AppendText(merci + "   \t\tPALLET INDEX : " + fpi[counter] + Environment.NewLine);
                                        }
                                        else
                                        {
                                            // operatörün durumunu meşgule çevir.
                                            operatoronebusyoridle = true;

                                            // operatörün hangi işle meşgul olduğunu hafızada tut.
                                            operatoronewentto = counter;

                                            // tolerans aralığındaki gecikmeyi hesapla.
                                            reasonabledelay = delaytime + (pft[fpi[counter]] / 16);

                                            // tolerans aralığı dışındaki gecikmelerin toplamına ekle.
                                            totalreasonabledelay += reasonabledelay;

                                            // tolerans aralığı dışındaki gecikme olmadığı için toplama eklenmez.
                                            unreasonabledelay = 0;

                                            // tüm gecikmelerin toplamına ekle.
                                            totaldelay += reasonabledelay;

                                            // yola çıkmadaki gecikmeyi dönüşe de yansıt.
                                            oct[counter] += reasonabledelay;


                                            secoder = Convert.ToInt64(olt[counter] + reasonabledelay);
                                            timer = TimeSpan.FromSeconds(secoder).Add(TimeSpan.FromMinutes(-7));
                                            string merci = timer.ToString(@"hh\:mm");
                                            richTextBox3.AppendText(merci + "   \t\tPALLET INDEX : " + fpi[counter] + Environment.NewLine);
                                            // atamayı forma gecikme zamanıyla birlikte yazdır.
                                            richTextBox1.AppendText((counter + 1) + "\t" + (olt[counter]+reasonabledelay) + "\t\tOPERATÖR\tPALLET INDEX : " + fpi[counter] + "\t Kabul Edilebilir Gecikme: " + reasonabledelay + Environment.NewLine);
                                        }
                                       

                                     

                                    }
                                    // eğer agv'nin işinin bitiş süresi operatörünkinden erkense;
                                    else if (oct[operatoronewentto] > oct[agvwentto])
                                    {
                                       
                                        // taşıma için gecikme zamanını hesapla.
                                        delaytime = oct[agvwentto] - (olt[counter] + pft[fpi[counter]] / 16);
                                        
                                        
                                        // atamayı forma gecikme zamanıyla birlikte yazdır.
                                        if (delaytime > 0 )
                                        {

                                            // ikinci operatörün durumunu meşgule çevir.
                                            agvbusyoridle = true;

                                            // ikinci operatörün hangi işlemle meşgul olduğunu hafızada tut.
                                            agvwentto = counter;
                                            
                                            // tolerans aralığındaki gecikmeyi hesapla.
                                            reasonabledelay = pft[fpi[counter]] / 16;
                                            // tolerans aralığındaki gecikmelerin toplamına ekle.
                                            totalreasonabledelay += reasonabledelay;

                                            // tolerans aralığı dışındaki gecikmeyi hesapla.
                                            unreasonabledelay = delaytime;
                                            // tolerans aralığı dışındaki gecikmelerin toplamına ekle.
                                            totalunreasonabledelay += unreasonabledelay;

                                            // gecikmeleri, gecikmeler toplamına ekle.
                                            totaldelay += reasonabledelay + unreasonabledelay;

                                            // yola çıkmadaki gecikmeyi dönüşe de yansıt.
                                            oct[counter] += (reasonabledelay + unreasonabledelay);


                                            richTextBox1.SelectionColor = Color.White;
                                            richTextBox1.SelectionBackColor = Color.Red;

                                           
                                            richTextBox1.AppendText((counter + 1) + "\t" + (olt[counter]+reasonabledelay+unreasonabledelay) + "\t\tAGV\t\t  PALLET INDEX : " + fpi[counter] + "\t İstenmeyen gecikme" + unreasonabledelay + Environment.NewLine);
                                            // gecikme yazdırma
                                            richTextBox4.AppendText(fpi[counter] + " . Hat Alanı Gecikme " + unreasonabledelay+ " saniye" + Environment.NewLine);

                                            secoder = Convert.ToInt64(olt[counter] + reasonabledelay+unreasonabledelay);
                                            timer = TimeSpan.FromSeconds(secoder).Add(TimeSpan.FromMinutes(-7));
                                            string saat = timer.ToString(@"hh\:mm");
                                            richTextBox2.AppendText(saat + "   \t\tPALLET INDEX : " + fpi[counter] + Environment.NewLine);
                                        }
                                        else
                                        {
                                            // agv nin durumunu meşgule çevir.
                                            agvbusyoridle = true;

                                            // agv nin hangi işle meşgul olduğunu hafızada tut.
                                            agvwentto = counter;

                                            // tolerans aralığındaki gecikmeyi hesapla.
                                            reasonabledelay = delaytime + (pft[fpi[counter]] / 16);
                                            // tolerans aralığı dışındaki gecikmelerin toplamına ekle.
                                            totalreasonabledelay += reasonabledelay;

                                            // tolerans aralığı dışındaki gecikme olmadığı için toplama eklenmez.
                                            unreasonabledelay = 0;

                                            // tüm gecikmelerin toplamına ekle.
                                            totaldelay += reasonabledelay;

                                            // yola çıkmadaki gecikmeyi dönüşe de yansıt.
                                            oct[counter] += reasonabledelay;
                                             richTextBox1.AppendText((counter + 1) + "\t" + (olt[counter]+reasonabledelay) + "\t\tAGV\t\t  PALLET INDEX : " + fpi[counter] + "\t Kabul Edilebilir gecikme: " + reasonabledelay + Environment.NewLine);

                                            secoder = Convert.ToInt64(olt[counter] + reasonabledelay);
                                            timer = TimeSpan.FromSeconds(secoder).Add(TimeSpan.FromMinutes(-7));
                                            string saat = timer.ToString(@"hh\:mm");
                                            richTextBox2.AppendText(saat + "   \t\tPALLET INDEX : " + fpi[counter] + Environment.NewLine);

                                        }
                                       
                                       

                                    }

                                }
                                
                                senaryo += olt[counter];
                                counter += 1;
                            }
                            
                        }
                    }
                    catch (Exception)
                    {
                      
                        break;
                    }

                }

                // FORMA GÜN SONUNDAKİ TOPLAM GECİKMELERİ YAZDIRMA:

                
                // gecikmeler toplamı:
                richTextBox4.AppendText("\n\nGünlük;\nGecikme : " + totaldelay);

                // tolere edilebilen gecikmeler toplamı:
                richTextBox4.AppendText("\nTolere Edilen: " + totalreasonabledelay);

                // tolere edilemeyen gecikmeler toplamı:
                richTextBox4.SelectionColor = Color.White;
                richTextBox4.SelectionBackColor = Color.Red;
                richTextBox4.AppendText("\nTolere Edilemeyen: " + totalunreasonabledelay + "\n\n");
                
                haftalikGecikme += totalunreasonabledelay;
            }
            textBox4.Text = Convert.ToString(haftalikGecikme + " saniye");
            
        }
        
        private void FormA_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox2.Clear();
            richTextBox3.Clear();
            richTextBox4.Clear();
            kontrol = 1;
            /*
            pawon[8] = true;
            pawon[9] = true;
            pawon[11] = true;
            pawon[12] = true;
            pawon[13] = true;
            pawon[14] = true;
            pawon[17] = true;
            pawon[23] = true;
            pawon[24] = true;
            pawon[38] = true;
            pawon[41] = true;
            pawon[46] = true;
            pawon[48] = true;
          */
            int source=0;

            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source= veri_baglanti.accdb"); con.Open(); //accdb dosyaları için

            for (int k = 1; k < 108; k++)
            {
                try
                {


                
                if (controlTF(k) == true)
                {
                    // uzaklık dizini doldurma
                    
                    pawon[k] = true;
                    dpa[k] = distance(k);

                    // ws doğrulama
                    
                    for (int i = 0; i < 7; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {

                            ws[k, i, j] = hatcek(k, j);
                            richTextBox2.AppendText(ws[k,i,j] + "   " + i + " . gün " + Environment.NewLine);
                        }
                    }

                    pft[k] = veri_cek(k) * 60;

                    richTextBox1.AppendText(k + ".ci" + "hat\t" + pft[k] + "  " + pawon[k] + Environment.NewLine);

                        // hat süreleri toplamı alma
                        source += pft[k];
                }
                else
                {
                    pawon[k] = false;
                }
                
                }
                catch (Exception)
                {
                    break;
                }
            }

            
           
            int veri_cek(int a)
            {
                int b;
                

                OleDbCommand cmd = new OleDbCommand("Select sure from Tablo2 where index=" + a, con);
                OleDbDataReader dr = cmd.ExecuteReader();

                dr.Read();

                b = Convert.ToInt32(dr["sure"]);


                
                return b;
            }

            // veri tabanından hat true mu false mu kontrol eder

            bool controlTF(int a)
            {
                
                bool b;
                

                OleDbCommand cmd = new OleDbCommand("Select kayit from Tablo2 where index=" + a, con);
                OleDbDataReader dr = cmd.ExecuteReader();

                dr.Read();
                b = Convert.ToBoolean(dr["kayit"]);

               
                return b;

            }

            double distance(int a)
            {
                
                double b;
               
                OleDbCommand cmd = new OleDbCommand("Select uzaklik from Tablo2 where index=" + a, con);
                OleDbDataReader dr = cmd.ExecuteReader();

                dr.Read();

                b = Convert.ToDouble(dr["uzaklik"]);


                
                return b;


            }

            bool hatcek(int a, int u)
            {
                bool b;
              
                
                OleDbCommand cmd = new OleDbCommand("Select * from tablo3 where Kimlik=" + a, con);
                OleDbDataReader dr = cmd.ExecuteReader();

                dr.Read();
              
                if (u==0)
                {
                    b = Convert.ToBoolean(dr["var1"]);
                }
               else if (u==1)
                {
                    b = Convert.ToBoolean(dr["var2"]);
                }
                else if (u==2)
                {
                    b = Convert.ToBoolean(dr["var3"]);
                }
                else if (u==3)
                {
                    b = Convert.ToBoolean(dr["var4"]);
                }
                else
                {
                    b = false;
                }
               
                
                return b;

            }
            con.Close();

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }
    }
}
