using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.CheckedListBox;

namespace Blockchain_Wahlclient
{
    public class FirstPastThePostModel
    {
        private FirstPastThePostForm FPTPform;
        private List<Candidate> candidates = new List<Candidate>();
        private List<StandardVotingCandidate> candidatesViewList = new List<StandardVotingCandidate>();
        private Candidate votedCandidate;
        private Backend backend;

        /// <summary>
        /// Constructor sets backend instance and loads candidate information for the current election
        /// </summary>
        /// <param name="backend">The backend instance to be set</param>
        public FirstPastThePostModel(Backend backend)
        {
            this.backend = backend;
            var task = Task.Run(async () => { await backend.LoadCandidateInfoAsync(); });
            task.Wait();
            this.candidates = backend.GetCandidateInfo();
        }

        /// <summary>
        /// Find the currently open <c>FirstPastThePostForm</c> instance
        /// </summary>
        private void InitFormReference()
        {
            // get the voting form
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType().Equals(typeof(FirstPastThePostForm)))
                {
                    FPTPform = (FirstPastThePostForm)form;
                    break;
                }
            }
        }

        /// <summary>
        /// Creates <c>StandardVotingCandidate</c> Control for the frontend from the list of candidates
        /// </summary>
        /// <param name="flp">A reference to the used <c>FlowLayoutPanel</c> on the open form</param>
        public void BuildCandidateList(ref FlowLayoutPanel flp)
        {
            foreach( Candidate c in candidates)
            {
                StandardVotingCandidate svc = new StandardVotingCandidate();
                svc.SetName(c.GetFullName());
                svc.SetParty(c.GetParty());
                svc.SetId(c.GetId());

                candidatesViewList.Add(svc);
            }

            foreach( StandardVotingCandidate svc in candidatesViewList)
            {
                flp.Controls.Add(svc);
            }
        }

        /// <summary>
        /// Verifies the selected and given voting options and shows error text if there are problems
        /// </summary>
        /// <param name="votingAdress">The private key of the voter</param>
        /// <returns>True if all given info is correct
        /// False if something is not correct</returns>
        public bool VerifyVote(String votingAdress)
        {
            InitFormReference();
            // check if exactly one item is checkd
            int items_checked = 0;
            foreach(StandardVotingCandidate c in candidatesViewList)
            {
                if (c.GetChecked())
                {
                    votedCandidate = this.candidates.Find(x => x.GetId() == c.GetId());
                    items_checked++;
                }
            }

            if(items_checked < 1)
            {
                FPTPform.ShowErrorText("You have to vote a candidate");
                return false;
            }

            if (items_checked > 1)
            {
                FPTPform.ShowErrorText("You can only vote one candidate");
                return false;
            }

            // check if the String contains only a HEX value
            if(!OnlyHexInString(votingAdress))
            {
                FPTPform.ShowErrorText("Adress is not a hash adress");
                return false;
            }

            // if everything is ok return true
            return true;
        }

        /// <summary>
        /// Calls the backend function to send the vote
        /// </summary>
        /// <param name="votingAdress">The private key of the voter</param>
        /// <returns>A task wrapping a bool to indicate if the voting was successful</returns>
        public async Task<bool> SendVote(String votingAdress)
        {
            return await backend.SendVoteStandard(votingAdress, votedCandidate);
        }

        /// <summary>
        /// Checks if only hex characters are in a String and its has the format 0x....
        /// </summary>
        /// <param name="test">The string to be checked</param>
        /// <returns><c>True</c> if only Hex characters are in the given String
        /// <c>False</c> if the String contains other characters</returns>
        public bool OnlyHexInString(string test)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return System.Text.RegularExpressions.Regex.IsMatch(test, @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z");
        }
    }
}
