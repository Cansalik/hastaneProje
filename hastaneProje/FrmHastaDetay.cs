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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }

        Sql bgl = new Sql();

        public string TC;

        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = TC;

            //Ad,Soyad Çekme
            SqlCommand Com = new SqlCommand("Select HastaAd,HastaSoyad from Tbl_Hastalar where HastaTC=@V1", bgl.baglanti());
            Com.Parameters.AddWithValue("@V1", lblTC.Text);
            SqlDataReader Dr = Com.ExecuteReader();
            while (Dr.Read())
            {
                lblAdSoyad.Text = Dr[0] + " " + Dr[1];
            }
            bgl.baglanti().Close();

            //Randevu Geçmişi
            DataTable dt = new DataTable();
            SqlDataAdapter Da = new SqlDataAdapter("select RandevuTarih as 'Tarih',RandevuBrans as 'Branş',RandevuDoktor as 'Doktor',HastaSikayet as 'Şikayet' from Tbl_Randevular where HastaTC=" + TC, bgl.baglanti());
            Da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Brnaşları Çekme
            SqlCommand Com2 = new SqlCommand("select BransAd from Tbl_Branslar",bgl.baglanti());
            SqlDataReader Dr2 = Com2.ExecuteReader();
            while (Dr2.Read())
            {
                cmbBrans.Items.Add(Dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            SqlCommand Com3 = new SqlCommand("select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@V1",bgl.baglanti());
            Com3.Parameters.AddWithValue("@v1",cmbBrans.Text);
            SqlDataReader Dr3 = Com3.ExecuteReader();
            while (Dr3.Read())
            {
                cmbDoktor.Items.Add(Dr3[0] + " " + Dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Randevuid as 'No',RandevuTarih as 'Tarih',RandevuSaat as 'Saat',RandevuDoktor as 'Doktor' from Tbl_Randevular where RandevuBrans= '" + cmbBrans.Text + "'"+ "and RandevuDoktor='"+cmbDoktor.Text+"'and RandevuDurum=0", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void lnkBilgiGuncelle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle Fr = new FrmBilgiDuzenle();
            Fr.TCno = lblTC.Text;
            Fr.Show();
        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand Com = new SqlCommand("update Tbl_Randevular set RandevuDurum=1,HastaTC=@V1,HastaSikayet=@V2 where Randevuid=@V3",bgl.baglanti());
            Com.Parameters.AddWithValue("@V1",lblTC.Text);
            Com.Parameters.AddWithValue("@V2",rchSikayet.Text);
            Com.Parameters.AddWithValue("@V3",txtRndvID.Text);
            Com.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Alındı","BİLGİLENDİRME",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            txtRndvID.Text = " ";
            cmbBrans.Text = " ";
            cmbDoktor.Text = " ";
            rchSikayet.Text = "";
            
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtRndvID.Text = dataGridView2.Rows[Secilen].Cells[0].Value.ToString();
        }
    }
}
