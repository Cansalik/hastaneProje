using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace hastaneProje
{
    public partial class FrmHastaKayit : Form
    {
        public FrmHastaKayit()
        {
            InitializeComponent();
        }

        Sql bgl = new Sql();

        private void btnKayit_Click(object sender, EventArgs e)
        {
            SqlCommand Com = new SqlCommand("insert into Tbl_Hastalar (HastaAd, HastaSoyad, HastaTC, HastaTelefon, HastaSifre, HastaCinsiyet) values (@V1,@V2,@V3,@V4,@V5,@V6)",bgl.baglanti());
            Com.Parameters.AddWithValue("@V1",txtAd.Text);
            Com.Parameters.AddWithValue("@V2", txtSoyad.Text);
            Com.Parameters.AddWithValue("@V3", mskTC.Text);
            Com.Parameters.AddWithValue("@V4", mskCep.Text);
            Com.Parameters.AddWithValue("@V5", txtSifre.Text);
            Com.Parameters.AddWithValue("@V6", cmbCinsiyet.Text);
            Com.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kaydınız Gerçekleşmiştir. Şifreniz: "+ txtSifre.Text,"BİLGİLENDİRME",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
