using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SystemTrayProgramLauncher.CustomFileReader;

namespace SystemTrayProgramLauncher
{
    public partial class main : Form
    {
        private readonly FileReader fr;
        public main()
        {
            InitializeComponent();
            fr = new FileReader();
            refreshMenu();
        }

        // Loads the application within the System Tray
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            notifyIcon.Visible = true;
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refreshMenu();
        }
        private void default_MenuItem_Click(object sender, EventArgs e, string path)
        {
            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = $"-File \"{path}\"",
                CreateNoWindow = true,
                UseShellExecute = false,
            };
            Process.Start(psi);
        }
        private void refreshMenu() {
            var paths = fr.ReadEnvFile();
            foreach (var kvp in paths)
            {
                var title = kvp.Key;
                var path = kvp.Value;
                if (title == "Seperator" && path == "Seperator") {
                    ToolStripItem seperator = new ToolStripSeparator();
                    contextMenuStrip1.Items.Add(seperator);
                }
                else
                {
                    bool itemExists = false;
                    foreach (ToolStripItem item in contextMenuStrip1.Items)
                    {
                        if (item.Text == title)
                        {
                            // Item already exists in contextMenuStrip1.Items
                            itemExists = true;
                            break;
                        }
                    }

                    if (!itemExists)
                    {
                        // Create new ToolStripMenuItem and add to contextMenuStrip1.Items
                        ToolStripMenuItem newItem = new ToolStripMenuItem($"{title}");
                        newItem.Click += (s, ev) => default_MenuItem_Click(s, ev, path);
                        contextMenuStrip1.Items.Add(newItem);
                    }
                }
            }
        }

       
    }
}