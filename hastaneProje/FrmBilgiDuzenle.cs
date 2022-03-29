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
    public partial class FrmBilgiDuzenle : Form
    {
        public FrmBilgiDuzenle()
        {
            InitializeComponent();
        }

        Sql bgl = new Sql();

        public string TCno;
        private void FrmBilgiDuzenle_Load(object sender, EventArgs e)
        {
            mskTC.Text = TCno;
            txtSifre.UseSystemPasswordChar = true;
            chkGizle.Checked = true;
            SqlCommand Com = new SqlCommand("select * from Tbl_Hastalar where HastaTC=@V1", bgl.baglanti());
            Com.Parameters.AddWithValue("@V1",mskTC.Text);
            SqlDataReader Dr = Com.ExecuteReader();
            while (Dr.Read())
            {
                txtAd.Text = Dr[1].ToString();
                txtSoyad.Text = Dr[2].ToString();
                mskCep.Text = Dr[4].ToString();
                txtSifre.Text = Dr[5].ToString();
                cmbCinsiyet.Text = Dr[6].ToString();
            }
            bgl.baglanti().Close();
        }

        private void chkGizle_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGizle.Checked == true)
            {
                txtSifre.UseSystemPasswordChar = true;
                pcrGizli.Visible = true;
                pcrGorunur.Visible = false;
            }
            else
            {
                txtSifre.UseSystemPasswordChar = false;
                pcrGizli.Visible = false;
                pcrGorunur.Visible = true;
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand Com2 = new SqlCommand("update Tbl_Hastalar set HastaAd=@V1, HastaSoyad=@V2, HastaTelefon=@V3, HastaSifre=@V4, HastaCinsiyet=@V5 where HastaTC=@V6",bgl.baglanti());
            Com2.Parameters.AddWithValue("@V1",txtAd.Text);
            Com2.Parameters.AddWithValue("@V2", txtSoyad.Text);
            Com2.Parameters.AddWithValue("@V3", mskCep.Text);
            Com2.Parameters.AddWithValue("@V4", txtSifre.Text);
            Com2.Parameters.AddWithValue("@V5", cmbCinsiyet.Text);
            Com2.Parameters.AddWithValue("@V6", mskTC.Text);
            Com2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Güncellenmiştir.","BİLGİLENDİRME",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
