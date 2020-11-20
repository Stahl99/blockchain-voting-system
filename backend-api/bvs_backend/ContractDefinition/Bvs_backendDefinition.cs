using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace BlockchainVotingSystem.Contracts.bvs_backend.ContractDefinition
{


    public partial class Bvs_backendDeployment : Bvs_backendDeploymentBase
    {
        public Bvs_backendDeployment() : base(BYTECODE) { }
        public Bvs_backendDeployment(string byteCode) : base(byteCode) { }
    }

    public class Bvs_backendDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b5061230d806100206000396000f3fe608060405234801561001057600080fd5b50600436106100885760003560e01c8063da2b86d11161005b578063da2b86d114610117578063ef8e51e61461012a578063f35953cc14610142578063f7f095471461016257610088565b80631840f0ca1461008d5780632df0f580146100b75780634aea2f6a146100d75780635a55c1f0146100f7575b600080fd5b6100a061009b366004611c36565b610175565b6040516100ae9291906120d1565b60405180910390f35b6100ca6100c5366004611c4e565b6104ba565b6040516100ae9190612197565b6100ea6100e5366004611c36565b610588565b6040516100ae91906120b7565b61010a610105366004611c36565b610815565b6040516100ae91906121a2565b6100ca610125366004611e16565b610b03565b610132610e4d565b6040516100ae94939291906120ff565b610155610150366004611bc3565b61112b565b6040516100ae9190612280565b6100ca610170366004611cf6565b6112f4565b606080610181836114b2565b1580610193575061019183611501565b155b1561019d576104b5565b6060600084815481106101ac57fe5b90600052602060002090600a0201600201805480602002602001604051908101604052809291908181526020016000905b828210156103dc5760008481526020908190206040805160048602909201805460026001821615610100026000190190911604601f8101859004909402830160a090810190925260808301848152929390928492909184918401828280156102865780601f1061025b57610100808354040283529160200191610286565b820191906000526020600020905b81548152906001019060200180831161026957829003601f168201915b50505050508152602001600182018054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156103285780601f106102fd57610100808354040283529160200191610328565b820191906000526020600020905b81548152906001019060200180831161030b57829003601f168201915b5050509183525050600282810180546040805160206001841615610100026000190190931694909404601f810183900483028501830190915280845293810193908301828280156103ba5780601f1061038f576101008083540402835291602001916103ba565b820191906000526020600020905b81548152906001019060200180831161039d57829003601f168201915b50505050508152602001600382015481525050815260200190600101906101dd565b5050505090506060600085815481106103f157fe5b90600052602060002090600a020160030180548060200260200160405190810160405280929190818152602001828054801561044c57602002820191906000526020600020905b815481526020019060010190808311610438575b5050505050905061045c8561152b565b15806104955750600080868154811061047157fe5b600091825260209091206007600a90920201015460ff16600181111561049357fe5b145b156104b2576104ab818360006001855103611555565b50506104b5565b50505b915091565b6000805b60005481101561057c5783600082815481106104d657fe5b90600052602060002090600a020160000154141561057457336001600160a01b03166000828154811061050557fe5b60009182526020909120600a909102016007015461010090046001600160a01b0316141561056a57826000828154811061053b57fe5b90600052602060002090600a0201600501908051906020019061055f9291906116d5565b506001915050610582565b6000915050610582565b6001016104be565b50600090505b92915050565b606060005b60005481101561080e57600081815481106105a457fe5b90600052602060002090600a02016000015483141561080657600081815481106105ca57fe5b90600052602060002090600a0201600201805480602002602001604051908101604052809291908181526020016000905b828210156107fa5760008481526020908190206040805160048602909201805460026001821615610100026000190190911604601f8101859004909402830160a090810190925260808301848152929390928492909184918401828280156106a45780601f10610679576101008083540402835291602001916106a4565b820191906000526020600020905b81548152906001019060200180831161068757829003601f168201915b50505050508152602001600182018054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156107465780601f1061071b57610100808354040283529160200191610746565b820191906000526020600020905b81548152906001019060200180831161072957829003601f168201915b5050509183525050600282810180546040805160206001841615610100026000190190931694909404601f810183900483028501830190915280845293810193908301828280156107d85780601f106107ad576101008083540402835291602001916107d8565b820191906000526020600020905b8154815290600101906020018083116107bb57829003601f168201915b50505050508152602001600382015481525050815260200190600101906105fb565b50505050915050610810565b60010161058d565b505b919050565b6060610820826114b2565b6108545750604080518082019091526013815272125b9d985b1a5908115b1958dd1a5bdb881251606a1b6020820152610810565b60005b336001600160a01b03166000848154811061086e57fe5b90600052602060002090600a0201600601828154811061088a57fe5b6000918252602090912001546001600160a01b0316146109075760008054600190920191849081106108b857fe5b90600052602060002090600a0201600601805490508110610902575050604080518082019091526011815270139bc81d9bdd19481cdd589b5a5d1d1959607a1b6020820152610810565b610857565b61090f61173a565b60005b6000858154811061091f57fe5b90600052602060002090600a0201600401805490508111610a0e576000858154811061094757fe5b90600052602060002090600a0201600401818154811061096357fe5b600091825260209182902060408051606081018252600390930290910180546001600160a01b0316835260018101548385015260028101805483518187028101870185528181529495929493860193928301828280156109e257602002820191906000526020600020905b8154815260200190600101908083116109ce575b5050509190925250508151919350506001600160a01b0316331415610a0657610a0e565b600101610912565b506000808581548110610a1d57fe5b600091825260209091206007600a90920201015460ff166001811115610a3f57fe5b1415610aea5760008481548110610a5257fe5b90600052602060002090600a0201600201816020015181548110610a7257fe5b906000526020600020906004020160000160008581548110610a9057fe5b90600052602060002090600a0201600201826020015181548110610ab057fe5b9060005260206000209060040201600101604051602001610ad292919061221a565b60405160208183030381529060405292505050610810565b6040518060200160405280600081525092505050610810565b6000610b0e836114b2565b610b1a57506000610582565b60005b336001600160a01b031660008581548110610b3457fe5b90600052602060002090600a02016005018281548110610b5057fe5b6000918252602090912001546001600160a01b031614610ba8576000805460019092019185908110610b7e57fe5b90600052602060002090600a0201600501805490508110610ba3576000915050610582565b610b1d565b60005b60008581548110610bb857fe5b90600052602060002090600a020160060180549050811015610c3557336001600160a01b031660008681548110610beb57fe5b90600052602060002090600a02016006018281548110610c0757fe5b6000918252602090912001546001600160a01b03161415610c2d57600092505050610582565b600101610bab565b50610c3f84611501565b610c645760405162461bcd60e51b8152600401610c5b906121e3565b60405180910390fd5b610c6d8461152b565b15610c8a5760405162461bcd60e51b8152600401610c5b906121b5565b60008481548110610c9757fe5b60009182526020808320600a9290920290910160040180546001808201835591845292829020865160039094020180546001600160a01b0319166001600160a01b03909416939093178355858201519083015560408501518051869392610d05926002850192910190611764565b5060009150610d119050565b60008581548110610d1e57fe5b600091825260209091206007600a90920201015460ff166001811115610d4057fe5b1415610d8a5760008481548110610d5357fe5b90600052602060002090600a0201600301836020015181548110610d7357fe5b600091825260209091200180546001019055610e00565b60005b836040015151811015610dfe5783604001518181518110610daa57fe5b602002602001015160011415610df65760008581548110610dc757fe5b90600052602060002090600a02016003018181548110610de357fe5b6000918252602090912001805460010190555b600101610d8d565b505b60008481548110610e0d57fe5b600091825260208083206006600a9093020191909101805460018181018355918452919092200180546001600160a01b0319163317905591505092915050565b606080606080606060016000805490500367ffffffffffffffff81118015610e7457600080fd5b50604051908082528060200260200182016040528015610e9e578160200160208202803683370190505b506000549091506060906000190167ffffffffffffffff81118015610ec257600080fd5b50604051908082528060200260200182016040528015610ef657816020015b6060815260200190600190039081610ee15790505b506000549091506060906000190167ffffffffffffffff81118015610f1a57600080fd5b50604051908082528060200260200182016040528015610f44578160200160208202803683370190505b506000549091506060906000190167ffffffffffffffff81118015610f6857600080fd5b50604051908082528060200260200182016040528015610f92578160200160208202803683370190505b50905060005b60005481101561111c5760008181548110610faf57fe5b90600052602060002090600a020160000154858281518110610fcd57fe5b60200260200101818152505060008181548110610fe657fe5b90600052602060002090600a02016001018054600181600116156101000203166002900480601f01602080910402602001604051908101604052809291908181526020018280546001816001161561010002031660029004801561108b5780601f106110605761010080835404028352916020019161108b565b820191906000526020600020905b81548152906001019060200180831161106e57829003601f168201915b505050505084828151811061109c57fe5b6020026020010181905250600081815481106110b457fe5b90600052602060002090600a0201600801548382815181106110d257fe5b602002602001018181525050600081815481106110eb57fe5b90600052602060002090600a02016009015482828151811061110957fe5b6020908102919091010152600101610f98565b50929791965094509092509050565b6000805460001901600155835161114990600290602087019061179f565b506008805486919060ff19166001838181111561116257fe5b0217905550600880546001600160a01b038816610100908102610100600160a81b0319909216919091179091556009849055600a8381556000805460018181018355918052815492027f290decd9548b62a8d60345a988386fc84ba6bc95484008f6362f93160ef3e5638101928355600280549294611214937f290decd9548b62a8d60345a988386fc84ba6bc95484008f6362f93160ef3e5649093019280871615909102600019011681900461181a565b5060028281018054611229928401919061189d565b506003828101805461123e928401919061196b565b506004828101805461125392840191906119aa565b5060058281018054611268928401919061196b565b506006828101805461127d928401919061196b565b50600782810154908201805460ff9092169160ff1916600183818111156112a057fe5b02179055506007828101549082018054610100600160a81b031916610100928390046001600160a01b0316909202919091179055600880830154908201556009918201549101555050600154949350505050565b6000805b60005481101561057c57836000828154811061131057fe5b90600052602060002090600a02016000015414156114aa57336001600160a01b03166000828154811061133f57fe5b60009182526020909120600a909102016007015461010090046001600160a01b0316141561056a576000818154811061137457fe5b90600052602060002090600a020160020160006113919190611a39565b6000818154811061139e57fe5b90600052602060002090600a020160030160006113bb9190611a5d565b60005b835181101561055f57600082815481106113d457fe5b90600052602060002090600a02016002018482815181106113f157fe5b60209081029190910181015182546001810184556000938452928290208151805192946004029091019261142a9284929091019061179f565b506020828101518051611443926001850192019061179f565b506040820151805161145f91600284019160209091019061179f565b50606082015181600301555050600080838154811061147a57fe5b90600052602060002090600a0201600301828154811061149657fe5b6000918252602090912001556001016113be565b6001016112f8565b6000805b6000548110156114f857600081815481106114cd57fe5b90600052602060002090600a0201600001548314156114f0576001915050610810565b6001016114b6565b50600092915050565b6000426000838154811061151157fe5b90600052602060002090600a020160080154109050919050565b6000426000838154811061153b57fe5b90600052602060002090600a020160090154109050919050565b8181808214156115665750506116cf565b60008660028686030586018151811061157b57fe5b602002602001015190505b8183136116a3575b8087848151811061159b57fe5b602002602001015110156115b45760019092019161158e565b8682815181106115c057fe5b60200260200101518110156115db57600019909101906115b4565b81831361169e578682815181106115ee57fe5b602002602001015187848151811061160257fe5b602002602001015188858151811061161657fe5b6020026020010189858151811061162957fe5b602002602001018281525082815250505085828151811061164657fe5b602002602001015186848151811061165a57fe5b602002602001015187858151811061166e57fe5b6020026020010188858151811061168157fe5b602090810291909101019190915252600190920191600019909101905b611586565b818512156116b7576116b787878785611555565b838312156116cb576116cb87878587611555565b5050505b50505050565b82805482825590600052602060002090810192821561172a579160200282015b8281111561172a57825182546001600160a01b0319166001600160a01b039091161782556020909201916001909101906116f5565b50611736929150611a77565b5090565b604051806060016040528060006001600160a01b0316815260200160008152602001606081525090565b82805482825590600052602060002090810192821561172a579160200282015b8281111561172a578251825591602001919060010190611784565b828054600181600116156101000203166002900490600052602060002090601f0160209004810192826117d5576000855561172a565b82601f106117ee57805160ff191683800117855561172a565b8280016001018555821561172a579182018281111561172a578251825591602001919060010190611784565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282611850576000855561172a565b82601f10611861578054855561172a565b8280016001018555821561172a57600052602060002091601f016020900482015b8281111561172a578254825591600101919060010190611882565b82805482825590600052602060002090600402810192821561195f5760005260206000209160040282015b8281111561195f578254839083906118f690829084906002600019610100600184161502019091160461181a565b506001820181600101908054600181600116156101000203166002900461191e92919061181a565b506002828101805461194392848101929160001961010060018316150201160461181a565b50600382015481600301555050916004019190600401906118c8565b50611736929150611a8c565b82805482825590600052602060002090810192821561172a5760005260206000209182018281111561172a578254825591600101919060010190611882565b828054828255906000526020600020906003028101928215611a2d5760005260206000209160030282015b82811115611a2d57825482546001600160a01b0319166001600160a01b03909116178255600180840154908301556002808401805485928592611a1b929184019161196b565b505050916003019190600301906119d5565b50611736929150611acc565b5080546000825560040290600052602060002090810190611a5a9190611a8c565b50565b5080546000825590600052602060002090810190611a5a91905b5b808211156117365760008155600101611a78565b80821115611736576000611aa08282611b01565b611aae600183016000611b01565b611abc600283016000611b01565b5060006003820155600401611a8c565b808211156117365780546001600160a01b0319168155600060018201819055611af86002830182611a5d565b50600301611acc565b50805460018160011615610100020316600290046000825580601f10611b275750611a5a565b601f016020900490600052602060002090810190611a5a9190611a77565b80356001600160a01b038116811461081057600080fd5b600082601f830112611b6c578081fd5b813567ffffffffffffffff811115611b8057fe5b611b93601f8201601f1916602001612289565b9150808252836020828501011115611baa57600080fd5b8060208401602084013760009082016020015292915050565b600080600080600060a08688031215611bda578081fd5b611be386611b45565b9450602086013560028110611bf6578182fd5b9350604086013567ffffffffffffffff811115611c11578182fd5b611c1d88828901611b5c565b9598949750949560608101359550608001359392505050565b600060208284031215611c47578081fd5b5035919050565b60008060408385031215611c60578182fd5b8235915060208084013567ffffffffffffffff811115611c7e578283fd5b8401601f81018613611c8e578283fd5b8035611ca1611c9c826122ad565b612289565b81815283810190838501858402850186018a1015611cbd578687fd5b8694505b83851015611ce657611cd281611b45565b835260019490940193918501918501611cc1565b5080955050505050509250929050565b60008060408385031215611d08578182fd5b8235915060208084013567ffffffffffffffff80821115611d27578384fd5b818601915086601f830112611d3a578384fd5b8135611d48611c9c826122ad565b81815284810190848601875b84811015611e0557813587016080818e03601f19011215611d7357898afd5b611d7d6080612289565b8982013588811115611d8d578b8cfd5b611d9b8f8c83860101611b5c565b825250604082013588811115611daf578b8cfd5b611dbd8f8c83860101611b5c565b8b83015250606082013588811115611dd3578b8cfd5b611de18f8c83860101611b5c565b60408301525060809190910135606082015284529287019290870190600101611d54565b50979a909950975050505050505050565b60008060408385031215611e28578182fd5b8235915060208084013567ffffffffffffffff80821115611e47578384fd5b9085019060608288031215611e5a578384fd5b604051606081018181108382111715611e6f57fe5b604052611e7b83611b45565b81528383013584820152604083013582811115611e96578586fd5b80840193505087601f840112611eaa578485fd5b82359150611eba611c9c836122ad565b82815284810190848601868502860187018b1015611ed6578788fd5b8795505b84861015611ef8578035835260019590950194918601918601611eda565b50604083015250949794965093945050505050565b60008282518085526020808601955080818302840101818601855b84811015611fa257601f19868403018952815160808151818652611f4e82870182611fe9565b9150508582015185820387870152611f668282611fe9565b91505060408083015186830382880152611f808382611fe9565b6060948501519790940196909652505098840198925090830190600101611f28565b5090979650505050505050565b6000815180845260208085019450808401835b83811015611fde57815187529582019590820190600101611fc2565b509495945050505050565b60008151808452815b8181101561200e57602081850181015186830182015201611ff2565b8181111561201f5782602083870101525b50601f01601f19169290920160200192915050565b600081546001808216600081146120525760018114612070576120ae565b60028304607f16865260ff19831660208701526040860193506120ae565b60028304808752612080866122cb565b60005b828110156120a45781546020828b0101528482019150602081019050612083565b8801602001955050505b50505092915050565b6000602082526120ca6020830184611f0d565b9392505050565b6000604082526120e46040830185611f0d565b82810360208401526120f68185611faf565b95945050505050565b6000608082526121126080830187611faf565b602083820381850152818751808452828401915082838202850101838a01865b8381101561216057601f1987840301855261214e838351611fe9565b94860194925090850190600101612132565b50508681036040880152612174818a611faf565b945050505050828103606084015261218c8185611faf565b979650505050505050565b901515815260200190565b6000602082526120ca6020830184611fe9565b602080825260149082015273159bdd1a5b99c8185b1c9958591e48195b99195960621b604082015260600190565b6020808252601a908201527f566f74696e6720686173206e6f74207374617274656420796574000000000000604082015260600190565b600060808252600e60808301526d02cb7ba903b37ba32b2103337b9160951b60a083015260c0602083015261225260c0830185612034565b82810380604085015260018252600160fd1b6020830152604081016060850152506120f66040820185612034565b90815260200190565b60405181810167ffffffffffffffff811182821017156122a557fe5b604052919050565b600067ffffffffffffffff8211156122c157fe5b5060209081020190565b6000908152602090209056fea2646970667358221220dccc94af92268dea6d1542de256203e1d80cc0096c435917755fc2559883249b64736f6c63430007050033";
        public Bvs_backendDeploymentBase() : base(BYTECODE) { }
        public Bvs_backendDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class CountVotesFunction : CountVotesFunctionBase { }

    [Function("countVotes", typeof(CountVotesOutputDTO))]
    public class CountVotesFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "electionId", 1)]
        public virtual BigInteger ElectionId { get; set; }
    }

    public partial class CreateElectionFunction : CreateElectionFunctionBase { }

    [Function("createElection", "uint256")]
    public class CreateElectionFunctionBase : FunctionMessage
    {
        [Parameter("address", "electionAdminAddress", 1)]
        public virtual string ElectionAdminAddress { get; set; }
        [Parameter("uint8", "electionVotingSystem", 2)]
        public virtual byte ElectionVotingSystem { get; set; }
        [Parameter("string", "electionName", 3)]
        public virtual string ElectionName { get; set; }
        [Parameter("uint256", "electionStartTimestamp", 4)]
        public virtual BigInteger ElectionStartTimestamp { get; set; }
        [Parameter("uint256", "electionEndTimestamp", 5)]
        public virtual BigInteger ElectionEndTimestamp { get; set; }
    }

    public partial class GetElectionInformationFunction : GetElectionInformationFunctionBase { }

    [Function("getElectionInformation", typeof(GetElectionInformationOutputDTO))]
    public class GetElectionInformationFunctionBase : FunctionMessage
    {

    }

    public partial class GetElectoralListFunction : GetElectoralListFunctionBase { }

    [Function("getElectoralList", typeof(GetElectoralListOutputDTO))]
    public class GetElectoralListFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "electionId", 1)]
        public virtual BigInteger ElectionId { get; set; }
    }

    public partial class GetVoteFunction : GetVoteFunctionBase { }

    [Function("getVote", "string")]
    public class GetVoteFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "electionId", 1)]
        public virtual BigInteger ElectionId { get; set; }
    }

    public partial class ReplaceElectoralListFunction : ReplaceElectoralListFunctionBase { }

    [Function("replaceElectoralList", "bool")]
    public class ReplaceElectoralListFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "electionId", 1)]
        public virtual BigInteger ElectionId { get; set; }
        [Parameter("tuple[]", "newElectoralList", 2)]
        public virtual List<Candidate> NewElectoralList { get; set; }
    }

    public partial class ReplaceListOfEligibleVotersFunction : ReplaceListOfEligibleVotersFunctionBase { }

    [Function("replaceListOfEligibleVoters", "bool")]
    public class ReplaceListOfEligibleVotersFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "electionId", 1)]
        public virtual BigInteger ElectionId { get; set; }
        [Parameter("address[]", "newEligibleVoterList", 2)]
        public virtual List<string> NewEligibleVoterList { get; set; }
    }

    public partial class VoteFunction : VoteFunctionBase { }

    [Function("vote", "bool")]
    public class VoteFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "electionId", 1)]
        public virtual BigInteger ElectionId { get; set; }
        [Parameter("tuple", "ballot", 2)]
        public virtual Ballot Ballot { get; set; }
    }

    public partial class CountVotesOutputDTO : CountVotesOutputDTOBase { }

    [FunctionOutput]
    public class CountVotesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("tuple[]", "candidateRanking", 1)]
        public virtual List<Candidate> CandidateRanking { get; set; }
        [Parameter("uint256[]", "voteCount", 2)]
        public virtual List<BigInteger> VoteCount { get; set; }
    }



    public partial class GetElectionInformationOutputDTO : GetElectionInformationOutputDTOBase { }

    [FunctionOutput]
    public class GetElectionInformationOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256[]", "", 1)]
        public virtual List<BigInteger> ReturnValue1 { get; set; }
        [Parameter("string[]", "", 2)]
        public virtual List<string> ReturnValue2 { get; set; }
        [Parameter("uint256[]", "", 3)]
        public virtual List<BigInteger> ReturnValue3 { get; set; }
        [Parameter("uint256[]", "", 4)]
        public virtual List<BigInteger> ReturnValue4 { get; set; }
    }

    public partial class GetElectoralListOutputDTO : GetElectoralListOutputDTOBase { }

    [FunctionOutput]
    public class GetElectoralListOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("tuple[]", "candidates", 1)]
        public virtual List<Candidate> Candidates { get; set; }
    }

    public partial class GetVoteOutputDTO : GetVoteOutputDTOBase { }

    [FunctionOutput]
    public class GetVoteOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }






}
