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
    public partial class Seri : Form
    {
        public Seri()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-71140O3\\SQLEXPRESS;Initial Catalog=araç_otoparkk;Integrated Security=True");
        private void marka()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka from markabilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["marka"].ToString());
            }
            baglanti.Close();
        }
        private void Seri_Load(object sender, EventArgs e)
        {
            marka();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into seribilgileri(marka,seri) values('"+comboBox1.Text+"','" + textBox1.Text + "')", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("MARKA'YA BAĞLI ARAÇ SERİSİ EKLENDİ");
            textBox1.Clear();
            comboBox1.Text = "";
            comboBox1.Items.Clear();
            marka();

        }
    }
}
