using System;
using System.Collections.Generic;
using System.Text;

namespace Blockchain_Wahlclient
{
    /// <summary>
    /// Class to represent a candidate
    /// </summary>
    public class Candidate
    {
        private int id;

        private string surname;

        private string lastName;

        private string party;

        private int rank;

        // Contructor to convert Backen candidate type to frontend candidate

        /// <summary>
        /// Constructor to create a candidate object from a backend candidate object
        /// </summary>
        /// <param name="candidate">The backend candidate</param>
        public Candidate(BlockchainVotingSystem.Contracts.bvs_backend.ContractDefinition.Candidate candidate)
        {
            this.id = (int) candidate.Id;
            this.surname = candidate.FirstName;
            this.lastName = candidate.LastName;
            this.party = candidate.Party;
            this.rank = 0;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The candidate id</param>
        /// <param name="surname">The surname of the candidate</param>
        /// <param name="lastName">The lastname of the candidate</param>
        /// <param name="party">The party of the candidate</param>
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
