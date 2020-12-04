using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Blockchain_Wahlclient
{
    public partial class AlternativeVotingCandidate : UserControl
    {
        private int id;
        public void SetName (string name)
        {
            this.name.Text = name;
        }

        public void SetParty (string party)
        {
            this.party.Text = party;
        }

        public void SetMax (int max)
        {
            this.rankChooser.Maximum = max;
        }

        public int GetRank()
        {
            return (int)this.rankChooser.Value;
        }

        public int GetId()
        {
            return this.id;
        }

        public void SetId(int id)
        {
            this.id = id;
        }

        public AlternativeVotingCandidate()
        {
            InitializeComponent();
        }

    }
}
