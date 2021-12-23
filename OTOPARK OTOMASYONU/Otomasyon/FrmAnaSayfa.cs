using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otomasyon
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            AraçOtoparkKaydı kayit = new AraçOtoparkKaydı();
            kayit.ShowDialog();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            AraçOtoparkYeri yer = new AraçOtoparkYeri();
            yer.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AraçOtoparkÇıkış çıkış = new AraçOtoparkÇıkış();
            çıkış.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Satis satis = new Satis();
            satis.ShowDialog();
        }

        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {

        }
    }
}
