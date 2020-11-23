﻿using System;
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

        public void SendVoteStandard(String votingAdress, Candidate candidate)
        {
            // Create ballot with voted candidate
            Ballot ballot = new Ballot();
            ballot.CandidateId = candidate.GetId();
            ballot.VoterAddress = votingAdress;
            // call service vote function
            votingService.VoteRequestAsync(currentElection.Id, ballot);
        }

        public void SendVoteAlternative(String votingAdress, List<Candidate> candidateList)
        {
            // Create ballot with voted candidate rankings
            Ballot ballot = new Ballot();
            List<BigInteger> rankings = new List<BigInteger>();
            candidateList.ForEach(x => rankings.Add(x.GetRank()));
            ballot.Ranking = rankings;

            votingService.VoteRequestAsync(currentElection.Id, ballot);
        }

        public void SetBlockchainUrl(String url)
        {
            if (url.Length != 0)
            {
                this.web3 = new Web3(url);
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