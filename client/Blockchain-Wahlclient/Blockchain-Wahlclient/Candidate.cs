using System;
using System.Collections.Generic;
using System.Text;

namespace Blockchain_Wahlclient
{
    public class Candidate
    {
        private int id;

        private string surname;

        private string lastName;

        private string party;

        private int rank;

        public Candidate(int id, string surname, string lastName, string party)
        {

            this.surname = surname;

            this.lastName = lastName;

            this.party = party;

            this.rank = 0;

        }

        public string GetFullName()
        {
            return this.surname + " " + this.lastName;
        }

        public string GetParty()
        {
            return this.party;
        }

        public void SetRank(int rank)
        {
            this.rank = rank;
        }

        public int GetRank()
        {
            return this.rank;
        }

        public int GetId()
        {
            return this.id;
        }
        

    }
}
