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

        /// <summary>
        /// Load all elections. Create <c>ElectionControl</c> objects for them and add them to the List of frontend elections
        /// </summary>
        /// <returns>A Task</returns>
        public async Task LoadElections()
        {
            await WaitForElections();

            foreach (TmpElectionObject election in this.elections)
            {
                ElectionControl electionControl = new ElectionControl();
                electionControl.SetName(election.Name);
                electionControl.SetId((int) election.Id);
                electionControl.SetTimestamp("Start: " + UnixTimeStampToDateTime((double) election.StartTimestamp).ToString() + "   "
                    + "End: " + UnixTimeStampToDateTime((double)election.EndTimestamp).ToString());

                frontendElections.Add(electionControl);
            }

            
        }

        /// <summary>
        /// Adds all elections to the given <c>FlowLayoutPanel</c> <paramref name="flp"/> and displays them
        /// </summary>
        /// <param name="flp">The <c>FlowLayoutPanel</c> </param>
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
            
               
        /// <summary>
        /// Validates the picked election and sets the selected election in the backend.
        /// Shows error messages to user if the input is wrong
        /// </summary>
        /// <returns>True if the method was successful
        /// False if there was and error</returns>
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

        /// <summary>
        /// Returns the voting system of the picked election
        /// </summary>
        /// <returns>An int representing the voting system.
        /// 0 = First past the post voting
        /// 1 = Alternative voting</returns>
        public int GetVotingType()
        {
            return (int) pickedElection.VotingSystem;
        }

        /// <summary>
        /// Converts a unix timestamp to a <c>DateTime</c> object
        /// </summary>
        /// <param name="unixTimeStamp">The unix timestamp to be converted</param>
        /// <returns>A <c>DateTime</c> object representing the date given in the unix timestamp</returns>
        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
