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
    /// <summary>
    /// Start Form
    /// </summary>
    public partial class Form1 : Form
    {
        private Backend backend = new Backend();
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Handles Clicks on the submit button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submitButton_Click(object sender, EventArgs e)
        {
            if(backend.InitService(this.blockchainUrlTB.Text, this.electionAdressTB.Text))
            {
                this.Hide();
                var electionPickerForm = new ElectionPickerForm(this.backend);
                electionPickerForm.ShowDialog();
                this.Close();
            }
        }

    }
}
