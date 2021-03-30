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
    public partial class EmanetKitapVerfrm : Form
    {
        public EmanetKitapVerfrm()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Server=.;Database=KütüphaneOtomasyonu;Trusted_Connection=True;");
        DataSet daset = new DataSet();
        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Kayıt Silinsin mi?","Uyarı",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if (dialog==DialogResult.Yes)
            {
                baglan.Open();
                SqlCommand komut = new SqlCommand("delete from spet where barkodno='" + dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString() + "'", baglan);
                komut.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Silme İşlemi Yapıldı", "Silme İşlemi");
                daset.Tables["sepet"].Clear();
                sepetlistele();
                lblKitapSayi.Text = "";
                kitapsayisi();
            }

        }
        private void sepetlistele()
        {
            baglan.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select* from sepet", baglan);
            adtr.Fill(daset,"sepet");
            dataGridView1.DataSource = daset.Tables["sepet"];
            baglan.Close();
        
        }

        private void kitapsayisi()
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("select sum(kitapsayisi) from sepet", baglan);
            lblKitapSayi.Text = komut.ExecuteScalar().ToString();
            baglan.Close();
            
        }
        private void EmanetKitapVerfrm_Load(object sender, EventArgs e)
        {
            sepetlistele();
            kitapsayisi();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("insert into sepet(barkodno,kitapadi,yazari,yayinevi,sayfasayisi,kitapsayisi,teslimtarihi,iadetarihi) values(@barkodno,@kitapadi,@yazari,@yayinevi,@sayfasayisi,@kitapsayisi,@teslimtarihi,@iadetarihi)", baglan);
            komut.Parameters.AddWithValue("@barkodno", txtBarkoNo.Text);
            komut.Parameters.AddWithValue("@kitapadi", txtKitapAdi.Text);
            komut.Parameters.AddWithValue("@yazari", txtYazari.Text);
            komut.Parameters.AddWithValue("@yayinevi", txtYayinevi.Text);
            komut.Parameters.AddWithValue("@sayfasayisi", txtSayfaSayisi.Text);
            komut.Parameters.AddWithValue("@kitapsayisi", int.Parse(txtKitapSayisi.Text));
            komut.Parameters.AddWithValue("@teslimtarihi", dateTimePicker1.Text);
            komut.Parameters.AddWithValue("@iadetarihi", dateTimePicker2.Text);
            komut.ExecuteNonQuery();
            baglan.Close();
            MessageBox.Show("Kitap Sepete Eklendi ","Ekleme İşlemi");
            daset.Tables["sepet"].Clear();
            sepetlistele();
            lblKitapSayi.Text = "";
            kitapsayisi();
            foreach (Control item in grpKitapBilgi.Controls)
            {
                if (item is TextBox)
                {
                    if (item!=txtKitapSayisi)
                    {
                        item.Text = "";
                    }

                }
            }
        }

        private void txtTc_TextChanged(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("select * from uye where tc like'"+txtTc.Text+"!",baglan);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtAdSoyad.Text = read["adsoyad"].ToString();
                txtYas.Text = read["yas"].ToString();
                txtTelefon.Text = read["telefon"].ToString();

            }
            baglan.Close();
            
            baglan.Open();
            SqlCommand komut2 = new SqlCommand("select sum(kitapsayisi) from emanetkitap where tc='"+txtTc.Text+"' ", baglan);
            lblKAyitliKitapSayisi.Text = komut2.ExecuteScalar().ToString();
            baglan.Close();
            if (txtTc.Text=="")
            {
                foreach (Control item in grpUyeBilgi.Controls)
                {
                    if (item is TextBox)
                    {
                     item.Text = "";
                     
                    } 
                }
                lblKAyitliKitapSayisi.Text = "";
            }

        }

        private void txtBarkoNo_TextChanged(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("select * from kitap where barkodno like'"+txtBarkoNo.Text+"'",baglan);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtKitapAdi.Text = read["kitapadi"].ToString();
                txtYazari.Text = read["yazari"].ToString();
                txtYayinevi.Text = read["yayinevi"].ToString();
                txtSayfaSayisi.Text = read["sayfasayisi"].ToString();
            }
            baglan.Close();
            if (txtBarkoNo.Text=="")
            {
                foreach (Control item in grpKitapBilgi.Controls)
                {
                    if (item is TextBox)
                    {
                        if (item != txtKitapSayisi)
                        {
                            item.Text = "";
                        }

                    }
                }
            }
           
        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTeslimEt_Click(object sender, EventArgs e)
        {
            if (lblKitapSayi.Text!="")
            {
                if (lblKAyitliKitapSayisi.Text=="" && int.Parse(lblKAyitliKitapSayisi.Text)<=3 || lblKAyitliKitapSayisi.Text!="" && int.Parse(lblKAyitliKitapSayisi.Text)+int.Parse(lblKAyitliKitapSayisi.Text)<=3)
                {
                    if (txtTc.Text!="" && txtAdSoyad.Text!="" && txtYas.Text!="" && txtTelefon.Text!="")
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                        {
                            baglan.Open();
                            SqlCommand komut = new SqlCommand("insert into emanetkitap(tc,adsoyad,yas,telefon,barkodno,kitapadi,yazari,yayinevi,sayfasayisi,kitapsayisi,teslimtarihi,iadeatarihi) values(@tc,@adsoyad,@yas,@telefon,@barkodno,@kitapadi,@yazari,@yayinevi,@sayfasayisi,@kitapsayisi,@teslimtarihi,@iadeatarihi)", baglan);
                            komut.Parameters.AddWithValue("@tc", txtTc.Text);
                            komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
                            komut.Parameters.AddWithValue("@yas", txtYas.Text);
                            komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                            komut.Parameters.AddWithValue("@barkodno", dataGridView1.Rows[i].Cells["barkodno"].Value.ToString());
                            komut.Parameters.AddWithValue("@kitapadi", dataGridView1.Rows[i].Cells["kitapadi"].Value.ToString());
                            komut.Parameters.AddWithValue("@yazari", dataGridView1.Rows[i].Cells["yazari"].Value.ToString());
                            komut.Parameters.AddWithValue("@yayinevi", dataGridView1.Rows[i].Cells["yayinevi"].Value.ToString());
                            komut.Parameters.AddWithValue("@sayfasayisi", dataGridView1.Rows[i].Cells["sayfasayisi"].Value.ToString());
                            komut.Parameters.AddWithValue("@kitapsayisi", int.Parse(dataGridView1.Rows[i].Cells["kitapsayisi"].Value.ToString()));
                            komut.Parameters.AddWithValue("@teslimtarihi", dataGridView1.Rows[i].Cells["teslimtarihi"].Value.ToString());
                            komut.Parameters.AddWithValue("@iadetarihi", dataGridView1.Rows[i].Cells["iadetarihi"].Value.ToString());
                            komut.ExecuteNonQuery();
                            SqlCommand komut2 = new SqlCommand("updata uye set okunankitapsayisi=okunankitapsayisi+'"+int.Parse(dataGridView1.Rows[i].Cells["kitapsayisi"].Value.ToString())+"'where tc='"+txtTc.Text+"' ",baglan);
                            komut2.ExecuteNonQuery();
                            SqlCommand komut3 = new SqlCommand("updata uye kitap  stoksayisi=stoksayisi-'" + int.Parse(dataGridView1.Rows[i].Cells["kitapsayisi"].Value.ToString()) + "'where barkodno='" + dataGridView1.Rows[i].Cells["barkodno"].Value.ToString() + "' ", baglan);
                            komut3.ExecuteNonQuery();
                            baglan.Close();
                        }

                        baglan.Open();
                        SqlCommand komut4 = new SqlCommand("delete from sepet", baglan);
                        komut4.ExecuteNonQuery();
                        baglan.Close();
                        MessageBox.Show("Kitap Emmanet Edildi");
                        daset.Tables["sepet"].Clear();
                        sepetlistele();
                        txtTc.Text = "";
                        lblKitapSayi.Text = "";
                        kitapsayisi();
                        lblKAyitliKitapSayisi.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Önce Üye İsmi Seçmeniz Gerekir","Uyarı");
                    }

                }
                else
                {
                    MessageBox.Show("Emanet Kitap Sayısı 3'den Fazla Olamaz", "Uyarı");
                } 

            }
            else
            {
                MessageBox.Show("Öncelikle Sepete Kitap Ekleyin","Uyarı");  
            }
            

        }

        private void grpKitapBilgi_Enter(object sender, EventArgs e)
        {

        }
    }
}
