using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace BlockchainVotingSystem.Contracts.bvs_backend.ContractDefinition
{
    public partial class Candidate : CandidateBase { }

    public class CandidateBase 
    {
        [Parameter("string", "firstName", 1)]
        public virtual string FirstName { get; set; }
        [Parameter("string", "lastName", 2)]
        public virtual string LastName { get; set; }
        [Parameter("string", "party", 3)]
        public virtual string Party { get; set; }
        [Parameter("uint256", "id", 4)]
        public virtual BigInteger Id { get; set; }
    }
}
