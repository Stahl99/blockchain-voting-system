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

        private async void button1_Click(object sender, EventArgs e)
        {
            HideErrorText();

            // Verify the entered Information
            if(Model.VerifyVote(textBox1.Text))
            {   
                if(await Model.SendVote(textBox1.Text))
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

            }
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
