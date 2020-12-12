using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blockchain_Wahlclient
{
    /// <summary>
    /// Form to display the FirstPastThePost voting screen
    /// </summary>
    public partial class FirstPastThePostForm : Form
    {
        public FirstPastThePostModel Model { get; set; }
        private Backend backend;

        public FirstPastThePostForm(Backend backend)
        {
            InitializeComponent();
            this.Model = new FirstPastThePostModel(backend);
            this.backend = backend;
        }

        /// <summary>
        /// Displays an Error on the ErrorLabel
        /// </summary>
        /// <param name="text">The error text to display</param>
        public void ShowErrorText(string text)
        {
            ErrorLabel.Text = text;
            ErrorLabel.Visible = !ErrorLabel.Visible;
        }

        /// <summary>
        /// Hides the Error Text
        /// </summary>
        public void HideErrorText()
        {
            ErrorLabel.Text = "";
            ErrorLabel.Visible = false;
        }

        /// <summary>
        /// Handles click on the vote button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {
            HideErrorText();

            // Verify the entered Information
            if(Model.VerifyVote(textBox1.Text))
            {
                try
                {
                    if (await Model.SendVote(textBox1.Text))
                    {
                        // Vote successfull redirect to ElectionPicker
                        MessageBox.Show("Vote sucessfull");
                        this.Hide();
                        var electionPickerForm = new ElectionPickerForm(this.backend);
                        electionPickerForm.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("There was an error with your Vote. Please check your address and try again");
                    }
                } catch
                {
                    MessageBox.Show("Already voted from this address");
                }

            }
        }

        /// <summary>
        /// On Load function  to load and display the candidate list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FirstPastThePostForm_Load(object sender, EventArgs e)
        {
            Model.BuildCandidateList(ref this.flowLayoutPanel1);
        }
    }
}
