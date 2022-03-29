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
    public partial class FrmBrans : Form
    {
        public FrmBrans()
        {
            InitializeComponent();
        }

        Sql bgl = new Sql();

        private void btnListele_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("select Bransid as 'Branş No', BransAd as 'Branş Adı' from Tbl_Branslar", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand Com = new SqlCommand("insert into Tbl_Branslar (BransAd) values (@V1)",bgl.baglanti());
            Com.Parameters.AddWithValue("@v1",txtBransAd.Text);
            Com.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş Eklenmiştir.","BİLGİLENDİRME",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView1.Rows[Secilen].Cells[0].Value.ToString();
            txtBransAd.Text = dataGridView1.Rows[Secilen].Cells[1].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand Com = new SqlCommand("delete from Tbl_Branslar where Bransid=@v1",bgl.baglanti());
            Com.Parameters.AddWithValue("@V1",txtId.Text);
            Com.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş Silinmiştir","BİLGİLENDİRME",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand Com = new SqlCommand("update Tbl_Branslar set BransAd=@V1 where Bransid=@V2", bgl.baglanti());
            Com.Parameters.AddWithValue("@V1",txtBransAd.Text);
            Com.Parameters.AddWithValue("@V2",txtId.Text);
            Com.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş Bilgileri Güncellenmiştir","BİLGİLENDİRME",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void FrmBrans_Load(object sender, EventArgs e)
        {

        }
    }
}
