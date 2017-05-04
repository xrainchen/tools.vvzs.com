using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace HttpTools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = GetReponseHtml(textBox1.Text);
        }

        private string GetReponseHtml(string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.81 Safari/537.36";
            req.Method = "GET";
            using (var reponse = req.GetResponse() as HttpWebResponse)
            {
                var reponseStream = reponse?.GetResponseStream();
                if (reponseStream == null) return string.Empty;
                using (var reader = new StreamReader(reponseStream, Encoding.GetEncoding(reponse.CharacterSet??"utf-8")))
                {
                    switch (reponse.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return reader.ReadToEnd();
                    }
                }
            }
            return string.Empty;
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }
    }
}
