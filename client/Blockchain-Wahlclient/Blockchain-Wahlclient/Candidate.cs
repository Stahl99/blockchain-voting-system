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

        // Contructor to convert Backen candidate type to frontend candidate
        public Candidate(BlockchainVotingSystem.Contracts.bvs_backend.ContractDefinition.Candidate candidate)
        {
            this.id = (int) candidate.Id;
            this.surname = candidate.FirstName;
            this.lastName = candidate.LastName;
            this.party = candidate.Party;
            this.rank = 0;
        }

        public Candidate(int id, string surname, string lastName, string party)
        {
            this.id = id;

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
