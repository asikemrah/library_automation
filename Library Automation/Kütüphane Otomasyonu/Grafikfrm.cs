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

namespace Kütüphane_Otomasyonu_18350092
{
    public partial class Grafikfrm : Form
    {
        public Grafikfrm()
        {
            InitializeComponent();
        }
       
        SqlConnection baglan = new SqlConnection("Server=.;Database=KütüphaneOtomasyonu;Trusted_Connection=True;");
        DataSet daset = new DataSet();
        private void Grafikfrm_Load(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("select adsoyad, okunankitasayısı from uye", baglan);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                chart1.Series["Okunan Kitap Sayısı"].Points.AddXY(read["adsoyad"].ToString(), read["okunankitapsayisi"]);
            }
            baglan.Close();
            chart1.Series["Okunan Kitap Sayısı"].Color = Color.Orange;
        }
    }
}
