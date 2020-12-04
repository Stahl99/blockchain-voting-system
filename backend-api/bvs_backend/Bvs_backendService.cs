using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using BlockchainVotingSystem.Contracts.bvs_backend.ContractDefinition;

namespace BlockchainVotingSystem.Contracts.bvs_backend
{
    public partial class Bvs_backendService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, Bvs_backendDeployment bvs_backendDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<Bvs_backendDeployment>().SendRequestAndWaitForReceiptAsync(bvs_backendDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, Bvs_backendDeployment bvs_backendDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<Bvs_backendDeployment>().SendRequestAsync(bvs_backendDeployment);
        }

        public static async Task<Bvs_backendService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, Bvs_backendDeployment bvs_backendDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, bvs_backendDeployment, cancellationTokenSource);
            return new Bvs_backendService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public Bvs_backendService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> CountVotesRequestAsync(CountVotesFunction countVotesFunction)
        {
             return ContractHandler.SendRequestAsync(countVotesFunction);
        }

        public Task<TransactionReceipt> CountVotesRequestAndWaitForReceiptAsync(CountVotesFunction countVotesFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(countVotesFunction, cancellationToken);
        }

        public Task<string> CountVotesRequestAsync(BigInteger electionId)
        {
            var countVotesFunction = new CountVotesFunction();
                countVotesFunction.ElectionId = electionId;
            
             return ContractHandler.SendRequestAsync(countVotesFunction);
        }

        public Task<TransactionReceipt> CountVotesRequestAndWaitForReceiptAsync(BigInteger electionId, CancellationTokenSource cancellationToken = null)
        {
            var countVotesFunction = new CountVotesFunction();
                countVotesFunction.ElectionId = electionId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(countVotesFunction, cancellationToken);
        }

        public Task<string> CreateElectionRequestAsync(CreateElectionFunction createElectionFunction)
        {
             return ContractHandler.SendRequestAsync(createElectionFunction);
        }

        public Task<TransactionReceipt> CreateElectionRequestAndWaitForReceiptAsync(CreateElectionFunction createElectionFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createElectionFunction, cancellationToken);
        }

        public Task<string> CreateElectionRequestAsync(string electionAdminAddress, byte electionVotingSystem, string electionName, BigInteger electionStartTimestamp, BigInteger electionEndTimestamp)
        {
            var createElectionFunction = new CreateElectionFunction();
                createElectionFunction.ElectionAdminAddress = electionAdminAddress;
                createElectionFunction.ElectionVotingSystem = electionVotingSystem;
                createElectionFunction.ElectionName = electionName;
                createElectionFunction.ElectionStartTimestamp = electionStartTimestamp;
                createElectionFunction.ElectionEndTimestamp = electionEndTimestamp;
            
             return ContractHandler.SendRequestAsync(createElectionFunction);
        }

        public Task<TransactionReceipt> CreateElectionRequestAndWaitForReceiptAsync(string electionAdminAddress, byte electionVotingSystem, string electionName, BigInteger electionStartTimestamp, BigInteger electionEndTimestamp, CancellationTokenSource cancellationToken = null)
        {
            var createElectionFunction = new CreateElectionFunction();
                createElectionFunction.ElectionAdminAddress = electionAdminAddress;
                createElectionFunction.ElectionVotingSystem = electionVotingSystem;
                createElectionFunction.ElectionName = electionName;
                createElectionFunction.ElectionStartTimestamp = electionStartTimestamp;
                createElectionFunction.ElectionEndTimestamp = electionEndTimestamp;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createElectionFunction, cancellationToken);
        }

        public Task<GetElectionInformationOutputDTO> GetElectionInformationQueryAsync(GetElectionInformationFunction getElectionInformationFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetElectionInformationFunction, GetElectionInformationOutputDTO>(getElectionInformationFunction, blockParameter);
        }

        public Task<GetElectionInformationOutputDTO> GetElectionInformationQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetElectionInformationFunction, GetElectionInformationOutputDTO>(null, blockParameter);
        }

        public Task<GetElectoralListOutputDTO> GetElectoralListQueryAsync(GetElectoralListFunction getElectoralListFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetElectoralListFunction, GetElectoralListOutputDTO>(getElectoralListFunction, blockParameter);
        }

        public Task<GetElectoralListOutputDTO> GetElectoralListQueryAsync(BigInteger electionId, BlockParameter blockParameter = null)
        {
            var getElectoralListFunction = new GetElectoralListFunction();
                getElectoralListFunction.ElectionId = electionId;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetElectoralListFunction, GetElectoralListOutputDTO>(getElectoralListFunction, blockParameter);
        }

        public Task<BigInteger> GetLastElectionIdQueryAsync(GetLastElectionIdFunction getLastElectionIdFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetLastElectionIdFunction, BigInteger>(getLastElectionIdFunction, blockParameter);
        }

        
        public Task<BigInteger> GetLastElectionIdQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetLastElectionIdFunction, BigInteger>(null, blockParameter);
        }

        public Task<GetVoteBallotOutputDTO> GetVoteBallotQueryAsync(GetVoteBallotFunction getVoteBallotFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetVoteBallotFunction, GetVoteBallotOutputDTO>(getVoteBallotFunction, blockParameter);
        }

        public Task<GetVoteBallotOutputDTO> GetVoteBallotQueryAsync(BigInteger electionId, BlockParameter blockParameter = null)
        {
            var getVoteBallotFunction = new GetVoteBallotFunction();
                getVoteBallotFunction.ElectionId = electionId;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetVoteBallotFunction, GetVoteBallotOutputDTO>(getVoteBallotFunction, blockParameter);
        }

        public Task<string> GetVoteStringQueryAsync(GetVoteStringFunction getVoteStringFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetVoteStringFunction, string>(getVoteStringFunction, blockParameter);
        }

        
        public Task<string> GetVoteStringQueryAsync(BigInteger electionId, string requestAddress, BlockParameter blockParameter = null)
        {
            var getVoteStringFunction = new GetVoteStringFunction();
                getVoteStringFunction.ElectionId = electionId;
                getVoteStringFunction.RequestAddress = requestAddress;
            
            return ContractHandler.QueryAsync<GetVoteStringFunction, string>(getVoteStringFunction, blockParameter);
        }

        public Task<string> ReplaceElectoralListRequestAsync(ReplaceElectoralListFunction replaceElectoralListFunction)
        {
             return ContractHandler.SendRequestAsync(replaceElectoralListFunction);
        }

        public Task<TransactionReceipt> ReplaceElectoralListRequestAndWaitForReceiptAsync(ReplaceElectoralListFunction replaceElectoralListFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(replaceElectoralListFunction, cancellationToken);
        }

        public Task<string> ReplaceElectoralListRequestAsync(BigInteger electionId, List<Candidate> newElectoralList)
        {
            var replaceElectoralListFunction = new ReplaceElectoralListFunction();
                replaceElectoralListFunction.ElectionId = electionId;
                replaceElectoralListFunction.NewElectoralList = newElectoralList;
            
             return ContractHandler.SendRequestAsync(replaceElectoralListFunction);
        }

        public Task<TransactionReceipt> ReplaceElectoralListRequestAndWaitForReceiptAsync(BigInteger electionId, List<Candidate> newElectoralList, CancellationTokenSource cancellationToken = null)
        {
            var replaceElectoralListFunction = new ReplaceElectoralListFunction();
                replaceElectoralListFunction.ElectionId = electionId;
                replaceElectoralListFunction.NewElectoralList = newElectoralList;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(replaceElectoralListFunction, cancellationToken);
        }

        public Task<string> ReplaceListOfEligibleVotersRequestAsync(ReplaceListOfEligibleVotersFunction replaceListOfEligibleVotersFunction)
        {
             return ContractHandler.SendRequestAsync(replaceListOfEligibleVotersFunction);
        }

        public Task<TransactionReceipt> ReplaceListOfEligibleVotersRequestAndWaitForReceiptAsync(ReplaceListOfEligibleVotersFunction replaceListOfEligibleVotersFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(replaceListOfEligibleVotersFunction, cancellationToken);
        }

        public Task<string> ReplaceListOfEligibleVotersRequestAsync(BigInteger electionId, List<string> newEligibleVoterList)
        {
            var replaceListOfEligibleVotersFunction = new ReplaceListOfEligibleVotersFunction();
                replaceListOfEligibleVotersFunction.ElectionId = electionId;
                replaceListOfEligibleVotersFunction.NewEligibleVoterList = newEligibleVoterList;
            
             return ContractHandler.SendRequestAsync(replaceListOfEligibleVotersFunction);
        }

        public Task<TransactionReceipt> ReplaceListOfEligibleVotersRequestAndWaitForReceiptAsync(BigInteger electionId, List<string> newEligibleVoterList, CancellationTokenSource cancellationToken = null)
        {
            var replaceListOfEligibleVotersFunction = new ReplaceListOfEligibleVotersFunction();
                replaceListOfEligibleVotersFunction.ElectionId = electionId;
                replaceListOfEligibleVotersFunction.NewEligibleVoterList = newEligibleVoterList;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(replaceListOfEligibleVotersFunction, cancellationToken);
        }

        public Task<string> TestVoteRequestAsync(TestVoteFunction testVoteFunction)
        {
             return ContractHandler.SendRequestAsync(testVoteFunction);
        }

        public Task<TransactionReceipt> TestVoteRequestAndWaitForReceiptAsync(TestVoteFunction testVoteFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(testVoteFunction, cancellationToken);
        }

        public Task<string> TestVoteRequestAsync(BigInteger electionId, BigInteger candidateId, List<BigInteger> ranking)
        {
            var testVoteFunction = new TestVoteFunction();
                testVoteFunction.ElectionId = electionId;
                testVoteFunction.CandidateId = candidateId;
                testVoteFunction.Ranking = ranking;
            
             return ContractHandler.SendRequestAsync(testVoteFunction);
        }

        public Task<TransactionReceipt> TestVoteRequestAndWaitForReceiptAsync(BigInteger electionId, BigInteger candidateId, List<BigInteger> ranking, CancellationTokenSource cancellationToken = null)
        {
            var testVoteFunction = new TestVoteFunction();
                testVoteFunction.ElectionId = electionId;
                testVoteFunction.CandidateId = candidateId;
                testVoteFunction.Ranking = ranking;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(testVoteFunction, cancellationToken);
        }

        public Task<string> VoteRequestAsync(VoteFunction voteFunction)
        {
             return ContractHandler.SendRequestAsync(voteFunction);
        }

        public Task<TransactionReceipt> VoteRequestAndWaitForReceiptAsync(VoteFunction voteFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(voteFunction, cancellationToken);
        }

        public Task<string> VoteRequestAsync(BigInteger electionId, Ballot ballot)
        {
            var voteFunction = new VoteFunction();
                voteFunction.ElectionId = electionId;
                voteFunction.Ballot = ballot;
            
             return ContractHandler.SendRequestAsync(voteFunction);
        }

        public Task<TransactionReceipt> VoteRequestAndWaitForReceiptAsync(BigInteger electionId, Ballot ballot, CancellationTokenSource cancellationToken = null)
        {
            var voteFunction = new VoteFunction();
                voteFunction.ElectionId = electionId;
                voteFunction.Ballot = ballot;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(voteFunction, cancellationToken);
        }
    }
}
