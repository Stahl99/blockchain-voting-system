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
        private String url;
        private String contractAddress;
        private Account votingAccount;
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

        public async Task<bool> SendVoteStandard(String votingAdress, Candidate candidate)
        {
            SetAccount(votingAdress);

            // Create ballot with voted candidate
            Ballot ballot = new Ballot();
            ballot.CandidateId = candidate.GetId();
            ballot.Ranking = new List<BigInteger>();
            ballot.VoterAddress = "0x00000000000000000000000000000000000000000";

            // call service vote function
            var receipt = await votingService.VoteRequestAndWaitForReceiptAsync(currentElection.Id, ballot);

            var result = await votingService.GetVoteBallotQueryAsync(currentElection.Id);

            return AreBallotsEqual(ballot, result.ReturnValue1);
        }

        public async Task<bool> SendVoteAlternativeAsync(String votingAdress, List<Candidate> candidateList)
        {
            // Set account
            SetAccount(votingAdress);

            // Create ballot with voted candidate rankings
            Ballot ballot = new Ballot();
            List<BigInteger> rankings = new List<BigInteger>();
            candidateList.ForEach(x => rankings.Add(x.GetRank())); 
            ballot.Ranking = rankings;
            ballot.VoterAddress = "0x00000000000000000000000000000000000000000";

            var receipt = await votingService.VoteRequestAndWaitForReceiptAsync(currentElection.Id, ballot);

            var result = await votingService.GetVoteBallotQueryAsync(currentElection.Id);

            return AreBallotsEqual(ballot, result.ReturnValue1);
        }

        public void SetBlockchainUrl(String url)
        {
            if (url.Length != 0)
            {
                this.url = url;

                this.web3 = new Web3(url);

            }
        }

        public void SetAccount(String voterKey)
        {
            if (OnlyHexInString(voterKey))
            {
                this.votingAccount = new Account(voterKey);
                this.web3 = new Web3(this.votingAccount, this.url);
                this.votingService = new Bvs_backendService(this.web3, this.contractAddress);
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

            this.contractAddress = contractAdress;

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

        /// <summary>
        /// Checks if only hex characters are in a String and its has the format 0x....
        /// </summary>
        /// <param name="test"></param>
        /// <returns><c>True</c> if only Hex characters are in the given String
        /// <c>False</c> if the String contains other characters</returns>
        private bool OnlyHexInString(string test)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return System.Text.RegularExpressions.Regex.IsMatch(test, @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z");
        }

        /// <summary>
        /// Compares two Ballots 
        /// </summary>
        /// <param name="a">First Ballot to compare</param>
        /// <param name="b">Second Ballot to compare</param>
        /// <returns> <c>True</c> if they contain the same values. 
        /// <c>False</c> if atleast one value is different</returns>
        private bool AreBallotsEqual(Ballot a, Ballot b)
        {
            // compare candidateIds and VoterAdresses
            if (a.CandidateId == b.CandidateId )
            {
                if (this.votingAccount.Address.ToLower().Equals((b.VoterAddress.ToLower())))
                {
                    // Compare Rankings
                    for (int i = 0; i < a.Ranking.Count; i++)
                    {
                        BigInteger compA = a.Ranking[i];
                        if (compA.CompareTo(b.Ranking[i]) == 0)
                        {
                            continue;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    // Ranking is the same
                    return true;
                }
            }

            return false;
        }
    }
}
