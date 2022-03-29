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
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }

        Sql bgl = new Sql();
        public string TC;

        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = TC;

            //Doktor Ad Soyad
            SqlCommand Com = new SqlCommand("select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorTC=@V1", bgl.baglanti());
            Com.Parameters.AddWithValue("@V1",lblTC.Text);
            SqlDataReader Dr = Com.ExecuteReader();
            while (Dr.Read())
            {
                lblAdSoyad.Text = Dr[0] + " " + Dr[1];
            }
            bgl.baglanti().Close();

            //Randevular
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Randevuid as 'No',RandevuTarih as 'Tarih',RandevuSaat as 'Saat',RandevuDurum as 'Durum',HastaTC as 'T.C.',HastaSikayet as 'Şikayet' from Tbl_Randevular where RandevuDoktor='" + lblAdSoyad.Text+"'",bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnGıncelle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDuzenle FrmDBD = new FrmDoktorBilgiDuzenle();
            FrmDBD.TCNO = lblTC.Text;
            FrmDBD.Show();
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular FrmD = new FrmDuyurular();
            FrmD.Show();

        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Secilen = dataGridView1.SelectedCells[0].RowIndex;
            rcrSikayet.Text = dataGridView1.Rows[Secilen].Cells[5].Value.ToString();
        }
    }
}
