﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WndApp2.DAL;
using WndApp2.Model;

namespace WndApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KitapDAL db = new KitapDAL();
            dataGridView1.DataSource = db.KitapListeVM();

            cmbYazar.DataSource = db.Yazarlar();
            cmbKategori.DataSource = db.Kategoriler();

            cmbKategori.DisplayMember = "KategoriAdi";
            cmbKategori.ValueMember = "KategoriID";

            cmbYazar.DisplayMember = "YazarADSOYAD";
            cmbYazar.ValueMember = "YazarID";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KitapDAL db = new KitapDAL();
            Kitap kitap = new Kitap();
            kitap.KitapAdi = txtKitapAdi.Text;
            kitap.KategoriID = ((Kategori)cmbKategori.SelectedItem).KategoriID;
            kitap.YazarID = ((Yazar)cmbYazar.SelectedItem).YazarID;
            db.KitapEkle(kitap);
        }
    }
}
