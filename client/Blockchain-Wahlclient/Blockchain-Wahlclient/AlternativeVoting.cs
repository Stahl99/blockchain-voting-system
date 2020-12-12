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
    /// The Form to display Alternative Voting elections
    /// </summary>
    public partial class AlternativeVoting : Form
    {

        private CandidateList cl;
        private Backend backend;
        
        /// <summary>
        /// Constructor sets backend and creates new <c>CandidateList</c>
        /// </summary>
        /// <param name="backend">The backend instance to give to the CandidateList</param>
        public AlternativeVoting(Backend backend)
        {


            InitializeComponent();

            this.backend = backend;
            this.cl = new CandidateList(this.backend);
            
        }

        /// <summary>
        /// On Load function that displays the candidates on the frontend
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AlternativeVoting_Load(object sender, EventArgs e)
        {
            cl.BuildFrontend(ref this.flowLayoutPanel1);
        }

        /// <summary>
        /// Handles Clicks on the vote button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void submitVote_Click(object sender, EventArgs e)
        {
            cl.SecureRanks();

            if (!cl.CheckRanks())
            {
                MessageBox.Show("Ranking not correct!");
                return;
            }

            if(!cl.OnlyHexInString(this.textBox1.Text))
            {
                MessageBox.Show("Wrong adress format");
                return;
            }


            try
            {
                if (await backend.SendVoteAlternativeAsync(textBox1.Text, cl.GetCandidates()))
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
                MessageBox.Show("Already voted from this adress");
            }
        }
    }
}
