using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemTrayProgramLauncher.Utilities
{
    internal class PowerShellIntegration
    {
        private void displaySwitchCommand(string command){
            string baseDirectory = AppContext.BaseDirectory;
            string displayswitchPath = Path.Combine(baseDirectory, "Scripts", "DisplaySwitch.exe");
            Process.Start(displayswitchPath, command);
        }

        public void displaySwitchExtend() {
            displaySwitchCommand("/extend");
        }

        public void displaySwitchSingle() {
            displaySwitchCommand("/internal");
        }

        public void runPowershellFromPath(string path) {
            ProcessStartInfo psi = new()
            {
                FileName = "powershell.exe",
                Arguments = $"-File \"{path}\"",
                CreateNoWindow = true,
                UseShellExecute = false,
            };
            Process.Start(psi);
        } 
    }
}
