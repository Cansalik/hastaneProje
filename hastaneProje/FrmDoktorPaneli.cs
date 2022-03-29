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
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }

        Sql bgl = new Sql();

        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            //Brnaşları ComboBox'a Aktarma 
            SqlCommand Com2 = new SqlCommand("select BransAd from Tbl_Branslar", bgl.baglanti());
            SqlDataReader Dr2 = Com2.ExecuteReader();
            while (Dr2.Read())
            {
                cmbBrans.Items.Add(Dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand Com = new SqlCommand("insert into Tbl_Doktorlar (DoktorAd,DoktorSoyad,DoktorBrans,DoktorTC,DoktorSifre) values (@V1,@V2,@V3,@V4,@V5)",bgl.baglanti());
            Com.Parameters.AddWithValue("@V1",txtAd.Text);
            Com.Parameters.AddWithValue("@V2", txtSoyad.Text);
            Com.Parameters.AddWithValue("@V3", cmbBrans.Text);
            Com.Parameters.AddWithValue("@V4", mskTC.Text);
            Com.Parameters.AddWithValue("@V5", txtSifre.Text);
            Com.ExecuteNonQuery();
            MessageBox.Show("Doktor Kaydedilmiştir.","BİLGİLENDİRME",MessageBoxButtons.OK,MessageBoxIcon.Information);
            bgl.baglanti().Close();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("select Doktorid as 'No', DoktorAd as 'Ad',  DoktorSoyad as 'Soyad',DoktorBrans as 'Brnaşı',DoktorTC as 'T.C.',DoktorSifre as 'Şifre' from Tbl_Doktorlar", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtAd.Text = dataGridView1.Rows[Secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[Secilen].Cells[2].Value.ToString();
            cmbBrans.Text = dataGridView1.Rows[Secilen].Cells[3].Value.ToString();
            mskTC.Text = dataGridView1.Rows[Secilen].Cells[4].Value.ToString();
            txtSifre.Text = dataGridView1.Rows[Secilen].Cells[5].Value.ToString();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand Com = new SqlCommand("delete from Tbl_Doktorlar where DoktorTC="+mskTC.Text,bgl.baglanti());
            Com.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Listeden Silinmiştir.","BİLGİLENDİRME",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand Com = new SqlCommand("update Tbl_Doktorlar set DoktorAd=@V1,DoktorSoyad=@V2,DoktorBrans=@V3,DoktorSifre=@V5 where DoktorTC="+mskTC.Text,bgl.baglanti());
            Com.Parameters.AddWithValue("@V1", txtAd.Text);
            Com.Parameters.AddWithValue("@V2", txtSoyad.Text);
            Com.Parameters.AddWithValue("@V3", cmbBrans.Text);
            Com.Parameters.AddWithValue("@V5", txtSifre.Text);
            Com.ExecuteNonQuery();
            MessageBox.Show("Doktor Bilgileri Güncellenmiştir.", "BİLGİLENDİRME", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bgl.baglanti().Close();

        }
    }
}
