using SharpUpdate;
using System;
using System.Windows.Forms;
namespace UpdateApp
{
    public partial class SharpUpdateInfoForm : Form
    {
        public SharpUpdateInfoForm(ISharpUpdate applicationIfo,SharpUpdateXml updateInfo)
        {
            InitializeComponent();

            if (applicationIfo.ApplicationIcon != null)
                this.Icon = applicationIfo.ApplicationIcon;

            this.Text = applicationIfo.ApplicationName + " - Güncelleme Bilgisi";
            this.lblVersions.Text = String.Format("Mevcut Versiyon: {0}\n Güncel Versiyon: {1}",applicationIfo.ApplicationAssembly.GetName().Version.ToString(),
                updateInfo.Version.ToString());

            this.txtDescription.Text = updateInfo.Description;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if(!(e.Control && e.KeyCode == Keys.C))
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
