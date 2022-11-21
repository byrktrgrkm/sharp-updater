using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UpdateApp;

namespace SharpUpdate
{
    public partial class SharpUpdateAcceptForm : Form
    {
        private ISharpUpdate applicationInfo;
        private SharpUpdateXml updateInfo;
        private SharpUpdateInfoForm updateInfoForm;

        internal SharpUpdateAcceptForm(ISharpUpdate applicationInfo,SharpUpdateXml updateInfo)
        {
            InitializeComponent();

            this.applicationInfo = applicationInfo;
            this.updateInfo = updateInfo;

            this.Text = this.applicationInfo.ApplicationName + " - Güncelleme Mevcut";
            if (this.applicationInfo.ApplicationIcon != null)
                this.Icon = this.applicationInfo.ApplicationIcon;

            this.lblNewVersion.Text = string.Format("Yeni versiyon: {0}", this.updateInfo.Version.ToString());


        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            if (this.updateInfoForm == null)
                this.updateInfoForm = new SharpUpdateInfoForm(this.applicationInfo, this.updateInfo);

            this.updateInfoForm.ShowDialog(this);
        }
    }
}
