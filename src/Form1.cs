using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
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

        public Form1()
        {
            InitializeComponent();

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
                proc = new Process();
                proc.StartInfo = psi;
                proc.Start();
                proc.WaitForExit(60000);
                FileInfo nfo = new FileInfo(this.TemporaryLogoPath);
                lblImageFileSize.Text = string.Format("Splash File Size: {0} bytes", nfo.Length);
            }
            catch
            {
                lblImageFileSize.Text = "";
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

        private bool Precheck()
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

            ProcessStartInfo psi = new ProcessStartInfo(Program.bmp2cPath, string.Format("\"{0}\" \"{1}\" -b -c", this.TemporaryBitmapPath, this.TemporaryLogoPath));
            proc = new Process();
            proc.StartInfo = psi;
            proc.Start();
            proc.WaitForExit(60000);

            List<byte> allData = new List<byte>();
            allData.AddRange(File.ReadAllBytes(txtFirmwareBinaryPath.Text));
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

            File.Copy(this.TemporaryFirmwarePath, sfd.FileName, true);

            return true;
        }

        private void btnSaveResultingFirmware_Click(object sender, EventArgs e)
        {
            if (Precheck() == false)
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
                if (Precheck() == false)
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

            if (string.IsNullOrWhiteSpace(comPort))
            {
                var list = Program.ComPortNames();
                if (list.Count <= 0)
                {
                    MessageBox.Show("COM port not found. Have you connected to the PanelDue, and reset it while holding the ERASE button?", "Error");
                    return;
                }
                comPort = list[0];
                lblSerialPort.Text = comPort;
            }

            if (string.IsNullOrWhiteSpace(comPort))
            {
                MessageBox.Show("No COM ports are found!", "Error");
                return;
            }

            ProcessStartInfo psi = new ProcessStartInfo(Program.bossacPath, string.Format("-p{0} -e -w -v --boot=1 --bod=0 --bor=0 \"{1}\"", comPort, txtFinalFirmwarePath.Text, this.TemporaryLogoPath));
            proc = new Process();
            proc.StartInfo = psi;
            proc.Start();
            proc.WaitForExit();
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
    }
}
