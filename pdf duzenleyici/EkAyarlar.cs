using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections;
using System.Data.SqlClient;
namespace pdf_duzenleyici
{
    public class EkAyarlar
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GNKKFD3;Initial Catalog=Ders;Integrated Security=True;Encrypt=False");

        public string ilkbuyuk(string isim)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(isim);
        }
        public string Listegos(ArrayList list)
        {
            string str = "";
            for (int i = 0; i < list.Count; i++)
            {
                str += list[i].ToString()+"\n";
            }

            return str;
        }
        public void güncelle(string ad, string yol, string kategori, string cozüm, string id) {
            conn.Open();

            SqlCommand cmd = new SqlCommand(string.Format("update derskitap set pdfAd='{0}',pdf_Yol='{1}',category='{2}',cozum='{3}'Where Id={4}", ad, yol,kategori, cozüm, id), conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

    }
}
