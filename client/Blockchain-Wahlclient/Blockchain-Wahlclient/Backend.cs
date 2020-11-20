using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows.Forms;
using Nethereum.Web3;
using BlockchainVotingSystem.Contracts.DHBWVoting;

namespace Blockchain_Wahlclient
{
    public class Backend
    {
        Web3 web3;
        DHBWVotingService votingService;

        public Backend()
        {
        }

        // Init the contract service with a blockchain url and contractAdress
        public void InitService(String url, String contractAdress)
        {
            // The order of these functions is important! Web3 object is needed for service
            SetBlockchainUrl(url);
            SetContactAdress(contractAdress);
        }

        // Return enum of election type
        public bool GetElectionType()
        {
            return true;
        }

        public List<Candidate> GetCandidateInfo()
        {
            return new List<Candidate>();
        }

        public void SendVote(String votingAdress, Candidate candidate)
        {
            // call service vote function
        }

        public void SetBlockchainUrl(String url)
        {
            this.web3 = new Web3(url);
        }

        public void SetContactAdress(String contractAdress)
        {
            // check format 
            if (!OnlyHexInString(contractAdress))
            {
                MessageBox.Show("Wrong format for election adresss (must be hex-string)");
                return;
            }

            // create voting service with new contract adress
            this.votingService = new DHBWVotingService(web3, contractAdress);
        }

        private bool OnlyHexInString(string test)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return System.Text.RegularExpressions.Regex.IsMatch(test, @"\A\b[0-9a-fA-F]+\b\Z");
        }
    }
}
