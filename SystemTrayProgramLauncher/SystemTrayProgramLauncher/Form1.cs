using System.Diagnostics;
using System.Windows.Forms;

namespace SystemTrayProgramLauncher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            notifyIcon.Visible = true;
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyIcon.Visible = false;
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        //FYI, DisplaySwitch.exe is included in the bin file as MS messed up the WIN11 implementation of it.
        private void single2ExtendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string baseDirectory = AppContext.BaseDirectory;
            string displayswitchPath = Path.Combine(baseDirectory, "Scripts", "DisplaySwitch.exe");
            Process.Start(displayswitchPath, "/extend");
        }

        private void extend2SingleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string baseDirectory = AppContext.BaseDirectory;
            string displayswitchPath = Path.Combine(baseDirectory, "Scripts", "DisplaySwitch.exe");
            Process.Start(displayswitchPath, "/internal");
        }

        //This will only work on my PC.
        private void oneDriveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string scriptPath = @"C:\Users\gayan\OneDrive - RMIT University\2023\Notes\word2pdf.ps1";
            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = $"-File \"{scriptPath}\"",
                CreateNoWindow = true,
                UseShellExecute = false,
            };
            Process.Start(psi);
        }


        //Code for later

        //Basically the plan is to allow users to create custom Context menu items/

        //ToolStripMenuItem newItem = new ToolStripMenuItem("New Menu Item");
        //newItem.Click += new EventHandler(newItem_Click);
        //contextMenuStrip1.Items.Add(newItem);
    }
}