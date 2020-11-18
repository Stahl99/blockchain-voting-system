using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blockchain_Wahlclient
{
    public partial class Form1 : Form
    {
        private Backend backend;
        public Form1()
        {
            InitializeComponent();
        }
        private void submitButton_Click(object sender, EventArgs e)
        {
            backend.InitService(this.blockchainUrlTB.Text, this.electionAdressTB.Text);

            // Change bool to electionType enum
            bool electionType = backend.GetElectionType();
            if(electionType)
            {
                this.Hide();
                var FPTPform = new FirstPastThePostForm();
                FPTPform.Show();
                this.Close();
            }
        }
    }
}
