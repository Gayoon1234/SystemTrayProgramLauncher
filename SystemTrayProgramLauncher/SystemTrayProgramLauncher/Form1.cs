using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SystemTrayProgramLauncher.CustomFileReader;
using SystemTrayProgramLauncher.Utilities;

namespace SystemTrayProgramLauncher
{
    public partial class main : Form
    {
        private readonly FileReader fr;
        private readonly PowerShellIntegration psi;
        private Dictionary<string, string> contextMenuItems;
        public main()
        {
            InitializeComponent();
            fr = new FileReader();
            psi = new PowerShellIntegration();
            refreshMenu();
        }

        // Loads the application within the System Tray
        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            notifyIcon.Visible = true;
        }

        // When the user double clicks on the system tray icon, open form
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyIcon.Visible = false;
        }

        // When the user right clicks, show context menu
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
            psi.displaySwitchExtend();
        }

        private void extend2SingleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            psi.displaySwitchSingle();
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
           psi.runPowershellFromPath(path);
        }

        public void removeNonDefaultOptions() {
            var length = contextMenuStrip1.Items.Count;
            for (var i = length - 1; i > 2; i--)
            {
                contextMenuStrip1.Items.RemoveAt(i);
            }
        }
        private void refreshMenu() {

            removeNonDefaultOptions();

            var contextMenuItems = fr.ReadEnvFile();
            foreach (var kvp in contextMenuItems)
            {
                var title = kvp.Key;
                var path = kvp.Value;
                if (title == "Seperator" && path == "Seperator") {
                    ToolStripItem seperator = new ToolStripSeparator();
                    contextMenuStrip1.Items.Add(seperator);
                }
                else if (title[0] == '!') {
                    ToolStripMenuItem parentItem = (ToolStripMenuItem)contextMenuStrip1.Items[contextMenuStrip1.Items.Count - 1];
                    ToolStripMenuItem newItem = new ToolStripMenuItem($"{title[1..]}");
                    newItem.Click += (s, ev) => default_MenuItem_Click(s, ev, path);
                    parentItem.DropDownItems.Add(newItem);
                }
                else
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