using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Management;

namespace PanelDueSplashMaker
{
    public partial class ComPortPicker : Form
    {
        public bool IsConfirmed
        {
            get;
            private set;
        }

        public bool IsEmpty
        {
            get
            {
                return dropComPorts.Items.Count <= 0;
            }
        }

        public string ComPort
        {
            get
            {
                string v = "";
                if (dropComPorts.SelectedIndex >= 0)
                {
                    v = (string)dropComPorts.Items[dropComPorts.SelectedIndex];
                    if (v.Contains(' '))
                    {
                        v = v.Substring(0, v.IndexOf(' '));
                    }
                }
                return v;
            }
        }

        private List<string> prevList = new List<string>();

        public ComPortPicker()
        {
            InitializeComponent();
            FillList();
        }

        private void FillList()
        {
            string prevSelection = "";

            if (dropComPorts.SelectedIndex >= 0)
            {
                prevSelection = (string)dropComPorts.Items[dropComPorts.SelectedIndex];
            }

            List<string> comports = new List<string>();

            // this trick below is from https://stackoverflow.com/questions/2837985/getting-serial-port-information/2876126
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM WIN32_SerialPort"))
            {
                string[] portnames = SerialPort.GetPortNames();
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList();
                var tList = (from n in portnames
                             join p in ports on n equals p["DeviceID"].ToString()
                             select n + " - " + p["Caption"]).ToList();
                comports.AddRange(tList);
            }

            bool needReload = false;
            if (prevList.Count != comports.Count)
            {
                needReload = true;
            }
            else
            {
                for (int i = 0; i < comports.Count; i++)
                {
                    if (comports[i] != prevList[i])
                    {
                        needReload = true;
                        break;
                    }
                }
            }
            prevList = comports;

            if (needReload)
            {
                dropComPorts.Items.Clear();
                for (int i = 0; i < comports.Count; i++)
                {
                    int j = dropComPorts.Items.Add(comports[i]);
                    if (comports[i].ToLowerInvariant() == prevSelection.ToLowerInvariant())
                    {
                        dropComPorts.SelectedIndex = j;
                    }
                }
                if (dropComPorts.Items.Count > 0 && dropComPorts.SelectedIndex < 0)
                {
                    dropComPorts.SelectedIndex = 0;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FillList();
        }

        private void ComPortPicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                if (btnCancel.Focused == false)
                {
                    e.Handled = true;
                    IsConfirmed = true;
                    this.Close();
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            IsConfirmed = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            IsConfirmed = false;
            this.Close();
        }

        private void dropComPorts_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                if (btnCancel.Focused == false)
                {
                    e.Handled = true;
                    IsConfirmed = true;
                    this.Close();
                }
            }
        }

        private void ComPortPicker_Load(object sender, EventArgs e)
        {
            if (dropComPorts.Items.Count == 1)
            {
                this.IsConfirmed = true;
                this.Close();
            }
            else if (dropComPorts.Items.Count == 0)
            {
                this.IsConfirmed = false;
                this.Close();
            }
        }
    }
}
