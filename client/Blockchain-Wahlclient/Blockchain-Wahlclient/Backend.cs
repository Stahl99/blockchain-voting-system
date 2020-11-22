using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows.Forms;
using Nethereum.Web3;
using BlockchainVotingSystem.Contracts.DHBWVoting;
using BlockchainVotingSystem.Contracts.bvs_backend;
using BlockchainVotingSystem.Contracts.bvs_backend.ContractDefinition;
using System.Threading.Tasks;

namespace Blockchain_Wahlclient
{
    public class Backend
    {
        Web3 web3;
        Bvs_backendService votingService;
        GetElectionInformationOutputDTO allElectionInfo;
        TmpElectionObject currentElection;
        GetElectoralListOutputDTO backendCandidates;

        public Backend()
        {
        }

        // Init the contract service with a blockchain url and contractAdress
        public void InitService(String url, String contractAdress)
        {
            // The order of these functions is important! Web3 object is needed for service
            SetBlockchainUrl(url);
            SetContractAdress(contractAdress);
        }

        // Return enum of election type
        public bool GetElectionType()
        {
            return true;
        }

        // Return a List of Candidates for the current election
        public List<Candidate> GetCandidateInfo()
        {
            List<Candidate> frontendCandidates = new List<Candidate>();
            foreach (BlockchainVotingSystem.Contracts.bvs_backend.ContractDefinition.Candidate c in backendCandidates.Candidates)
            {
                Candidate frontendC = new Candidate(c);
                frontendCandidates.Add(frontendC);
            }

            return frontendCandidates;
        }

        public void SendVote(String votingAdress, Candidate candidate)
        {
            // call service vote function
        }

        public void SetBlockchainUrl(String url)
        {
            this.web3 = new Web3(url);
        }

        public void SetContractAdress(String contractAdress)
        {
            // check format 
            if (!OnlyHexInString(contractAdress))
            {
                MessageBox.Show("Wrong format for election adresss (must be hex-string)");
                return;
            }

            // create voting service with new contract adress
            this.votingService = new Bvs_backendService(web3, contractAdress);
        }

        public async Task<List<TmpElectionObject>> LoadElectionInformationAsync()
        {
            allElectionInfo =  await this.votingService.GetElectionInformationQueryAsync();
            return allElectionInfo.ReturnValue1;
        }

        // Set which election is currently selected
        public void SetCurrentElection(int electionId)
        {
            currentElection = allElectionInfo.ReturnValue1.Find(x => (x.Id == electionId));
        }

        private bool OnlyHexInString(string test)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return System.Text.RegularExpressions.Regex.IsMatch(test, @"\A\b[0-9a-fA-F]+\b\Z");
        }
    }
}
