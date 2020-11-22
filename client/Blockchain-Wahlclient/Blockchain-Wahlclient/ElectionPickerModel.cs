using BlockchainVotingSystem.Contracts.bvs_backend.ContractDefinition;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Blockchain_Wahlclient
{
    class ElectionPickerModel
    {
        public Backend Backend { get; set; }
        private List<TmpElectionObject> elections;
        private List<ElectionControl> frontendElections;

        public ElectionPickerModel()
        {
           
        }

        // Load all elections from Backend and show them in Form
        public void LoadElections(ref FlowLayoutPanel flp)
        {
            this.elections = Backend.LoadElectionInformationAsync().Result;
            foreach (TmpElectionObject election in this.elections)
            {
                ElectionControl electionControl = new ElectionControl();
                electionControl.SetName(election.Name);
                electionControl.SetId((int) election.Id);
                electionControl.SetTimestamp(election.StartTimestamp.ToString() + election.EndTimestamp.ToString());

                frontendElections.Add(electionControl);
            }

            foreach(ElectionControl ec in frontendElections)
            {
                flp.Controls.Add(ec);
            }
        }

        public bool ValidatePick()
        {
            ElectionControl pickedElectionControl = null;
            int checkedItems = 0;
            foreach (ElectionControl ec in frontendElections)
            {
                if(ec.GetChecked())
                {
                    checkedItems++;
                    pickedElectionControl = ec;
                }
            }

            if(checkedItems < 1)
            {
                MessageBox.Show("You have to pick an election");
                return false;
            }

            if (checkedItems > 1)
            {
                MessageBox.Show("You can only pick one election");
                return false;
            }

            // everything is oke. Set picked election ID in backend and return true
            if (pickedElectionControl != null)
            {
                Backend.SetCurrentElection(pickedElectionControl.GetId());
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
