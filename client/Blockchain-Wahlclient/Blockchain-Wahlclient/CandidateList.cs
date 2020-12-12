using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blockchain_Wahlclient
{
    /// <summary>
    /// Class to represent a list of candidates and to implement functions for the AlternativeVotingForm
    /// </summary>
    public class CandidateList : IEnumerable, IEnumerator
    {

        private List<Candidate> candidates = new List<Candidate>();
        private List<AlternativeVotingCandidate> candsFrontend = new List<AlternativeVotingCandidate>();
        private Backend backend;

        int position = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="backend">The backend the class is initialized with</param>
        public CandidateList(Backend backend)
        {
            this.backend = backend;
            var task = Task.Run(async () => { await backend.LoadCandidateInfoAsync(); });
            task.Wait();
            this.candidates = backend.GetCandidateInfo();
        }

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        public bool MoveNext()
        {
            position++;
            return (position < candidates.Count);
        }

        public void Reset()
        {
            position = 0;
        }

        public object Current
        {
            get { return candidates[position]; }
        }

        /// <summary>
        /// Check if the set ranks are correct.
        /// All numbers from 1 to n (number of candidates) are entered
        /// </summary>
        /// <returns></returns>
        public bool CheckRanks()
        {
            int i = 1;

            foreach (Candidate c in candidates)
            {
                for (int a = i; a < candidates.Count; a++)
                {
                    if (c.GetRank() == candidates[i].GetRank() || c.GetRank() > candidates.Count)
                    {
                        return false;
                    }
                }

                i++;
            }

            return true;
        }

        /// <summary>
        /// Creates a User Control for every candidate and adds them to the FlowLayoutPanel,
        /// so they can be displayed on the form
        /// </summary>
        /// <param name="flp">The FlowLayoutPanel the User Controls should be added to</param>
        public void BuildFrontend(ref FlowLayoutPanel flp)        
        {            

            foreach (Candidate c in candidates)
            {
                AlternativeVotingCandidate avc = new AlternativeVotingCandidate();
                avc.SetName(c.GetFullName());
                avc.SetParty(c.GetParty());
                avc.SetMax(candidates.Count);
                avc.SetId(c.GetId());

                candsFrontend.Add(avc);                
            }

            foreach (AlternativeVotingCandidate avc in candsFrontend)
            {
                flp.Controls.Add(avc);
            }
        }

        /// <summary>
        /// Transfer the ranks entered on the frontend to the list of candidates
        /// </summary>
        public void SecureRanks()
        {

            int index = 0;
            foreach (AlternativeVotingCandidate avc in candsFrontend)
            {
                candidates[index].SetRank(avc.GetRank());
                index++;
            }
        }

        public List<Candidate> GetCandidates()
        {
            return candidates;
        }

        public bool OnlyHexInString(string test)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return System.Text.RegularExpressions.Regex.IsMatch(test, @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z");
        }
    }
}
