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

namespace hastaneProje
{
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }

        Sql bgl = new Sql();
        public string TCNO;
        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            mskTC.Text = TCNO;
            SqlCommand Com = new SqlCommand("select * from Tbl_Doktorlar where DoktorTC=@V1",bgl.baglanti());
            Com.Parameters.AddWithValue("@V1",mskTC.Text);
            SqlDataReader dr = Com.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                cmbBrans.Text = dr[3].ToString();
                txtSifre.Text = dr[5].ToString();
            }
            bgl.baglanti().Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand Com = new SqlCommand("update Tbl_Doktorlar set DoktorAd=@V1,DoktorSoyad=@V2,DoktorBrans=@V3,DoktorSifre=@V4 where DoktorTC=@V5",bgl.baglanti());
            Com.Parameters.AddWithValue("@V1",txtAd.Text);
            Com.Parameters.AddWithValue("@V2", txtSoyad.Text);
            Com.Parameters.AddWithValue("@V3", cmbBrans.Text);
            Com.Parameters.AddWithValue("@V4", txtSifre.Text);
            Com.Parameters.AddWithValue("@V5", mskTC.Text);
            Com.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Güncellendi","BİLGİLENDİRME",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
