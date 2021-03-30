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
    public partial class KitapListeleForm : Form
    {
        public KitapListeleForm()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Server=.;Database=KütüphaneOtomasyonu;Trusted_Connection=True;");
        DataSet dt = new DataSet();
        private void kitaplistele()
        {
            baglan.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from kitap", baglan);
            sda.Fill(dt, "kitap");
            dataGridView1.DataSource = dt.Tables["kitap"];
            baglan.Close();
        }
            private void KitapListeleForm_Load(object sender, EventArgs e)
        {
            kitaplistele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu Kayıtı Silmek İstiyor musunuz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialog == DialogResult.Yes)
            {
                baglan.Open();
                SqlCommand com = new SqlCommand("delete from kitap where barkodno=@barkodno", baglan);
                com.Parameters.AddWithValue("@barkodno", dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString());
                com.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Silme İşlemi Başarılı");
                dt.Tables["kitap"].Clear();
                kitaplistele();
                foreach (Control item in Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand com = new SqlCommand("update kitap set kitapadi=@kitapadi,yazar=@yazar,yayinevi=@yayinevi,sayfasayisi=@sayfasayisi,turu=@turu,stoksayisi=@stoksayisi,rafno=@rafno,aciklama=@aciklama",baglan);
            com.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
            com.Parameters.AddWithValue("@kitapadi", txtKitapAdi.Text);
            com.Parameters.AddWithValue("@yazar", txtYazar.Text);
            com.Parameters.AddWithValue("@yayinevi", txtYayinevi.Text);
            com.Parameters.AddWithValue("@sayfasayisi", txtSayfaSayisi.Text);
            com.Parameters.AddWithValue("@tur", cmbTur.Text);
            com.Parameters.AddWithValue("@stoksayisi", txtStokSayisi.Text);
            com.Parameters.AddWithValue("@rafno", txtRafNo.Text);
            com.Parameters.AddWithValue("@aciklama", txtAciklama.Text);
            com.ExecuteNonQuery();
            baglan.Close();
            MessageBox.Show("İşlem Başarılı,Kitap Kayıtı Yapıldı");
            dt.Tables["kitap"].Clear();
            kitaplistele();
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void textBarkodAra_TextChanged(object sender, EventArgs e)
        {
            dt.Tables["kitap"].Clear();
            baglan.Open();
            SqlDataAdapter zd = new SqlDataAdapter("select *from uye where tc like '%" + textBarkodAra.Text + "% '", baglan);
            zd.Fill(dt, "kitap");
            dataGridView1.DataSource = dt.Tables["kitap"];
            baglan.Close();
        }

        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand com = new SqlCommand("Select * from kitap barkodno tc like '" + txtBarkodNo.Text + " '", baglan);
            SqlDataReader read = com.ExecuteReader();
            while (read.Read())
            {
                txtKitapAdi.Text = read["kitapadi"].ToString();
                txtYazar.Text = read["yazar"].ToString();
                txtYayinevi.Text = read["yayinevi"].ToString();
                txtSayfaSayisi.Text = read["sayfasayisi"].ToString();
                cmbTur.Text = read["turu"].ToString();
                txtStokSayisi.Text = read["stoksayisi"].ToString();
                txtRafNo.Text = read["rafno"].ToString();
                txtAciklama.Text = read["aciklama"].ToString();
            }
            baglan.Close();
        }
    }
}
