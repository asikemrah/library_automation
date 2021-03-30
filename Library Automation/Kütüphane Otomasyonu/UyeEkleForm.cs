using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Kütüphane_Otomasyonu_18350092
{
    public partial class UyeEkleForm : Form
    {
        public UyeEkleForm()
        {
            InitializeComponent();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUyeCıkıs_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        SqlConnection baglan = new SqlConnection("Server=.;Database=KütüphaneOtomasyonu;Trusted_Connection=True;");
        private void btnUyeEkle_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand com = new SqlCommand("İnsert İnto Uye,(tc,adsoyad,yas,cinsiyet,telefon,adres,eposta,okunankitapsayısı) values(@tc,@adsoyad,@yas,@cinsiyet,@telefon,@adres,@eposta,@okunankitapsayısı)", baglan);
            com.Parameters.AddWithValue("@tc",txtTc.Text);
            com.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
            com.Parameters.AddWithValue("@yas", txtYas.Text);
            com.Parameters.AddWithValue("@cinsiyet", cmbCnsyt.Text);
            com.Parameters.AddWithValue("@telefon", txtTel.Text);
            com.Parameters.AddWithValue("@adres", txtAdres.Text);
            com.Parameters.AddWithValue("@eposta", txteposta.Text);
            com.Parameters.AddWithValue("@okunankitapsayısı", txtokunankitap.Text);
            com.ExecuteNonQuery();
            baglan.Close();
            MessageBox.Show("İşlem Başarılı,Üye Kayıtı Yapıldı");
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    if (item!=txtokunankitap)
                    {
                        item.Text = "";
                    }
                    
                }
            }
        }
    }
}
