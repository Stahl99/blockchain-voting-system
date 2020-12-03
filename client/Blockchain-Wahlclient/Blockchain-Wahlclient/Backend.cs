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
using Nethereum.Web3.Accounts;

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
        public bool InitService(String url, String contractAdress)
        {
            // The order of these functions is important! Web3 object is needed for service
            SetBlockchainUrl(url);
            if(!SetContractAdress(contractAdress))
            {
                return false;
            }

            return true;
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

        public async Task SendVoteStandard(String votingAdress, Candidate candidate)
        {
            // Create ballot with voted candidate
            Ballot ballot = new Ballot();
            ballot.CandidateId = candidate.GetId();
            ballot.VoterAddress = votingAdress;
            ballot.Ranking = new List<BigInteger>();
            // call service vote function
            var receipt = await votingService.VoteRequestAndWaitForReceiptAsync(currentElection.Id, ballot);

            var result = await votingService.GetVoteBallotQueryAsync(currentElection.Id);
            MessageBox.Show(result.ReturnValue1.CandidateId.ToString());

        }

        public async Task SendVoteAlternativeAsync(String votingAdress, List<Candidate> candidateList)
        {
            // Create ballot with voted candidate rankings
            Ballot ballot = new Ballot();
            List<BigInteger> rankings = new List<BigInteger>();
            candidateList.ForEach(x => rankings.Add(x.GetRank())); 
            //ballot.Ranking = rankings;

            await votingService.VoteRequestAsync(currentElection.Id, ballot);

            var result = await votingService.GetVoteBallotQueryAsync(currentElection.Id);
            MessageBox.Show(result.ReturnValue1.CandidateId.ToString());
        }

        public void SetBlockchainUrl(String url)
        {
            if (url.Length != 0)
            {
                var privateKey = "0x5569b93622765c3100095da4d24e9494231dc01873ad7c07d69acc06cc1ca3b3";
                var account = new Account(privateKey);

                this.web3 = new Web3(account, url);
                this.web3.TransactionManager.DefaultGas = 5000000;
            }
        }

        public bool SetContractAdress(String contractAdress)
        {
            // check format 
            if (!OnlyHexInString(contractAdress))
            {
                MessageBox.Show("Wrong format for election adresss (must be hex-string)");
                return false;
            }

            // create voting service with new contract adress
            this.votingService = new Bvs_backendService(web3, contractAdress);

            
            return true;
        }

        public async Task<List<TmpElectionObject>> LoadElectionInformationAsync()
        {
            allElectionInfo =  await this.votingService.GetElectionInformationQueryAsync();
            return allElectionInfo.ReturnValue1;
        }

        public async Task LoadCandidateInfoAsync()
        {
            backendCandidates = await votingService.GetElectoralListQueryAsync(currentElection.Id);
        }

        // Set which election is currently selected
        public void SetCurrentElection(int electionId)
        {
            currentElection = allElectionInfo.ReturnValue1.Find(x => (x.Id == electionId));
        }

        public void LoadElectoralList()
        {
            var task = votingService.GetElectoralListQueryAsync(0);

            task.Wait();

            var resu = task.Result;


        }

        private bool OnlyHexInString(string test)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return true;
            return System.Text.RegularExpressions.Regex.IsMatch(test, @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z");
        }
    }
}
