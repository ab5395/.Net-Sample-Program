using Coypu;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;

namespace EventApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        //MoveWindow changes window's top, left, width and height...
        //...even if it do not want to change their dimensions and haves fixed edges!
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        // Disables or enables window!!!
        [DllImport("user32.dll")]
        static extern bool EnableWindow(IntPtr hWnd, bool bEnable);



        [DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(int xPoint, int yPoint);

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var hWnd = WindowFromPoint(100, 100); // X, Y
            SetForegroundWindow(hWnd);

            //SendKeys.Send(Keys.PageUp.ToString());
            var random = new Random();

            while (true)
            {
                var tabKey = string.Empty;
                for (var i = 0; i < 2; i++)
                {
                    tabKey += "{TAB}";
                }
                var ctrlTab = "^(" + tabKey + ")";
                SendKeys.Send(ctrlTab);
                //Thread.Sleep(60000);
                SendKeys.Send("{END}");
                for (var i = 0; i < random.Next(0, 10); i++)
                {
                    SendKeys.Send("{PGUP}");
                    ArrawKey();
                    Thread.Sleep(60000);
                }
                SendKeys.Send("{HOME}");
                for (var i = 0; i < random.Next(0, 10); i++)
                {
                    SendKeys.Send("{PGDN}");
                    ArrawKey();
                    Thread.Sleep(60000);
                }
                Thread.Sleep(120000);
            }
        }



        public void ArrawKey()
        {
            SendKeys.Send("{RIGHT}");
            SendKeys.Send("{LEFT}");
            SendKeys.Send("{UP}");
            SendKeys.Send("{DOWN}");
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }
    }
}
