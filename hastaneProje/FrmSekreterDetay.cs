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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        Sql bgl = new Sql();

        public string TCnumara;

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = TCnumara;

            //Ad Soyad Getirme
            SqlCommand Com1 = new SqlCommand("select SekreterAdSoyad from Tbl_Sekreter where SekreterTC="+TCnumara,bgl.baglanti());
            SqlDataReader Dr1 = Com1.ExecuteReader();
            while (Dr1.Read())
            {
                lblAdSoyad.Text = Dr1[0].ToString();
            }
            bgl.baglanti().Close();

            //Bransları DataGridView Aktarma
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Bransid as 'Branş No',BransAd as 'Branş' from Tbl_Branslar",bgl.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //Doktorları DataGridView Aktarma
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select (DoktorAd + ' ' +DoktorSoyad) as 'Adı',DoktorBrans as 'Branşı' from Tbl_Doktorlar", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //Branşları ComboBox Aktarma
            SqlCommand Com2 = new SqlCommand("select BransAd from Tbl_Branslar",bgl.baglanti());
            SqlDataReader Dr2 = Com2.ExecuteReader();
            while (Dr2.Read())
            {
                cmbBrans.Items.Add(Dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void btnRndvOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand Com1 = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@V1,@V2,@V3,@V4)",bgl.baglanti());
            Com1.Parameters.AddWithValue("@V1",mskTarih.Text);
            Com1.Parameters.AddWithValue("@V2", mskSaat.Text);
            Com1.Parameters.AddWithValue("@V3", cmbBrans.Text);
            Com1.Parameters.AddWithValue("@V4", cmbDoktor.Text);
            Com1.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu.","BİLGİLENDİRME",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();

            //Doktorları Branslara Göre Çekme
            SqlCommand Com = new SqlCommand("select DoktorAd, DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@V1",bgl.baglanti());
            Com.Parameters.AddWithValue("@V1",cmbBrans.Text);
            SqlDataReader Dr = Com.ExecuteReader();
            while (Dr.Read())
            {
                cmbDoktor.Items.Add(Dr[0] + " " + Dr[1]);
            }
            bgl.baglanti().Close();
        }

        private void btnDuyuru_Click(object sender, EventArgs e)
        {
            SqlCommand Com = new SqlCommand("insert into Tbl_Duyurular (Duyuru) values (@V1)",bgl.baglanti());
            Com.Parameters.AddWithValue("@V1",rchDuyuru.Text);
            Com.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Kaydedildi","BİLGİLENDİRME",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli FrmDP = new FrmDoktorPaneli();
            FrmDP.Show();
        }

        private void btnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBrans FrmB = new FrmBrans();
            FrmB.Show();
        }

        private void btnRndvList_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi FrmRnd = new FrmRandevuListesi();
            FrmRnd.Show();
        }

        private void btnDuyuruGoruntule_Click(object sender, EventArgs e)
        {
            FrmDuyurular FrD = new FrmDuyurular();
            FrD.Show();
        }
    }
}
