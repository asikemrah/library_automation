using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kütüphane_Otomasyonu_18350092
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }

        private void btnUyeEkle_Click(object sender, EventArgs e)
        {
            UyeEkleForm uyeeklefrm = new UyeEkleForm();
            uyeeklefrm.ShowDialog();
        }

        private void btnUyeListele_Click(object sender, EventArgs e)
        {
            UyeEkleForm uyelistele = new UyeEkleForm();
            uyelistele.ShowDialog();
        }

        private void btnKitapEkle_Click(object sender, EventArgs e)
        {
            KitapEkleForm ktpekle = new KitapEkleForm();
            ktpekle.ShowDialog();
        }

        private void btnKitapListele_Click(object sender, EventArgs e)
        {
            KitapListeleForm ktpliste = new KitapListeleForm();
            ktpliste.ShowDialog();
        }

        private void btnEmanetVer_Click(object sender, EventArgs e)
        {
            EmanetKitapVerfrm emanetkitapver = new EmanetKitapVerfrm();
            emanetkitapver.ShowDialog();
        }

        private void btnEmanetListele_Click(object sender, EventArgs e)
        {
            EmanetKitapListelefrm listele = new EmanetKitapListelefrm();
            listele.ShowDialog();
        }

        private void btnEmanetİade_Click(object sender, EventArgs e)
        {
            EmanetKitapİadefrm iade = new EmanetKitapİadefrm();
            iade.ShowDialog();
        }

        private void btnSiralama_Click(object sender, EventArgs e)
        {
            Sıralamafrm sırala = new Sıralamafrm();
            sırala.ShowDialog();
        }

        private void btnGrafik_Click(object sender, EventArgs e)
        {
            Grafikfrm grafik = new Grafikfrm();
            grafik.ShowDialog();
        }
    }
}
