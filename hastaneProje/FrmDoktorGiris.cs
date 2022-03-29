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
    public partial class FrmDoktorGiris : Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent();
        }

        Sql bgl = new Sql();

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand Com = new SqlCommand("select * from Tbl_Doktorlar where DoktorTC=@V1 and DoktorSifre=@V2",bgl.baglanti());
            Com.Parameters.AddWithValue("@V1",mskTC.Text);
            Com.Parameters.AddWithValue("@V2",txtSifre.Text);
            SqlDataReader Dr = Com.ExecuteReader();
            if (Dr.Read())
            {
                FrmDoktorDetay FrmD= new FrmDoktorDetay();
                FrmD.TC = mskTC.Text;
                FrmD.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Bilgileri","HATA",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
    }
}
