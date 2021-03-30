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
    public partial class EmanetKitapİadefrm : Form
    {
        public EmanetKitapİadefrm()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Server=.;Database=KütüphaneOtomasyonu;Trusted_Connection=True;");
        DataSet daset = new DataSet();
        private void EmanetListele()
        {
            baglan.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from emanetkitap", baglan);
            adtr.Fill(daset, "emanetkitap");
            dataGridView1.DataSource = daset.Tables["emanetkitap"];
            baglan.Close();
        }
            private void EmanetKitapİadefrm_Load(object sender, EventArgs e)
        {
            EmanetListele();

        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTcAra_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["emanetkitap"].Clear();
            baglan.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from emanatkitap where tc like '%"+txtTcAra.Text+"%'", baglan);
            adtr.Fill(daset,"emanetkitap");
            baglan.Close();
            if (txtTcAra.Text=="")
            {
                daset.Tables["emanetkitap"].Clear();
                EmanetListele();

            }
        }

        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["emanetkitap"].Clear();
            baglan.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from emanatkitap where barkodno like '%" + txtBarkodNo.Text + "%'", baglan);
            adtr.Fill(daset, "emanetkitap");
            baglan.Close();
            if (txtBarkodNo.Text == "")
            {
                daset.Tables["emanetkitap"].Clear();
                EmanetListele();

            }
        }

        private void btnTeslimAl_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("delete from emanetkitap where tc=@tc and barkodno=@barkodno", baglan);
            komut.Parameters.AddWithValue("@tc",dataGridView1.CurrentRow.Cells["tc"].Value.ToString());
            komut.Parameters.AddWithValue("@barkodno", dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString());
            komut.ExecuteNonQuery();
            SqlCommand komut2 = new SqlCommand("update kitap set stoksayisi=stoksayisi+'"+dataGridView1.CurrentRow.Cells["kitapsayisi"].Value.ToString()+"'where barkodno=@barkodno", baglan);
            komut2.Parameters.AddWithValue("@barkodno", dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString());
            komut2.ExecuteNonQuery();
            baglan.Close();
            MessageBox.Show("Kitap");
            daset.Tables["emanetkitap"].Clear();
            EmanetListele();
        }
    }
}
