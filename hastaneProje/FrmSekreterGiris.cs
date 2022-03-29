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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }

        Sql bgl = new Sql();

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand com = new SqlCommand("select * from Tbl_Sekreter where SekreterTC=@V1 and SekreterSifre=@V2",bgl.baglanti());
            com.Parameters.AddWithValue("@V1",mskTC.Text);
            com.Parameters.AddWithValue("@V2",txtSifre.Text);
            SqlDataReader Dr = com.ExecuteReader();
            if (Dr.Read())
            {
                FrmSekreterDetay Frs = new FrmSekreterDetay();
                Frs.TCnumara = mskTC.Text;
                Frs.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Bilgileri.","HATA",MessageBoxButtons.OK,MessageBoxIcon.Error);
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

        private void FrmSekreterGiris_Load(object sender, EventArgs e)
        {
            chkGizle.Checked = true;
        }
    }
}
