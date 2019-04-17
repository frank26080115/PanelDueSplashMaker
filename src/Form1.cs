using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimplePaletteQuantizer.Helpers;
using SimplePaletteQuantizer.Ditherers;
using SimplePaletteQuantizer.Ditherers.ErrorDiffusion;
using SimplePaletteQuantizer.Ditherers.Ordered;
using SimplePaletteQuantizer.Quantizers;
using SimplePaletteQuantizer.Quantizers.Uniform;

namespace PanelDueSplashMaker
{
    public partial class Form1 : Form
    {
        Image sourceImage;
        Image resultImage = null;
        List<IColorDitherer> dithererList;
        string comPort = "";
        Process proc = null;
        Dictionary<string, string> webFirmwareList = new Dictionary<string, string>();

        private string TemporaryBitmapPath
        {
            get
            {
                return Program.GetTempFolder() + Path.DirectorySeparatorChar + "temp.bmp";
            }
        }
        private string TemporaryLogoPath
        {
            get
            {
                return Program.GetTempFolder() + Path.DirectorySeparatorChar + "templogo.bin";
            }
        }
        private string TemporaryFirmwarePath
        {
            get
            {
                return Program.GetTempFolder() + Path.DirectorySeparatorChar + "tempfw.bin";
            }
        }
        private string TemporaryDownloadPath
        {
            get
            {
                return Program.GetTempFolder() + Path.DirectorySeparatorChar + "tempdl.bin";
            }
        }

        public Form1()
        {
            InitializeComponent();

            this.bkgndFirmwareListReader.RunWorkerAsync();

            this.dropDithering.SelectedIndex = 0;
            dithererList = new List<IColorDitherer>
            {
                null,
                null,
                new BayerDitherer4(),
                new BayerDitherer8(),
                new ClusteredDotDitherer(),
                new DotHalfToneDitherer(),
                null,
                new FanDitherer(),
                new ShiauDitherer(),
                new SierraDitherer(),
                new StuckiDitherer(),
                new BurkesDitherer(),
                new AtkinsonDithering(),
                new TwoRowSierraDitherer(),
                new FloydSteinbergDitherer(),
                new JarvisJudiceNinkeDitherer()
            };
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All files (*.*)|*.*|Bitmap files (*.bmp)|*.bmp|Portable Network Graphics files (*.png)|*.png|JPEG files (*.jpg)|*.jpg|GIF files (*.gif)|*.gif";
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            sourceImage = Image.FromFile(ofd.FileName);
            RenderBitmap(sourceImage);
        }

        private void RenderBitmap()
        {
            RenderBitmap(this.sourceImage);
        }

        private void RenderBitmap(Image srcImg)
        {
            if (srcImg == null)
            {
                return;
            }

            int decideSize = (800 + 480) / 2;

            Bitmap resized = null;
            Graphics g = null;

            if (srcImg.Width >= decideSize)
            {
                resized = new Bitmap(800, 480);
                if (srcImg.Width != 800 || srcImg.Height != 480)
                {
                    lblImageSize.Text = string.Format("{0}x{1} stretched to 800x480", srcImg.Width, srcImg.Height);
                }
                else
                {
                    lblImageSize.Text = string.Format("800x480");

                }
            }
            else
            {
                resized = new Bitmap(480, 272);
                if (srcImg.Width != 480 || srcImg.Height != 272)
                {
                    lblImageSize.Text = string.Format("{0}x{1} stretched to 480x272", srcImg.Width, srcImg.Height);
                }
                else
                {
                    lblImageSize.Text = string.Format("480x272");

                }
            }

            g = Graphics.FromImage(resized);

            g.FillRectangle(new SolidBrush(Color.Black), new RectangleF(0, 0, resized.Width, resized.Height));
            g.DrawImage(srcImg, 0, 0, resized.Width, resized.Height);

            // this function was taken from https://www.codeproject.com/Articles/66341/A-Simple-Yet-Quite-Powerful-Palette-Quantizer-in-C
            Image dithered = ImageBuffer.QuantizeImage(resized, new UniformQuantizer(), dithererList[dropDithering.SelectedIndex], 256, 1);

            Bitmap fromDithered = new Bitmap(dithered);
            picBox.Image = dithered;

            if (dithered.PixelFormat != PixelFormat.Format8bppIndexed)
            {
                MessageBox.Show("Error: processed image format cannot be converted to 8 bit");
                resultImage = null;
                //lblImageFileSize.Text = "";
                return;
            }

            dithered.Save(this.TemporaryBitmapPath, ImageFormat.Bmp);
            resultImage = dithered;

            TryGetBinarySize();
        }

        private void TryGetBinarySize()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(Program.bmp2cPath, string.Format("\"{0}\" \"{1}\" -b -c", this.TemporaryBitmapPath, this.TemporaryLogoPath));
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                TryCloseProc();
                proc = new Process();
                proc.StartInfo = psi;
                proc.Start();
                proc.WaitForExit(60000);
                FileInfo nfo = new FileInfo(this.TemporaryLogoPath);
                lblImageFileSize.Text = string.Format("Splash File Size: {0} bytes", nfo.Length);
                if (nfo.Length > (64000 * 3))
                {
                    lblImageFileSize.Text += "!!!";
                }
            }
            catch
            {
                lblImageFileSize.Text = "";
            }
        }

        private void TryCloseProc()
        {
            try
            {
                if (proc != null)
                {
                    proc.Kill();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dropDithering_SelectedIndexChanged(object sender, EventArgs e)
        {
            RenderBitmap();
        }

        private void btnBrowseFirmwareBinary_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All files (*.*)|*.*|Binary files (*.bin)|*.bin";
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            txtFirmwareBinaryPath.Text = ofd.FileName;
        }

        private void btnDownloadLatest_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/dc42/PanelDueFirmware/releases");
        }

        private bool Precheck(bool mustUseFile)
        {
            if (resultImage == null)
            {
                MessageBox.Show("No resulting image was created. Please try processing an image.", "Error");
                return false;
            }
            if (File.Exists(this.TemporaryBitmapPath) == false)
            {
                MessageBox.Show("No resulting image file was created. Please try processing an image.", "Error");
                return false;
            }
            if (mustUseFile)
            {
                try
                {
                    if (File.Exists(txtFirmwareBinaryPath.Text) == false)
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    MessageBox.Show("No firmware binary file was found. Please select a firmware binary file (the nologo version).", "Error");
                    return false;
                }
            }
            if (File.Exists(Program.bmp2cPath) == false)
            {
                MessageBox.Show("Internal error, BMP2C is missing.", "Error");
                return false;
            }
            return true;
        }

        private bool SaveResultingFirmware()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";

            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            txtFinalFirmwarePath.Text = sfd.FileName;

            if (MakeFinalBinary(txtFirmwareBinaryPath.Text) == false)
            {
                return false;
            }

            File.Copy(this.TemporaryFirmwarePath, sfd.FileName, true);

            return true;
        }

        private bool RunBossac(string path)
        {
            TryCloseProc();
            Stopwatch sw = Stopwatch.StartNew();
            bool silent = true;
            do
            {
                ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", (silent ? "/C" : "/K")  + string.Format(" {0} -p{1} -e -w -v --boot=1 --bod=0 --bor=0 \"{2}\"", Program.bossacPath.Replace(" ", "^ "), comPort, path));

                psi.CreateNoWindow = false;
                psi.WindowStyle = ProcessWindowStyle.Normal;

                psi.UseShellExecute = false;
                psi.WorkingDirectory = Path.GetDirectoryName(Program.bossacPath);

                proc = new Process();
                proc.StartInfo = psi;
                proc.Start();
                if (silent)
                {
                    proc.WaitForExit(60 * 4 * 1000);
                    sw.Stop();
                    if (sw.ElapsedMilliseconds < 2000)
                    {
                        silent = false;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            while (true);
            return true;
        }

        private bool GetComPort()
        {
            if (string.IsNullOrWhiteSpace(comPort))
            {
                var list = Program.ComPortNames();
                if (list.Count <= 0)
                {
                    ComPortPicker cpp = new ComPortPicker();
                    if (cpp.IsEmpty)
                    {
                        MessageBox.Show("No COM port available", "Error");
                        return false;
                    }
                    else
                    {
                        cpp.ShowDialog();
                        if (cpp.IsConfirmed)
                        {
                            comPort = cpp.ComPort;
                            if (string.IsNullOrWhiteSpace(comPort) == false)
                            {
                                lblSerialPort.Text = comPort;
                            }
                            else
                            {
                                MessageBox.Show("No COM port selected", "Error");
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    comPort = list[0];
                    lblSerialPort.Text = comPort;
                }
            }
            return true;
        }

        private bool MakeFinalBinary(string srcPath)
        {
            ProcessStartInfo psi = new ProcessStartInfo(Program.bmp2cPath, string.Format("\"{0}\" \"{1}\" -b -c", this.TemporaryBitmapPath, this.TemporaryLogoPath));
            psi.UseShellExecute = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            proc = new Process();
            proc.StartInfo = psi;
            proc.Start();
            proc.WaitForExit(60000);

            List<byte> allData = new List<byte>();
            allData.AddRange(File.ReadAllBytes(srcPath));
            allData.AddRange(File.ReadAllBytes(this.TemporaryLogoPath));

            if (allData.Count >= 256000)
            {
                MessageBox.Show("File size is greater than 256kb!", "Error");
                return false;
            }
            else if (allData.Count >= 128000 && resultImage.Width == 480)
            {
                MessageBox.Show("File size is greater than 128kb!", "Error");
                return false;
            }
            File.WriteAllBytes(this.TemporaryFirmwarePath, allData.ToArray());

            return true;
        }

        private void btnSaveResultingFirmware_Click(object sender, EventArgs e)
        {
            if (Precheck(true) == false)
            {
                return;
            }

            SaveResultingFirmware();
        }

        private void btnBrowseFinalFirmware_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";

            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            txtFinalFirmwarePath.Text = ofd.FileName;
        }

        private void btnFlashMe_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(txtFinalFirmwarePath.Text) == false)
                {
                    throw new Exception();
                }
            }
            catch
            {
                if (Precheck(true) == false)
                {
                    return;
                }
            }

            try
            {
                if (File.Exists(txtFinalFirmwarePath.Text) == false)
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Please save the resulting binary file somewhere first.");
                if (SaveResultingFirmware() == false)
                {
                    return;
                }
            }

            try
            {
                if (File.Exists(txtFinalFirmwarePath.Text) == false)
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Unable to read the firmware file", "Error");
                return;
            }

            if (File.Exists(Program.bossacPath) == false)
            {
                MessageBox.Show("Internal error, BOSSAC is missing.", "Error");
                return;
            }

            if (GetComPort() == false)
            {
                return;
            }

            RunBossac(txtFinalFirmwarePath.Text);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var list = Program.ComPortNames();
            if (list.Count > 0)
            {
                comPort = list[0];
                lblSerialPort.Text = comPort;
            }
            else
            {
                lblSerialPort.Text = "None";
            }
        }

        private void btnFlashMeFromWeb_Click(object sender, EventArgs e)
        {
            if (Precheck(false) == false)
            {
                return;
            }
            if (File.Exists(Program.bossacPath) == false)
            {
                MessageBox.Show("Internal error, BOSSAC is missing.", "Error");
                return;
            }
            
            try
            {
                if (File.Exists(this.TemporaryDownloadPath))
                {
                    File.Delete(this.TemporaryDownloadPath);
                    Thread.Sleep(500);
                }
            }
            catch
            {
                MessageBox.Show("Internal error: unable to create temporary file for download", "Error");
                return;
            }

            try
            {
                string drop = dropWebVersion.SelectedItem.ToString();
                string url = "";
                if (webFirmwareList.ContainsKey(drop))
                {
                    url = webFirmwareList[drop];
                }
                else
                {
                    MessageBox.Show("Internal error: unable to find URL for dropdown item", "Error");
                    return;
                }

                if (drop.Contains("4.3"))
                {
                    if (resultImage.Width != 480)
                    {
                        MessageBox.Show("Wrong image size for hardware screen size", "Error");
                        return;
                    }
                }
                else
                {
                    if (resultImage.Width != 800)
                    {
                        MessageBox.Show("Wrong image size for hardware screen size", "Error");
                        return;
                    }
                }

                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(url, this.TemporaryDownloadPath);
                }
            }
            catch
            {
                MessageBox.Show("Internal error: unable to download binary firmware", "Error");
                return;
            }

            if (File.Exists(this.TemporaryDownloadPath) == false)
            {
                MessageBox.Show("Internal error: failed to download binary firmware", "Error");
                return;
            }

            if (MakeFinalBinary(this.TemporaryDownloadPath) == false)
            {
                return;
            }

            if (GetComPort() == false)
            {
                return;
            }

            RunBossac(this.TemporaryDownloadPath);
        }

        private void bkgndFirmwareListReader_DoWork(object sender, DoWorkEventArgs e)
        {
            string html = "";
            try
            {
                using (WebClient client = new WebClient())
                {
                    html = client.DownloadString("https://github.com/dc42/PanelDueFirmware/releases");
                }
            }
            catch
            {
                return;
            }

            string re1 = "(<)"; // Any Single Character 1
            string re2 = "(a)"; // Any Single Character 2
            string re3 = "(\\s+)";  // White Space 1
            string re4 = "(href)";  // Variable Name 1
            string re5 = "(=)"; // Any Single Character 3
            string re6 = "(\")";    // Any Single Character 4
            string re7 = "(\\/)";   // Any Single Character 5
            string re8 = "(dc42)";  // Variable Name 2
            string re9 = "(\\/)";   // Any Single Character 6
            string re10 = "(PanelDueFirmware)"; // Variable Name 3
            string re11 = "(\\/)";  // Any Single Character 7
            string re12 = "(releases)"; // Variable Name 4
            string re13 = "(\\/)";  // Any Single Character 8
            string re14 = "(download)"; // Variable Name 5
            string re15 = "(\\/)";  // Any Single Character 9
            string re16 = "([0-9.]+)";    // Non-greedy match on filler
            string re17 = "(\\/)";  // Any Single Character 10
            string re18 = "(PanelDue-)"; // Variable Name 6
            string re20 = "([0-9vi.-]+)";    // Non-greedy match on filler
            string re24 = "(-nologo\\.bin)"; // File Name 1
            string re25 = "(\")";	// Any Single Character 13

            string rt = re1 + re2 + re3 + re4 + re5 + re6 + re7 + re8 + re9 + re10 + re11 + re12 + re13 + re14 + re15 + re16 + re17 + re18 + re20 + re24 + re25;

            Regex r = new Regex(rt, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(html);
            while (m.Success)
            {
                string fw = m.Groups[16].ToString();
                string hw = m.Groups[19].ToString();

                if (fw.Contains("/") == false && hw.Contains(".bin") == false)
                {
                    string url = string.Format("https://github.com/dc42/PanelDueFirmware/releases/download/{0}/PanelDue-{1}-nologo.bin", fw, hw);
                    string name = string.Format("FW v{0} - HW {1}", fw, hw);

                    if (webFirmwareList.ContainsKey(name) == false)
                    {
                        webFirmwareList.Add(name, url);
                    }
                }

                m = m.NextMatch();
            }
        }

        private void bkgndFirmwareListReader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (webFirmwareList.Count <= 0)
            {
                btnFlashMeFromWeb.Enabled = false;
                dropWebVersion.Items.Clear();
                dropWebVersion.Items.Add("Failed to retrieve list from web");
                dropWebVersion.SelectedIndex = 0;
                dropWebVersion.Enabled = false;
            }
            else
            {
                btnFlashMeFromWeb.Enabled = true;
                dropWebVersion.Items.AddRange(webFirmwareList.Keys.ToArray());
                dropWebVersion.SelectedIndex = 0;
            }
        }
    }
}
