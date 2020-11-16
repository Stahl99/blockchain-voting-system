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
using BlockchainVotingSystem.Contracts.DHBWVoting.ContractDefinition;

namespace BlockchainVotingSystem.Contracts.DHBWVoting
{
    public partial class DHBWVotingService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, DHBWVotingDeployment dHBWVotingDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<DHBWVotingDeployment>().SendRequestAndWaitForReceiptAsync(dHBWVotingDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, DHBWVotingDeployment dHBWVotingDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<DHBWVotingDeployment>().SendRequestAsync(dHBWVotingDeployment);
        }

        public static async Task<DHBWVotingService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, DHBWVotingDeployment dHBWVotingDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, dHBWVotingDeployment, cancellationTokenSource);
            return new DHBWVotingService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public DHBWVotingService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<BigInteger> GetVoteAmountQueryAsync(GetVoteAmountFunction getVoteAmountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetVoteAmountFunction, BigInteger>(getVoteAmountFunction, blockParameter);
        }

        
        public Task<BigInteger> GetVoteAmountQueryAsync(string votingOption, BlockParameter blockParameter = null)
        {
            var getVoteAmountFunction = new GetVoteAmountFunction();
                getVoteAmountFunction.VotingOption = votingOption;
            
            return ContractHandler.QueryAsync<GetVoteAmountFunction, BigInteger>(getVoteAmountFunction, blockParameter);
        }

        public Task<string> GetWinnersQueryAsync(GetWinnersFunction getWinnersFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetWinnersFunction, string>(getWinnersFunction, blockParameter);
        }

        
        public Task<string> GetWinnersQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetWinnersFunction, string>(null, blockParameter);
        }

        public Task<bool> HasVotedQueryAsync(HasVotedFunction hasVotedFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<HasVotedFunction, bool>(hasVotedFunction, blockParameter);
        }

        
        public Task<bool> HasVotedQueryAsync(string account, BlockParameter blockParameter = null)
        {
            var hasVotedFunction = new HasVotedFunction();
                hasVotedFunction.Account = account;
            
            return ContractHandler.QueryAsync<HasVotedFunction, bool>(hasVotedFunction, blockParameter);
        }

        public Task<BigInteger> MultiplyQueryAsync(MultiplyFunction multiplyFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MultiplyFunction, BigInteger>(multiplyFunction, blockParameter);
        }

        
        public Task<BigInteger> MultiplyQueryAsync(BigInteger a, BigInteger b, BlockParameter blockParameter = null)
        {
            var multiplyFunction = new MultiplyFunction();
                multiplyFunction.A = a;
                multiplyFunction.B = b;
            
            return ContractHandler.QueryAsync<MultiplyFunction, BigInteger>(multiplyFunction, blockParameter);
        }

        public Task<string> VoteRequestAsync(VoteFunction voteFunction)
        {
             return ContractHandler.SendRequestAsync(voteFunction);
        }

        public Task<TransactionReceipt> VoteRequestAndWaitForReceiptAsync(VoteFunction voteFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(voteFunction, cancellationToken);
        }

        public Task<string> VoteRequestAsync(string votingOption)
        {
            var voteFunction = new VoteFunction();
                voteFunction.VotingOption = votingOption;
            
             return ContractHandler.SendRequestAsync(voteFunction);
        }

        public Task<TransactionReceipt> VoteRequestAndWaitForReceiptAsync(string votingOption, CancellationTokenSource cancellationToken = null)
        {
            var voteFunction = new VoteFunction();
                voteFunction.VotingOption = votingOption;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(voteFunction, cancellationToken);
        }
    }
}
