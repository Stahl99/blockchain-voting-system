using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using BlockchainVotingSystem.Contracts.bvs_backend.ContractDefinition;
using System.Windows.Forms;

namespace blockchain_admintool
{
    public partial class candidate : UserControl
    {
        public candidate()
        {
            InitializeComponent();
        }

        public Candidate GetCandObj(int id)
        {
            Candidate candidate = new Candidate();

            if (string.IsNullOrEmpty(surname.Text) || string.IsNullOrEmpty(party.Text) || string.IsNullOrEmpty(lastname.Text))
            {
                return null;
            }

            candidate.FirstName = surname.Text;
            candidate.LastName = lastname.Text;
            candidate.Party = party.Text;
            candidate.Id = id;

            return candidate;
        }
    }
}
