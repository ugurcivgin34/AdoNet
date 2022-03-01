using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoNet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strConn = "Data Source=.;Initial Catalog=KitapDB;Integrated Security=True";

            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Kitaplar", conn);

            //SqlReader kullanımında select için kullanılır
            SqlDataReader dr = cmd.ExecuteReader();//metod içinde kendisi newliyor. fabrika modeli 

            string baslik = "";
            for (int i = 0; i < dr.FieldCount; i++)
                baslik += dr.GetName(i).ToUpper() + " ";
            listBox1.Items.Add(baslik);

            while (dr.Read())
            {
                string satir = "";
                for (int i = 0; i < dr.FieldCount; i++) //sorgu sonucu satır sonuna kadar okur
                {
                    satir += dr[i] + " ";
                }
                listBox1.Items.Add(satir);
                //listBox1.Items.Add(dr[0] + " " + dr["KitapAdi"]);

            }
            conn.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strConn = "Data Source=.;Initial Catalog=KitapDB;Integrated Security=True";

            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();

            //Bu daha güvenli bir koddur.Böyle yapmak daha sağlıklı
            SqlCommand cmd = new SqlCommand("INSERT INTO Kitaplar(KitapAdi,KategoriID,YazarID) VALUES(@ad,@kID,@yID) ", conn);

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ad", textBox1.Text);
            cmd.Parameters.AddWithValue("@kID", textBox3.Text);
            cmd.Parameters.AddWithValue("@yID", textBox2.Text);

            cmd.ExecuteNonQuery(); //CRUD İŞLEMLERİNDE KULLANILIr
            conn.Close();
            //executescalar bide bu var.Geriye sadece 1 tane değer gönderecekse orda kullanıyoruz
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strConn = "Data Source=.;Initial Catalog=KitapDB;Integrated Security=True";

            //DataTable:
            //DataSet:DataTable'lardan oluşur...Bir veritabanı gibi davranır.Bİr datasetin içinde onlarca datatable olabilir
            //Dataset içinde datatable arasında ilişki de kurulabiliyor.
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Kitaplar",strConn);
            DataTable dt = new DataTable();
            da.Fill(dt); //data atapter doldurmak için kullanılıyor.Gelen verileri dt nin içinde tutuyor.Bunu da fill
            //sağlıyor.Şuan veri localde

            //for(int i = 0; i < dt.Rows.Count; i++)
            //{
            //    string satir = "";
            //    for (int j = 0; j < dt.Columns.Count; j++)
            //    {
            //        satir += dt.Rows[i][j] + " ";
            //    }
            //    listBox1.Items.Add(satir);
            //}
            DataRow yeni = dt.NewRow();
            yeni[0] = 123;
            yeni[1] = "Serenat";
            dt.Rows.Add(yeni); //locale ekliyor şuan

            
            SqlCommandBuilder scb = new SqlCommandBuilder(da);

            da.Update(dt);
            //dt.Rows[0].RowState = DataRowState.Deleted;


            dataGridView1.DataSource = dt;

            

        }
    }
}
