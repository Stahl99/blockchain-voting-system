using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace BlockchainVotingSystem.Contracts.bvs_backend.ContractDefinition
{
    public partial class Ballot : BallotBase { }

    public class BallotBase 
    {
        [Parameter("address", "voterAddress", 1)]
        public virtual string VoterAddress { get; set; }
        [Parameter("uint256", "candidateId", 2)]
        public virtual BigInteger CandidateId { get; set; }
        [Parameter("uint256[]", "ranking", 3)]
        public virtual List<BigInteger> Ranking { get; set; }
    }
}
