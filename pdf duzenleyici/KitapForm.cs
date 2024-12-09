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
using System.Diagnostics;
using System.Collections;

namespace pdf_duzenleyici
{
    public partial class KitapForm : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GNKKFD3;Initial Catalog=Ders;Integrated Security=True;Encrypt=False");
        public KitapForm()
        {
            InitializeComponent();
        }

        private void KitapForm_Load(object sender, EventArgs e)
        {
          
            conn.Open();
            listBox1.Items.Clear();
            SqlCommand cmd = new SqlCommand("select ad from dersler", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["ad"]);
                listBox1.Items.Add(reader["ad"]);
            }
            conn.Close();reader.Close();
        }
        string pdfyol;
        ArrayList list=new ArrayList();
        ArrayList listcozum=new ArrayList();
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            list.Clear();
            listBox2.Items.Clear();
            
            conn.Open();
            SqlCommand sql = new SqlCommand("select * from derskitap where category=" + (10 * listBox1.SelectedIndex + 131), conn);
            SqlDataReader reader = sql.ExecuteReader();
            while (reader.Read())
            {
                
                listBox2.Items.Add(reader["pdfAd"]);
                list.Add(reader["pdf_Yol"].ToString());
               listcozum.Add(reader["cozum"].ToString());
            }
            conn.Close();reader.Close();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItems.Count > 0)
            {
              
                Process.Start(list[Convert.ToInt16(listBox2.SelectedIndex)].ToString());
                if (!string.IsNullOrEmpty(listcozum[Convert.ToInt16(listBox2.SelectedIndex)].ToString()))
                {
                Process.Start(listcozum[Convert.ToInt16(listBox2.SelectedIndex)].ToString());

                }
            }
            else
            {
                MessageBox.Show("Açılacak Ögeyi seçin");
            }
        }
        string yeniPdfYol;
        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                pdfyol = openFileDialog1.FileName;
            }
        }
        bool yolonay;
        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
                yolonay = true;
            if (!string.IsNullOrEmpty(textBox1.Text) &&!string.IsNullOrEmpty(textBox2.Text) &&!string.IsNullOrEmpty(comboBox1.Text))
            {
                SqlCommand sql = new SqlCommand("select * from derskitap" , conn);
                SqlDataReader reader = sql.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["pdf_Yol"].ToString()==textBox1.Text)
                    {
                        yolonay = false;
                        MessageBox.Show("Yol Başka bir yerde Kullanılıyor!");
                    }
                    
                }
                reader.Close();
                if (yolonay)
                {
                    
                    SqlCommand cmd = new SqlCommand(string.Format("insert into derskitap(pdfAd,pdf_Yol,category,cozum)values('{0}','{1}',{2},'{3}')", en.ilkbuyuk(textBox2.Text), textBox1.Text, (10 * comboBox1.SelectedIndex + 131),textBox3.Text), conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Eklendi");
                    
                }
                conn.Close();
            }
            else
            {
                MessageBox.Show("Lütfen Alanları Doldurunuz");
            }
        }
        EkAyarlar en=new EkAyarlar();

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        //  update derskitap set pdfAd='{0}',pdf_Yol='{1}',category='{2}',cozum='{3}'Where Id={1}

        private void button4_Click(object sender, EventArgs e)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand(string.Format("update derskitap set pdfAd='{0}',pdf_Yol='{1}',category='{2}',cozum='{3}'Where Id={4}",textBox2.Text,textBox1.Text, (10 * comboBox1.SelectedIndex + 131),textBox3.Text,id), conn);
            cmd.ExecuteNonQuery();
            conn.Close();   
        }
        string id;
        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {
            conn.Open();
            SqlCommand sql = new SqlCommand("select * from derskitap where pdf_Yol='" + list[listBox2.SelectedIndex].ToString() + "'", conn);
            SqlDataReader reader = sql.ExecuteReader();
            while (reader.Read())
            {
                id = reader["Id"].ToString();
                textBox2.Text = reader["pdfAd"].ToString();
                textBox1.Text = reader["pdf_Yol"].ToString();
                comboBox1.SelectedIndex = (Convert.ToInt32(reader["category"]) - 131) / 10;
                //(10 * comboBox1.SelectedIndex + 131)



            }
            reader.Close();
            conn.Close();
        }

        
    }
}
