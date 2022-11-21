using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SharpUpdate
{
    public class SharpUpdater
    {

        private ISharpUpdate applicationInfo;
        private BackgroundWorker bgWorker;

        public SharpUpdater(ISharpUpdate applicationInfo)
        {
            this.applicationInfo = applicationInfo;

            this.bgWorker = new BackgroundWorker();

            bgWorker.DoWork += new DoWorkEventHandler(BgWorker_DoWork);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BgWorker_RunWorkerCompleted);
        }
        public void DoUpdate()
        {
            if (!this.bgWorker.IsBusy)
            {
                this.bgWorker.RunWorkerAsync(this.applicationInfo);
            }
        }
        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ISharpUpdate application = (ISharpUpdate)e.Argument;
            if (!SharpUpdateXml.ExistsOnServer(application.UpdateXmlLocation))
                e.Cancel = true;
            else
                e.Result = SharpUpdateXml.Parse(application.UpdateXmlLocation, application.ApplicationID);
        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                SharpUpdateXml update = (SharpUpdateXml)e.Result;
                if(update != null && update.IsNewerThan(this.applicationInfo.ApplicationAssembly.GetName().Version))
                {
                    if( new SharpUpdateAcceptForm(this.applicationInfo, update).ShowDialog(this.applicationInfo.Context) == DialogResult.Yes)
                    {
                        this.DownloadUpdate(update);
                    }
                }
                else
                {
                    MessageBox.Show("Uygulamanız Güncel");
                }
            }
        }

        private void DownloadUpdate(SharpUpdateXml update)
        {
            SharpUpdateDownlaodForm form = new SharpUpdateDownlaodForm(update.Uri, update.MD5, this.applicationInfo.ApplicationIcon);

            DialogResult result = form.ShowDialog(this.applicationInfo.Context);

            if(result == DialogResult.OK)
            {
                string currentPath = this.applicationInfo.ApplicationAssembly.Location;
                string newpath = Path.GetDirectoryName(currentPath) + "\\" + update.FileName;

                UpdateApplication(form.TempFilePath, currentPath, newpath, update.LaunchArgs);
                Application.Exit();
            }else if(result == DialogResult.Abort)
            {
                MessageBox.Show("Güncelleme işlemi iptal edildi.\n Programda değişiklik yapılmadı.","Güncelleme Iptal Edildi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Güncelleme yaparken bir hata oluştu. Lütfen daha sonra tekrar deneyin.", "Güncelleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateApplication(string tempFilePath, string currentPath, string newpath, string launchArgs)
        {
            string argument = "/C Choice /C Y /N /D Y /T 4 & Del /F /Q \"{0}\" & Choice /C Y /N /D Y /T 2 & Move /Y \"{1}\" \"{2}\" & Start \"\" /D \"{3}\" \"{4}\" {5}";
            ProcessStartInfo info = new ProcessStartInfo();
            info.Arguments = string.Format(argument, currentPath, tempFilePath, newpath, Path.GetDirectoryName(newpath), Path.GetFileName(newpath), launchArgs);
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.CreateNoWindow = true;
            info.FileName = "cmd.exe";
            Process.Start(info);

        }
    }
}
