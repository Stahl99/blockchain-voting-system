using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Blockchain_Wahlclient
{
    public partial class StandardVotingCandidate : UserControl
    {
        public StandardVotingCandidate()
        {
            InitializeComponent();
        }

        public void SetName(string name)
        {
            this.name.Text = name;
        }

        public void SetParty(string party)
        {
            this.party.Text = party;
        }

        public bool GetChecked()
        {
            return this.votingCheckbox.Checked;
        }
    }
}
