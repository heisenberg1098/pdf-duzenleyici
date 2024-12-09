using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfiumViewer;


namespace pdf_duzenleyici
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }
        public void SplitPdf(string sourceFile, string destinationFile, int[] pages)
        {
            // Kaynak PDF dosyasını açın
            using (PdfReader reader = new PdfReader(sourceFile))
            {
                // Yeni bir PDF dosyası oluşturun
                using (FileStream fs = new FileStream(destinationFile, FileMode.Create))
                using (Document document = new Document())
                using (PdfCopy copy = new PdfCopy(document, fs))
                {
                    document.Open();

                    // Belirtilen sayfaları yeni PDF'ye ekleyin
                    foreach (int pageNumber in pages)
                    {
                        if (pageNumber <= reader.NumberOfPages && pageNumber > 0)
                        {
                            copy.AddPage(copy.GetImportedPage(reader, pageNumber));
                        }
                    }
                }
            }
        }
        string kaynak = @"C:\Users\muham\Downloads\APOTEMİ PROBLEMLER FASİKÜLÜ 2023.pdf";
        string hedef = "deneme_kitabi1.pdf";
        // Yeni PDF'de görmek istediğiniz sayfa numaraları
        //SplitPdf(kaynak, hedef, hangi_sayfalar);
        private void Form1_Load(object sender, EventArgs e)
        {

            //SplitPdf(kaynak, hedef, hangi_sayfalar);



        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                kaynak = dialog.FileName;
                label1.Text = kaynak;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int min = Convert.ToInt32(maskedTextBox1.Text);
            int max = Convert.ToInt32(maskedTextBox2.Text);
            int[] hangi_sayfalar = new int[max - min];
            int a = 0;
            for (int i = min; i < max; i++)
            {
                hangi_sayfalar[a] = i;
                a++;
            }
            hedef = textBox1.Text + ".pdf";
            foreach (var item in hangi_sayfalar)
            {
                label4.Text += item + " ";
            }
            SplitPdf(kaynak, hedef, hangi_sayfalar);
        }
    }
}
