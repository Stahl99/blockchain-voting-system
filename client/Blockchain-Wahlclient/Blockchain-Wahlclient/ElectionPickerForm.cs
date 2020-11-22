using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Blockchain_Wahlclient
{
    public partial class ElectionPickerForm : Form
    {
        ElectionPickerModel model;

        public ElectionPickerForm(Backend backend)
        {
            InitializeComponent();

            this.model = new ElectionPickerModel();
            this.model.Backend = backend;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (model.ValidatePick())
            {
                this.Hide();
                // switch on election type
            }
        }

        private void ElectionPickerForm_Load(object sender, EventArgs e)
        {
            model.LoadElections(ref this.flowLayoutPanel1);
        }
    }
}
