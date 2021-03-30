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
    public partial class UyeListeleme : Form
    {
        public UyeListeleme()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTc.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
        }

        SqlConnection baglan = new SqlConnection("Server=.;Database=KütüphaneOtomasyonu;Trusted_Connection=True;");
        private void txtTc_TextChanged(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand com = new SqlCommand("Select * from uye where tc like '"+txtTc.Text+" '", baglan);
            SqlDataReader read = com.ExecuteReader();
            while (read.Read())
            {
                txtAdSoyad.Text = read["adsoyad"].ToString();
                txtYas.Text = read["yas"].ToString();
                cmbCnsyt.Text = read["cinsiyet"].ToString();
                txtTel.Text = read["telefon"].ToString();
                txteposta.Text = read["eposya"].ToString();
                txtokunankitap.Text = read["okunankitapsayısı"].ToString();
            }
            baglan.Close();
        }
        DataSet dt = new DataSet();
        private void txtara_TextChanged(object sender, EventArgs e)
        {
            dt.Tables["uye"].Clear();
            baglan.Open();
            SqlDataAdapter zd  = new SqlDataAdapter("select *from uye where tc like '%" + txtara.Text + "% '", baglan);
            zd.Fill(dt,"uye");
            dataGridView1.DataSource = dt.Tables["uye"];
            baglan.Close();
        }

        private void btnUyeCıkıs_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu Kayıtı Silmek İstiyor musunuz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialog==DialogResult.Yes)
            {
                baglan.Open();
                SqlCommand com = new SqlCommand("delete from uye where tc=@tc", baglan);
                com.Parameters.AddWithValue("@tc", dataGridView1.CurrentRow.Cells["tc"].Value.ToString());
                com.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Silme İşlemi Başarılı");
                dt.Tables["uye"].Clear();
                uyelistele();
                foreach (Control item in Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
            
        }
        private void uyelistele()
        {
            baglan.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from uye",baglan);
            sda.Fill(dt, "uye");
            dataGridView1.DataSource = dt.Tables["uye"];
            baglan.Close();

        }
        private void UyeListeleme_Load(object sender, EventArgs e)
        {
            uyelistele();
        }

        private void btnUyeGuncellebtnUyeGuncelle_Click(object sender, EventArgs e)
        {
            baglan.Open();
        }
    }
}
