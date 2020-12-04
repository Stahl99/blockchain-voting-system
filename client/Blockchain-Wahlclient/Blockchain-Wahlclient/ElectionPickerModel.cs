using BlockchainVotingSystem.Contracts.bvs_backend.ContractDefinition;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blockchain_Wahlclient
{
    class ElectionPickerModel
    {
        public Backend Backend { get; set; }
        private List<TmpElectionObject> elections;
        private TmpElectionObject pickedElection;
        private List<ElectionControl> frontendElections = new List<ElectionControl>();

        public ElectionPickerModel()
        {
           
        }

        // Load all elections from Backend and show them in Form
        public async Task LoadElections()
        {
            await WaitForElections();

            foreach (TmpElectionObject election in this.elections)
            {
                ElectionControl electionControl = new ElectionControl();
                electionControl.SetName(election.Name);
                electionControl.SetId((int) election.Id);
                electionControl.SetTimestamp(UnixTimeStampToDateTime((double) election.StartTimestamp).ToString() + "   "
                    + UnixTimeStampToDateTime((double)election.EndTimestamp).ToString());

                frontendElections.Add(electionControl);
            }

            
        }

        public void ShowElections(ref FlowLayoutPanel flp)
        {
            foreach (ElectionControl ec in frontendElections)
            {
                flp.Controls.Add(ec);
            }
        }

        public async Task WaitForElections()
        {
            this.elections = await Backend.LoadElectionInformationAsync();

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
                    pickedElection = elections.Find(x => (x.Id == ec.GetId()));
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

        // Return voting type 0 = Standard, 1 = Alternative Voting
        public int GetVotingType()
        {
            return (int) pickedElection.VotingSystem;
        }

        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
