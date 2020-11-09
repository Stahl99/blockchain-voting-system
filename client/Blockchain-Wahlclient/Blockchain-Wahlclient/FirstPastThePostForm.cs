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
        private FirstPastThePostModel model;

        public FirstPastThePostForm()
        {
            InitializeComponent();
            model = new FirstPastThePostModel();
        }

        private void VoteButton_Click(object sender, EventArgs e)
        {
            HideErrorText();
            model.VerifyVote(textBox1.Text, checkedListBox1.CheckedItems);
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
    }
}
