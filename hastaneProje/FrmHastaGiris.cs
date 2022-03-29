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
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }

        Sql bgl = new Sql();

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand Com = new SqlCommand("select * from Tbl_Hastalar where HastaTC=@V1 and HastaSifre=@V2",bgl.baglanti());
            Com.Parameters.AddWithValue("@V1",mskTC.Text);
            Com.Parameters.AddWithValue("@V2",txtSifre.Text);
            SqlDataReader Dr = Com.ExecuteReader();
            if (Dr.Read())
            {
                FrmHastaDetay Fr = new FrmHastaDetay();
                Fr.TC = mskTC.Text;
                Fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Bilgileri!","HATA",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void lnkKayit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayit Fr = new FrmHastaKayit();
            Fr.Show();
        }
    }
}
