using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Blockchain_Wahlclient
{
    public partial class AlternativeVoting : Form
    {

        private CandidateList cl;
        private Backend backend;
        
        public AlternativeVoting(Backend backend)
        {


            InitializeComponent();

            this.backend = backend;
            this.cl = new CandidateList(this.backend);
            
        }

        public void SetCandList(CandidateList cl)
        {
            this.cl = cl;
        }

        private void AlternativeVoting_Load(object sender, EventArgs e)
        {
            cl.BuildFrontend(ref this.flowLayoutPanel1);
        }

        private void submitVote_Click(object sender, EventArgs e)
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

            backend.SendVoteAlternative(this.textBox1.Text, cl.GetCandidates());
        }
    }
}
