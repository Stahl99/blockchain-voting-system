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
        private Web3 web3;
        private Bvs_backendService votingService;
        private GetElectionInformationOutputDTO allElectionInfo;
        private TmpElectionObject currentElection;
        private GetElectoralListOutputDTO backendCandidates;

        public Backend()
        {
        }

        /// <summary>
        /// Init the contract service with a blockchain url and contractAdress
        /// </summary>
        /// <param name="url">The url of the blockchain</param>
        /// <param name="contractAdress">The adress of the voting contract</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get the List of Canidates for the current election
        /// </summary>
        /// <returns>A List of <c>Candidate</c></returns>
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

        /// <summary>
        /// Send the vote for a first past the post election to the blockchain
        /// </summary>
        /// <param name="votingAdress">The private key of the voter</param>
        /// <param name="candidate">The candidate the voter has voted for</param>
        /// <returns>A task wrapping a bool to indicate if the voting was successful</returns>
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

        /// <summary>
        /// Send the vote for a alternative voting election to the blockchain
        /// </summary>
        /// <param name="votingAdress">The private key of the voter</param>
        /// <param name="candidateList">The list of candidates for the current election</param>
        /// <returns>A task wrapping a bool to indicate if the voting was successful</returns>
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

        /// <summary>
        /// Generates the Web3 object with the given blockchain url
        /// </summary>
        /// <param name="url">The url of the blockchain</param>
        private void SetBlockchainUrl(String url)
        {
            if (url.Length != 0)
            {
                this.url = url;

                this.web3 = new Web3(url);

            }
        }

        /// <summary>
        /// Generates an account from the private key of the voter and sets the account to the web3 object
        /// </summary>
        /// <param name="voterKey">The private key of the voter</param>
        private void SetAccount(String voterKey)
        {
            if (OnlyHexInString(voterKey))
            {
                this.votingAccount = new Account(voterKey);
                this.web3 = new Web3(this.votingAccount, this.url);
                this.votingService = new Bvs_backendService(this.web3, this.contractAddress);
            }
        }

        /// <summary>
        /// Sets the address of the smart contract and creates the backendService to communicate with the blockchain
        /// </summary>
        /// <param name="contractAdress">The address of the contract</param>
        /// <returns>If the method was successful</returns>
        private bool SetContractAdress(String contractAdress)
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
        /// <param name="test">The string to be checked</param>
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
