using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace BlockchainVotingSystem.Contracts.bvs_backend.ContractDefinition
{
    public partial class TmpElectionObject : TmpElectionObjectBase { }

    public class TmpElectionObjectBase 
    {
        [Parameter("uint256", "id", 1)]
        public virtual BigInteger Id { get; set; }
        [Parameter("string", "name", 2)]
        public virtual string Name { get; set; }
        [Parameter("uint256", "startTimestamp", 3)]
        public virtual BigInteger StartTimestamp { get; set; }
        [Parameter("uint256", "endTimestamp", 4)]
        public virtual BigInteger EndTimestamp { get; set; }
    }
}
