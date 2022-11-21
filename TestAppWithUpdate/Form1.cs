using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpUpdate;

namespace TestAppWithUpdate
{
    public partial class Form1 : Form, ISharpUpdate
    {
        SharpUpdater updater;

        public Form1()
        {
            InitializeComponent();

          
            this.label1.Text = this.ApplicationAssembly.GetName().Version.ToString();

            updater = new SharpUpdater(this);

            /*
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                    | SecurityProtocolType.Tls11
                    | SecurityProtocolType.Tls12
                    | SecurityProtocolType.Ssl3;
                    */
           /* ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.DefaultConnectionLimit = 9999;


            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://updater.gorkembayraktar.com/update.xml");
            req.Method = "GET";
          


            // Skip validation of SSL/TLS certificate
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };


            WebResponse respon = req.GetResponse();
            Stream res = respon.GetResponseStream();

            string ret = "";
            byte[] buffer = new byte[1048];
            int read = 0;
            while ((read = res.Read(buffer, 0, buffer.Length)) > 0)
            {
                Console.Write(Encoding.ASCII.GetString(buffer, 0, read));
                ret += Encoding.ASCII.GetString(buffer, 0, read);
            }


            string x = "";

    */
        }

        public string ApplicationName
        {
            get { return "TestAppWithUpdate"; }
        }

        public string ApplicationID 
        {
            get { return "TestAppWithUpdate"; }
        }

        public Assembly ApplicationAssembly 
        {
            get { return Assembly.GetExecutingAssembly(); }
        }

        public Icon ApplicationIcon
        {
            get { return this.Icon; }
        }

        public Uri UpdateXmlLocation
        {
            get { return new Uri("http://updater.gorkembayraktar.com/update.xml"); }
        }

        public Form Context
        {
            get { return this; }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            updater.DoUpdate();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
            int x1 = Convert.ToInt32(textBox1.Text);
            int x2 = Convert.ToInt32(textBox2.Text);
            button1.Text = ((x1 + x2).ToString());

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
