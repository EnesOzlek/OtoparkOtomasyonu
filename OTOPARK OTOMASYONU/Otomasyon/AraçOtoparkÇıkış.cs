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
    public partial class AraçOtoparkÇıkış : Form
    {
        public AraçOtoparkÇıkış()              
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-71140O3\\SQLEXPRESS;Initial Catalog=araç_otoparkk;Integrated Security=True");

        private void AraçOtoparkÇıkış_Load(object sender, EventArgs e)
        {
            DoluYerler();
            Plakalar();
            timer1.Enabled = true;
        }

        private void Plakalar()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from araç_otopark_kaydı", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboPlaka.Items.Add(read["plaka"].ToString());
            }
            baglanti.Close();
        }

        private void DoluYerler()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from araçdurumu where durumu='Dolu'", baglanti);
            SqlDataReader read = komut.ExecuteReader(); 
            while (read.Read())
            {
                comboParkYeri.Items.Add(read["parkyeri"].ToString());
            }
            baglanti.Close();
        }

        private void comboPlaka_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from araç_otopark_kaydı where plaka= '"+comboPlaka.SelectedItem+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtParkYeri.Text=read["parkyeri"].ToString();
            }
            baglanti.Close();
        }

        private void comboParkYeri_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from araç_otopark_kaydı where parkyeri= '" + comboParkYeri.SelectedItem + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtParkYeri2.Text = read["plaka"].ToString();
                txtTc.Text = read["tc"].ToString();
                txtAd.Text = read["ad"].ToString();
                txtSoyad.Text = read["soyad"].ToString();
                txtMarka.Text = read["marka"].ToString();
                txtSeri.Text = read["seri"].ToString();
                txtPlaka.Text = read["plaka"].ToString();
                lblGirişTarihi.Text = read["tarih"].ToString();

            }
            baglanti.Close();
            DateTime giriş, çıkış;
            giriş = DateTime.Parse(lblGirişTarihi.Text);
            çıkış = DateTime.Parse(lblÇıkışTarihi.Text);
            TimeSpan fark;
            fark = çıkış - giriş;
            lblSüre.Text = fark.TotalHours.ToString("0.00");
            lblToplamTutar.Text = (double.Parse(lblSüre.Text) * (0.75)).ToString("0.00");






        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblÇıkışTarihi.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete  from araç_otopark_kaydı  where plaka= '"+txtPlaka.Text+"'", baglanti);
            komut.ExecuteNonQuery();
            SqlCommand komut2 = new SqlCommand("update araçdurumu set durumu='BOŞ' where parkyeri= '" + txtParkYeri2.Text + "'", baglanti);
            komut2.ExecuteNonQuery();
            SqlCommand komut3 = new SqlCommand("insert into satis(parkyeri,plaka,giriş_tarihi,çıkış_tarihi,süre,tutar)values(@parkyeri,@plaka,@giriş_tarihi,@çıkış_tarihi,@süre,@tutar)", baglanti);

            komut3.Parameters.AddWithValue("@parkyeri", txtParkYeri2.Text);
            komut3.Parameters.AddWithValue("@plaka", txtPlaka.Text);
            komut3.Parameters.AddWithValue("@giriş_tarihi", lblGirişTarihi.Text);
            komut3.Parameters.AddWithValue("@çıkış_tarihi", lblÇıkışTarihi.Text);
            komut3.Parameters.AddWithValue("@süre",double.Parse(lblSüre.Text));
            komut3.Parameters.AddWithValue("@tutar",double.Parse(lblToplamTutar.Text));

            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Araç çıkış yapıldı");
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {

                    item.Text = "";
                    txtParkYeri.Text = "";
                    comboParkYeri.Text = "";
                    comboPlaka.Text = "";


                }
            }
            comboPlaka.Items.Clear();
            comboParkYeri.Items.Clear();
            DoluYerler();
            Plakalar();

        }
    }
}
