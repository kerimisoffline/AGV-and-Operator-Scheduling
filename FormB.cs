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
    public partial class FormB : Form
    {
       
        public FormB()
        {
            InitializeComponent();
        }
        // diğer forma yaşıyacak public türünde değişkenler hat ve süre sıralı
        

        public static int[] shareAtama = new int[100];
        public static int[] shareIndex = new int[100];
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
        int[] pft_AGV = new int[108];
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            byte area;
            area = Convert.ToByte(textBox1.Text);
            pawon[area] = true;

            int time;
            time = Convert.ToInt32(Convert.ToDouble(textBox2.Text) * 60);
            pft_AGV[area] = time;

            double distance;
            distance = Convert.ToDouble(textBox3.Text);
            dpa[area] = distance;

            at[area] = Convert.ToInt32(dpa[area] / 0.15);
            //     0.15 : coefficient of how many metre can be carried by AGV an empty palet in one second 
            tt[area] = Convert.ToInt32(at[area] + tfpe + (Convert.ToInt32(dpa[area] / 0.10)) + ptw);
            //     0.10 : coefficient of how many metre can be carried by AGV an full palet in one second 




            /* SHIFTS INFORMATION WILL BE TAKEN FROM CHECKBOXES */

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
            textBox1.Select();
        }

        
        

        /* SCHEDULING ALGORITHM:
         *      B- AGV SCHEDULING */
        public   void button2_Click_1(object sender, EventArgs e)
        {

            

            /* SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK SİLİNECEK */




            /* DATABASE */

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    ws[8, i, j] = true;
                    ws[9, i, j] = true;
                    ws[11, i, j] = true;
                    ws[12, i, j] = true;
                    ws[13, i, j] = true;
                    ws[14, i, j] = true;
                    ws[17, i, j] = true;
                    ws[23, i, j] = true;
                    ws[24, i, j] = true;
                    
                }
            }

            // TOPLAM AGV YOLU 59 metre  atro = 59, TOPLAM DOLU YOLCULUK
            // area 8
            
            pawon[8] = true;
            pft_AGV[8] = Convert.ToInt32(144.1 * 60);
            dpa[8] = 2;
            at[8] = Convert.ToInt32(dpa[8] / 0.15);
            tt[8] = Convert.ToInt32(at[8] + tfpe + (atro - dpa[8]) * 10 + ptw);
           
            // area 9
            pawon[9] = true;
            pft_AGV[9] = Convert.ToInt32(150 * 60);
            dpa[9] = 3.5;
            at[9] = Convert.ToInt32(dpa[9] / 0.15);
            tt[9] = Convert.ToInt32(at[9] + tfpe + (atro - dpa[9]) * 10 + ptw);
            //area 11
            pawon[11] = true;
            pft_AGV[11] = Convert.ToInt32(346 * 60);
            dpa[11] = 5.5;
            at[11] = Convert.ToInt32(dpa[11] / 0.15);
            tt[11] = Convert.ToInt32(at[11] + tfpe + (atro - dpa[11]) * 10 + ptw);
            //area 12
            pawon[12] = true;
            pft_AGV[12] = Convert.ToInt32(120 * 60);
            dpa[12] = 6.5;
            at[12] = Convert.ToInt32(dpa[12] / 0.15);
            tt[12] = Convert.ToInt32(at[12] + tfpe + (atro - dpa[12]) * 10 + ptw);
            
            // area 13
            pawon[13] = true;
            pft_AGV[13] = Convert.ToInt32(130 * 60);
            dpa[13] = 7;
            at[13] = Convert.ToInt32(dpa[13] / 0.15);
            tt[13] = Convert.ToInt32(at[13] + tfpe + (atro - dpa[13]) * 10 + ptw);
            // area 14
            pawon[14] = true;
            pft_AGV[14] = Convert.ToInt32(500 * 60);
            dpa[14] = 7.5;
            at[14] = Convert.ToInt32(dpa[14] / 0.15);
            tt[14] = Convert.ToInt32(at[14] + tfpe + (atro - dpa[14]) * 10 + ptw);
            // area 17
            pawon[17] = true;
            pft_AGV[17] = Convert.ToInt32(280 * 60);
            dpa[17] = 12.7;
            at[17] = Convert.ToInt32(dpa[17] / 0.15);
            tt[17] = Convert.ToInt32(at[17] + tfpe + (atro - dpa[17]) * 10 + ptw);
            // area 23
            pawon[23] = true;
            pft_AGV[23] = Convert.ToInt32(733.3 * 60);
            dpa[23] = 24;
            at[23] = Convert.ToInt32(dpa[23] / 0.15);
            tt[23] = Convert.ToInt32(at[23] + tfpe + (atro - dpa[23]) * 10 + ptw);
            // area 24
            pawon[24] = true;
            pft_AGV[24] = Convert.ToInt32(503.3 * 60);
            dpa[24] = 24.5;
            at[24] = Convert.ToInt32(dpa[24] / 0.15);
            tt[24] = Convert.ToInt32(at[24] + tfpe + (atro - dpa[24]) * 10 + ptw);
            


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




            /* MAIN SCHEDULING */

            for (byte dow = 0; dow < 7; dow++)
            // dow : days of a week
            {
                for (byte pi = 0; pi < 28; pi++)
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
                                    ets = 26100;
                                }
                                else if (sod == 1)
                                {
                                    pftbs = 26100;
                                    ets = 52200;
                                }
                                else if (sod == 2)
                                {
                                    pftbs = 52200;
                                    ets = 78300;
                                }
                                else if (sod == 3)
                                {
                                    pftbs = 26100;
                                    ets = 67500;
                                }
                                pftbs -= rt[pi];
                                while (pftbs + pft_AGV[pi] <= ets)
                                {
                                    pftbs += pft_AGV[pi];
                                    Array.Resize(ref fpi, aic + 1);
                                    fpi[aic] = pi;
                     // shareINDEX DİĞER FORMA SIRALAMA GÖNDERMEK İÇİN
                                    Array.Resize(ref shareIndex, aic + 1);
                                    shareIndex[aic] = fpi[aic];
                                    Array.Resize(ref fps, aic + 1);
                                    fps[aic] = pftbs;
                                    Array.Resize(ref olt, aic + 1);
                                    olt[aic] = pftbs - at[fpi[aic]];
                                    Array.Resize(ref shareAtama, aic + 1);
                    //  shareATAMA DİĞER FORMA GÖNDERMEK İÇİN                
                                    shareAtama[aic] = olt[aic];
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
                    richTextBox1.AppendText("----------\tPAZARTESİ\t----------\n");
                }
                else if (dow == 1)
                {
                    richTextBox1.AppendText("\n\n----------\tSALI\t----------\n");
                }
                else if (dow == 2)
                {
                    richTextBox1.AppendText("\n\n----------\tÇARŞAMBA\t----------\n");
                }
                else if (dow == 3)
                {
                    richTextBox1.AppendText("\n\n----------\tPERŞEMBE\t----------\n");
                }
                else if (dow == 4)
                {
                    richTextBox1.AppendText("\n\n----------\tCUMA\t----------\n");
                }
                else if (dow == 5)
                {
                    richTextBox1.AppendText("\n\n----------\tCUMARTESİ\t----------\n");
                }
                else if (dow == 6)
                {
                    richTextBox1.AppendText("\n\n----------\tPAZAR\t----------\n");
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

                            ram = shareIndex[j];
                            shareIndex[j] = shareIndex[j + 1];
                            shareIndex[j + 1] = ram;

                            ram = olt[j];
                            olt[j] = olt[j + 1];
                            olt[j + 1] = ram;

                            ram = shareAtama[j];
                            shareAtama[j] = shareAtama[j + 1];
                            shareAtama[j + 1] = ram;

                            ram = fps[j];
                            fps[j] = fps[j + 1];
                            fps[j + 1] = ram;

                            ram = oct[j];
                            oct[j] = oct[j + 1];
                            oct[j + 1] = ram;
                        }
                    }
                }




                /* OPERATOR ASSINGMENT */

                bool operatoronebusyoridle = false;
                int operatoronewentto = 0;

                bool operatortwobusyoridle = false;
                int operatortwowentto = 0;

                int counter = 0;
                

                for (int i = 0; i < 80000; i++)
                /* (i = 0; i < olt[olt.GetUpperBound(0)]; i++) 
                 * şeklinde olması gerekirdi ama bilmediğim bir sebepten 
                 * yarıda son değeri almıyor. olt sorting kodunun 
                 * altında işe yarayan yöntem burada işe yaramıyor */
                {
                    // birinci operatör meşgulse ve birinci operatörün dönüş süresi geldiyse; 
                    if (operatoronebusyoridle == true && oct[operatoronewentto] < i)
                    {
                        // birinci operatörün durumunu atıl olarak değiştir.
                        operatoronebusyoridle = false;
                    }
                    // ikinci operatör meşgulse ve birinci operatörün dönüş süresi geldiyse;
                    if (operatortwobusyoridle == true && oct[operatortwowentto] < i)
                    {
                        // ikinci operatörün durumunu atıl olarak değiştir.
                        operatortwobusyoridle = false;
                    }

                    try
                    {

                        // eğer operatörün i saniyeside yola çıkması gerekiyorsa,
                        if (olt[counter] == i)
                        {

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

                            // eğer AGV  atıl durumdaysa;
                            if (operatoronebusyoridle == false)
                            {
                                // AGV ata ve durumunu meşgule çevir.
                                operatoronebusyoridle = true;

                                // birinci operatörün kaçıncı işlemi yapıyor olduğunu aklında tut.
                                operatoronewentto = counter;

                                // atanmayı form a yazdır.
                                richTextBox1.AppendText((counter + 1) + "\t" + olt[counter] + "\t\tAGV    \t\tPALLET INDEX : " + fpi[counter] + Environment.NewLine);
                            }
                            // AGV meşgul, operatör atılsa;
                            else if (operatortwobusyoridle == false)
                            {
                                // ikinci operatörü ata ve durumunu meşgule çevir.
                                operatortwobusyoridle = true;

                                // ikinci operatörün kaçıncı işlemi yapıyor olduğunu aklında tut.
                                operatortwowentto = counter;

                                // atamayı form a yazdır.
                                richTextBox1.AppendText((counter + 1) + "\t" + olt[counter] + "\t\tOPERATÖR\tPALLET INDEX : " + fpi[counter] + Environment.NewLine);
                            }
                            // hem AGV hem de operatör meşgulse en yakın uygunu ata;
                            else if (operatoronebusyoridle == true && operatortwobusyoridle == true)
                            {
                                int delaytime;

                                // eğer birinci operatörün işinin bitiş süresi ikinci operatörün işinin bitiş süresinden daha  erkense ya da işleri aynı saniyede bitiyorsa;
                                if (oct[operatoronewentto] <= oct[operatortwowentto])
                                {
                                    // taşıma için gecikme zamanını hesapla.
                                    delaytime = oct[operatoronewentto] - olt[counter];

                                    // birinci operatörün durumunu meşgule çevir.
                                    operatoronebusyoridle = true;

                                    // birinci operatörün hangi işlemle meşgul olduğunu hafızada tut.
                                    operatoronewentto = counter;

                                    // atamayı forma gecikme zamanıyla birlikte yazdır.
                                    richTextBox1.AppendText((counter + 1) + "\t" + (olt[counter] + delaytime) + "\t\tAGV\t\tPALLET INDEX : " + fpi[counter] + "  ---------------- GECİKME : " + delaytime + Environment.NewLine);
                                }
                                // eğer ikinci operatörün işinin bitiş süresi birinci operatörünkinden erkense;
                                else if (oct[operatoronewentto] > oct[operatortwowentto])
                                {
                                    // taşıma için gecikme zamanını hesapla.
                                    delaytime = oct[operatortwowentto-1] - olt[counter];

                                    // ikinci operatörün durumunu meşgule çevir.
                                    operatortwobusyoridle = true;

                                    // ikinci operatörün hangi işlemle meşgul olduğunu hafızada tut.
                                    operatortwowentto = counter;

                                    // atamayı forma gecikme zamanıyla birlikte yazdır.
                                    richTextBox1.AppendText((counter + 1) + "\t" + (olt[counter] + delaytime) + "\t\tOPERATÖR\tPALLET INDEX : " + fpi[counter] + "  ---------------- GECİKME : " + delaytime + Environment.NewLine);
                                }
                            }
                            // yapılacak işlem indeksini bir arttır.
                            counter += 1;
                        }

                    }
                    catch (Exception)
                    {
                        DialogResult dialog = new DialogResult();
                        dialog = MessageBox.Show("Programdan çıkılsın mı?", "ÇIKIŞ", MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.Yes)
                        {
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Çıkış yapılmadı");
                        }
                        break;
                    }
                }
            }
        }

        private void FormB_Load(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            

        }
        
    }
}
