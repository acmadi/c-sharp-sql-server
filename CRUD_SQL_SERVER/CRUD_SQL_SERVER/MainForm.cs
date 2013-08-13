using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace CRUD_SQL_SERVER
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private DataSet ds;
        private DataView dv;
        private int res;

        private void SetTabel(DataSet ds)
        {
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].HeaderText = "No Induk";
            dataGridView1.Columns[0].Width = 75;
            dataGridView1.Columns[1].HeaderText = "Nama";
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].HeaderText = "Alamat";
            dataGridView1.Columns[2].Width = 180;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                btn_simpan.Enabled = true;
                btn_ubah.Enabled = false;
                btn_hapus.Enabled = false;
                btn_cari.Enabled = true;
                btn_bersih.Enabled = false;
                txt_nim.Enabled = true;
                txt_nim.Clear();
                txt_nama.Clear();
                txt_alamat.Clear();
                ds = Connection.GetInstance().GetMahasiswa();
                dv = new DataView(ds.Tables[0]);
                SetTabel(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_cari_Click(object sender, EventArgs e)
        {
            if (txt_nim.Text != "")
            {
                ds = Connection.GetInstance().GetMahasiswa("where nim = '"+txt_nim.Text+"'");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        txt_nama.Text = row["nama"].ToString();
                        txt_alamat.Text = row["alamat"].ToString();
                    }
                    btn_simpan.Enabled = false;
                    btn_ubah.Enabled = true;
                    btn_hapus.Enabled = true;
                    btn_cari.Enabled = false;
                    btn_bersih.Enabled = true;
                    txt_nim.Enabled = false;
                    SetTabel(ds);
                }
                else
                {
                    MessageBox.Show("Data yang anda cari tidak ada !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Inputkan no induk yang anda cari !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_bersih_Click(object sender, EventArgs e)
        {
            MainForm_Load(null, null);
        }

        private void btn_simpan_Click(object sender, EventArgs e)
        {
            if (txt_nim.Text != "" && txt_nama.Text != "" && txt_alamat.Text != "")
            {
                dv.Sort = "nim";
                if (dv.Find(txt_nim.Text) < 0)
                {
                    res = 0;
                    res = Connection.GetInstance().InsertMahasiswa(txt_nim.Text, txt_nama.Text, txt_alamat.Text);
                    if (res == 1)
                    {
                        MainForm_Load(null, null);
                        MessageBox.Show("insert Data Sukses !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Insert data Gagal !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("No induk sudah dipakai !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Data gak lengkap !","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void btn_ubah_Click(object sender, EventArgs e)
        {
            if (txt_nama.Text != "" && txt_alamat.Text != "")
            {
                res = 0;
                res = Connection.GetInstance().UpdateMahasiswa(txt_nim.Text, txt_nama.Text, txt_alamat.Text);
                if (res == 1)
                {
                    MainForm_Load(null, null);
                    MessageBox.Show("Ubah Data Sukses !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ubah data Gagal !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Data gak lengkap !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_hapus_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Yakin hapus data ini ?","Question",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                res = 0;
                res = Connection.GetInstance().DeleteMahasiswa(txt_nim.Text);
                if (res == 1)
                {
                    MainForm_Load(null, null);
                    MessageBox.Show("Hapus Data Sukses !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Hapus data Gagal !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
