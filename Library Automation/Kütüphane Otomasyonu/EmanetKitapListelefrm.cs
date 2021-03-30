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
    public partial class EmanetKitapListelefrm : Form
    {
        public EmanetKitapListelefrm()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Server=.;Database=KütüphaneOtomasyonu;Trusted_Connection=True;");
        DataSet daset = new DataSet();
        private void EmanetKitapListelefrm_Load(object sender, EventArgs e)
        {
            EmanetListele();
            comboBox1.SelectedIndex = 0;
        }

        private void EmanetListele()
        {
            baglan.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from emanetkitap", baglan);
            adtr.Fill(daset, "emanetkitap");
            dataGridView1.DataSource = daset.Tables["emanetkitap"];
            baglan.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            daset.Tables["emanetkitap"].Clear();
            if (comboBox1.SelectedIndex==0)
            {
                EmanetListele();
            }
            else if (comboBox1.SelectedIndex==1)
            {
                baglan.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select * from emanetkitap where'"+DateTime.Now.ToShortDateString()+"'>iadetarihi", baglan);
                adtr.Fill(daset, "emanetkitap");
                dataGridView1.DataSource = daset.Tables["emanetkitap"];
                baglan.Close();
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                baglan.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select * from emanetkitap where'" + DateTime.Now.ToShortDateString() + "'<= iadetarihi", baglan);
                adtr.Fill(daset, "emanetkitap");
                dataGridView1.DataSource = daset.Tables["emanetkitap"];
                baglan.Close();
            }
        }
    }
}
