using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Blockchain_Wahlclient
{
    public class CandidateList : IEnumerable, IEnumerator
    {

        private List<Candidate> candidates = new List<Candidate>();
        private List<AlternativeVotingCandidate> candsFrontend = new List<AlternativeVotingCandidate>();

        int position = 0;

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

        public void AddCandidate(Candidate c)
        {
            this.candidates.Add(c);
        }

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

        public void BuildFrontend(ref FlowLayoutPanel flp)        
        {            

            foreach (Candidate c in candidates)
            {
                AlternativeVotingCandidate avc = new AlternativeVotingCandidate();
                avc.SetName(c.GetFullName());
                avc.SetParty(c.GetParty());
                avc.SetMax(candidates.Count);

                candsFrontend.Add(avc);                
            }

            foreach (AlternativeVotingCandidate avc in candsFrontend)
            {
                flp.Controls.Add(avc);
            }
        }

        public void SecureRanks()
        {

            int index = 0;
            foreach (AlternativeVotingCandidate avc in candsFrontend)
            {
                candidates[index].SetRank(avc.GetRank());
                index++;
            }
        }
    }
}
