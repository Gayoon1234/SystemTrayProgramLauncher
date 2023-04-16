using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemTrayProgramLauncher.CustomFileReader
{
    internal class FileReader
    {
        public Dictionary<string, string> ReadEnvFile()
        {
          var envPath =Path.Combine(AppContext.BaseDirectory, "environment", "env.txt");

          var envData = new Dictionary<string, string>();

            if (File.Exists(envPath))
            {
                var lines = File.ReadAllLines(envPath);
                foreach (var line in lines)
                {
                    var parts = line.Split(new[] { "###" }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 2)
                    {
                        envData[parts[0]] = parts[1];
                    }
                    else if(parts.Length == 1) { 
                        envData[parts[0]] = String.Empty;
                    }
                }
            }

            return envData;
        }

    }
}
