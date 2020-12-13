using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Blockchain_Wahlclient
{
    /// <summary>
    /// User Control that represents an entry in the list of candidates that can be displayed on a form
    /// </summary>
    public partial class StandardVotingCandidate : UserControl
    {
        private int id;
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

        public String GetName()
        {
            return this.name.Text;
        }

        public String GetParty()
        {
            return this.party.Text;
        }

        public bool GetChecked()
        {
            return this.votingCheckbox.Checked;
        }

        public void SetId(int id)
        {
            this.id = id; 
        }

        public int GetId()
        {
            return this.id;
        }
    }
}
