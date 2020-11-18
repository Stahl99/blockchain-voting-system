using System;
using System.Collections.Generic;
using System.Text;
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

        public FirstPastThePostModel(Backend backend)
        {
            this.backend = backend;
            this.candidates = backend.GetCandidateInfo();
        }

        // Get Reference to the active Form
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

        public void AddCandidate(Candidate c)
        {
            this.candidates.Add(c);
        }

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

        // verify the voting logic
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

            // check if the adress has the correct length
            if (votingAdress.Length != 42)
            {
                FPTPform.ShowErrorText("Adress has incorrect size");
                return false;
            }

            // if everything is ok return true
            return true;
        }

        public void SendVote(String votingAdress)
        {
            backend.SendVote(votingAdress, votedCandidate);
        }

        public bool OnlyHexInString(string test)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return System.Text.RegularExpressions.Regex.IsMatch(test, @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z");
        }
    }
}
