using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Management;

using LogUtility;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace PanelDueSplashMaker
{
    static class Program
    {
        static string exePath;
        public static string bmp2cPath;
        public static string bossacPath;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process proc = Process.GetCurrentProcess();
            exePath = proc.Modules[0].FileName;
            exePath = Path.GetDirectoryName(exePath).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            ExtractFiles();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static void ExtractFilesThread()
        {
            string dir = exePath + Path.DirectorySeparatorChar;
            bmp2cPath = dir + "bmp2c-escher3d.exe";
            bossacPath = dir + "bossac.exe";

            if (File.Exists(bmp2cPath) == false)
            {
                File.WriteAllBytes(bmp2cPath, ExecutableFiles.bmp2c_escher3d);
            }

            if (File.Exists(bossacPath) == false)
            {
                File.WriteAllBytes(bossacPath, ExecutableFiles.bossac);
            }
        }

        static void ExtractFiles()
        {
            ThreadStart ts = new ThreadStart(ExtractFilesThread);
            Thread t = new Thread(ts);
            t.Start();
        }

        static public string GetTempFolder()
        {
            string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), System.Reflection.Assembly.GetExecutingAssembly().GetName().Name).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            if (Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
                Thread.Sleep(500);
            }
            return dir;
        }

        // function from https://stackoverflow.com/questions/10350340/identify-com-port-using-vid-and-pid-for-usb-device-attached-to-x64
        static public List<string> ComPortNames()
        {
            string VID = "03EB";
            string PID = "6124";
            // IDs from https://github.com/atmelcorp/sam-ba/blob/master/dist/driver/atm6124_cdc.inf

            String pattern = String.Format("^VID_{0}.PID_{1}", VID, PID);
            Regex _rx = new Regex(pattern, RegexOptions.IgnoreCase);
            List<string> comports = new List<string>();

            RegistryKey rk1 = Registry.LocalMachine;
            RegistryKey rk2 = rk1.OpenSubKey("SYSTEM\\CurrentControlSet\\Enum");

            foreach (String s3 in rk2.GetSubKeyNames())
            {

                RegistryKey rk3 = rk2.OpenSubKey(s3);
                foreach (String s in rk3.GetSubKeyNames())
                {
                    if (_rx.Match(s).Success)
                    {
                        RegistryKey rk4 = rk3.OpenSubKey(s);
                        foreach (String s2 in rk4.GetSubKeyNames())
                        {
                            RegistryKey rk5 = rk4.OpenSubKey(s2);
                            string location = (string)rk5.GetValue("LocationInformation");
                            if (!String.IsNullOrEmpty(location))
                            {
                                string port = location.Substring(location.IndexOf('#') + 1, 4).TrimStart('0');
                                if (!String.IsNullOrEmpty(port)) comports.Add(String.Format("COM{0:####}", port));
                            }
                            //RegistryKey rk6 = rk5.OpenSubKey("Device Parameters");
                            //comports.Add((string)rk6.GetValue("PortName"));
                        }
                    }
                }
            }

            // this trick below is from https://stackoverflow.com/questions/2837985/getting-serial-port-information/2876126
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM WIN32_SerialPort"))
            {
                string[] portnames = SerialPort.GetPortNames();
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList();
                var tList = (from n in portnames
                             join p in ports on n equals p["DeviceID"].ToString()
                             select n + " - " + p["Caption"]).ToList();

                foreach (string i in tList)
                {
                    if (i.ToLowerInvariant().Contains("bossa"))
                    {
                        string n = i.Substring(0, i.IndexOf(' '));
                        if (comports.Contains(n) == false)
                        {
                            comports.Add(n);
                        }
                    }
                }
            }

            // the two methods from above might result in entries that are old and not currently connectable
            // so find what's actually connected right now first
            List<string> availablePorts = new List<string>(SerialPort.GetPortNames());

            // finally make a list of what is actually available and also Bossa
            List<string> finalList = new List<string>();
            foreach (string i in availablePorts)
            {
                if (comports.Contains(i) && finalList.Contains(i) == false)
                {
                    finalList.Add(i);
                }
            }

            return finalList;
        }
    }
}
