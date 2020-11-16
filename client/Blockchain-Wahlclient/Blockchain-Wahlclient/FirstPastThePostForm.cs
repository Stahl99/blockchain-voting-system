using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Blockchain_Wahlclient
{
    public partial class FirstPastThePostForm : Form
    {
        public FirstPastThePostModel Model { get; set; } = new FirstPastThePostModel();

        public FirstPastThePostForm()
        {
            InitializeComponent();
        }

        public void SetCandidateList(FirstPastThePostModel model)
        {
            this.Model = model;
        }

        public void ShowErrorText(string text)
        {
            ErrorLabel.Text = text;
            ErrorLabel.Visible = !ErrorLabel.Visible;
        }

        public void HideErrorText()
        {
            ErrorLabel.Text = "";
            ErrorLabel.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HideErrorText();
            Model.VerifyVote(textBox1.Text);
        }

        private void FirstPastThePostForm_Load(object sender, EventArgs e)
        {
            Model.BuildCandidateList(ref this.flowLayoutPanel1);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
