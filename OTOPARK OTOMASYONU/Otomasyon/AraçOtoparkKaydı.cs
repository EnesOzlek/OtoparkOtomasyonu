using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otomasyon
{
    public partial class AraçOtoparkKaydı : Form
    {
        public AraçOtoparkKaydı()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-71140O3\\SQLEXPRESS;Initial Catalog=araç_otoparkk;Integrated Security=True");
        private void AraçOtoparkKaydı_Load(object sender, EventArgs e)
        {
            BoşAraçlar();
            Marka();
        }

        private void Marka()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka from markabilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboMARKA.Items.Add(read["marka"].ToString());
            }
            baglanti.Close();
        }

        private void BoşAraçlar()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from araçdurumu WHERE durumu='BOŞ'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboPARK.Items.Add(read["parkyeri"].ToString());
            }
            baglanti.Close();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into araç_otopark_kaydı(tc,ad,soyad,telefon,email,plaka,marka,seri,renk,parkyeri,tarih) values (@tc,@ad,@soyad,@telefon,@email,@plaka,@marka,@seri,@renk,@parkyeri,@tarih)", baglanti);
            komut.Parameters.AddWithValue("@tc", txtTC.Text);
            komut.Parameters.AddWithValue("@ad", txtAD.Text);
            komut.Parameters.AddWithValue("@soyad", txtSOYAD.Text);
            komut.Parameters.AddWithValue("@telefon", txtTELEFON.Text);
            komut.Parameters.AddWithValue("@email", txtEMAİL.Text);
            komut.Parameters.AddWithValue("@plaka", txtPLAKA.Text);
            komut.Parameters.AddWithValue("@marka", comboMARKA.Text);
            komut.Parameters.AddWithValue("@seri", comboSERİ.Text);
            komut.Parameters.AddWithValue("@renk", txtRENK.Text);
            komut.Parameters.AddWithValue("@parkyeri", comboPARK.Text);
            komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
            
            komut.ExecuteNonQuery();
            
            SqlCommand komut2 = new SqlCommand("update araçdurumu set durumu ='DOLU' where parkyeri='"+comboPARK.SelectedItem+"'", baglanti);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            
            MessageBox.Show("ARAÇ KAYDI OLIŞTURULDU", "Kayıt");
            comboPARK.Items.Clear();
            BoşAraçlar();

            comboMARKA.Items.Clear();
            Marka();
            comboSERİ.Items.Clear();

            foreach (Control item in grupKişi.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            foreach (Control item in grupAraç.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            foreach (Control item in grupAraç.Controls)
            {
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }

        private void btnMarka_Click(object sender, EventArgs e)
        {
            Marka marka = new Marka();
            marka.ShowDialog();
        }

        private void btnSeri_Click(object sender, EventArgs e)
        {
            Seri seri = new Seri();
            seri.ShowDialog();
        }

        private void grupAraç_Enter(object sender, EventArgs e)
        {

        }

        private void comboMARKA_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboSERİ.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka,seri from seribilgileri where marka='"+comboMARKA.SelectedItem+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboSERİ.Items.Add(read["seri"].ToString());
            }
            baglanti.Close();

        }
    }
}
