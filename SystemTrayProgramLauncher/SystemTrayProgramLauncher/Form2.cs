using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemTrayProgramLauncher
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private int selection;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gbAdd.Visible = true;
            if (cbSelection.Text == "Seperator")
            {
                gbAdd.Visible = false;
                selection = 0;
            }
            else if (cbSelection.Text == "Label")
            {
                lblPath.Visible = false;
                tbPath.Visible = false;
                selection = 1;
            }
            else if (cbSelection.Text == "Link") 
            {
                lblPath.Visible = true;
                tbPath.Visible = true;
                selection = 2;
            }
                
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbSelection.Text)){
                MessageBox.Show("Type not Selected");
                return;
            }

            string textToAdd = "";
            if (selection == 1)
            {
                textToAdd = "Seperator###Seperator";
            }
            else if (selection == 2 || selection == 3)
            {
                textToAdd = tbTitle.Text + "###";
            }
            else if (selection == 3) {
                textToAdd += tbPath.Text;
            }

            //This should really go in its own class/method but eh
            var envPath = Path.Combine(AppContext.BaseDirectory, "environment", "env.txt");
            using (FileStream fs = new FileStream(envPath, FileMode.Append, FileAccess.Write))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(textToAdd);
                fs.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
