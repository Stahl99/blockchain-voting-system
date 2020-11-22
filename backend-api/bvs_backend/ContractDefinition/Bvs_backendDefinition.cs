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
        public static string BYTECODE = "608060405234801561001057600080fd5b5061227a806100206000396000f3fe608060405234801561001057600080fd5b50600436106100885760003560e01c8063da2b86d11161005b578063da2b86d114610117578063ef8e51e61461012a578063f35953cc1461013f578063f7f095471461015f57610088565b80631840f0ca1461008d5780632df0f580146100b75780634aea2f6a146100d75780635a55c1f0146100f7575b600080fd5b6100a061009b366004611bc9565b610172565b6040516100ae92919061202a565b60405180910390f35b6100ca6100c5366004611be1565b6104b7565b6040516100ae91906120fb565b6100ea6100e5366004611bc9565b610585565b6040516100ae9190612010565b61010a610105366004611bc9565b610812565b6040516100ae9190612106565b6100ca610125366004611da9565b610b00565b610132610e4a565b6040516100ae9190612073565b61015261014d366004611b56565b611043565b6040516100ae91906121ed565b6100ca61016d366004611c89565b61120c565b60608061017e836113ca565b1580610190575061018e83611419565b155b1561019a576104b2565b6060600084815481106101a957fe5b90600052602060002090600a0201600201805480602002602001604051908101604052809291908181526020016000905b828210156103d95760008481526020908190206040805160048602909201805460026001821615610100026000190190911604601f8101859004909402830160a090810190925260808301848152929390928492909184918401828280156102835780601f1061025857610100808354040283529160200191610283565b820191906000526020600020905b81548152906001019060200180831161026657829003601f168201915b50505050508152602001600182018054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156103255780601f106102fa57610100808354040283529160200191610325565b820191906000526020600020905b81548152906001019060200180831161030857829003601f168201915b5050509183525050600282810180546040805160206001841615610100026000190190931694909404601f810183900483028501830190915280845293810193908301828280156103b75780601f1061038c576101008083540402835291602001916103b7565b820191906000526020600020905b81548152906001019060200180831161039a57829003601f168201915b50505050508152602001600382015481525050815260200190600101906101da565b5050505090506060600085815481106103ee57fe5b90600052602060002090600a020160030180548060200260200160405190810160405280929190818152602001828054801561044957602002820191906000526020600020905b815481526020019060010190808311610435575b5050505050905061045985611443565b15806104925750600080868154811061046e57fe5b600091825260209091206007600a90920201015460ff16600181111561049057fe5b145b156104af576104a881836000600185510361146d565b50506104b2565b50505b915091565b6000805b6000548110156105795783600082815481106104d357fe5b90600052602060002090600a020160000154141561057157336001600160a01b03166000828154811061050257fe5b60009182526020909120600a909102016007015461010090046001600160a01b0316141561056757826000828154811061053857fe5b90600052602060002090600a0201600501908051906020019061055c9291906115ed565b50600191505061057f565b600091505061057f565b6001016104bb565b50600090505b92915050565b606060005b60005481101561080b57600081815481106105a157fe5b90600052602060002090600a02016000015483141561080357600081815481106105c757fe5b90600052602060002090600a0201600201805480602002602001604051908101604052809291908181526020016000905b828210156107f75760008481526020908190206040805160048602909201805460026001821615610100026000190190911604601f8101859004909402830160a090810190925260808301848152929390928492909184918401828280156106a15780601f10610676576101008083540402835291602001916106a1565b820191906000526020600020905b81548152906001019060200180831161068457829003601f168201915b50505050508152602001600182018054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156107435780601f1061071857610100808354040283529160200191610743565b820191906000526020600020905b81548152906001019060200180831161072657829003601f168201915b5050509183525050600282810180546040805160206001841615610100026000190190931694909404601f810183900483028501830190915280845293810193908301828280156107d55780601f106107aa576101008083540402835291602001916107d5565b820191906000526020600020905b8154815290600101906020018083116107b857829003601f168201915b50505050508152602001600382015481525050815260200190600101906105f8565b5050505091505061080d565b60010161058a565b505b919050565b606061081d826113ca565b6108515750604080518082019091526013815272125b9d985b1a5908115b1958dd1a5bdb881251606a1b602082015261080d565b60005b336001600160a01b03166000848154811061086b57fe5b90600052602060002090600a0201600601828154811061088757fe5b6000918252602090912001546001600160a01b0316146109045760008054600190920191849081106108b557fe5b90600052602060002090600a02016006018054905081106108ff575050604080518082019091526011815270139bc81d9bdd19481cdd589b5a5d1d1959607a1b602082015261080d565b610854565b61090c611652565b60005b6000858154811061091c57fe5b90600052602060002090600a0201600401805490508111610a0b576000858154811061094457fe5b90600052602060002090600a0201600401818154811061096057fe5b600091825260209182902060408051606081018252600390930290910180546001600160a01b0316835260018101548385015260028101805483518187028101870185528181529495929493860193928301828280156109df57602002820191906000526020600020905b8154815260200190600101908083116109cb575b5050509190925250508151919350506001600160a01b0316331415610a0357610a0b565b60010161090f565b506000808581548110610a1a57fe5b600091825260209091206007600a90920201015460ff166001811115610a3c57fe5b1415610ae75760008481548110610a4f57fe5b90600052602060002090600a0201600201816020015181548110610a6f57fe5b906000526020600020906004020160000160008581548110610a8d57fe5b90600052602060002090600a0201600201826020015181548110610aad57fe5b9060005260206000209060040201600101604051602001610acf92919061217e565b6040516020818303038152906040529250505061080d565b604051806020016040528060008152509250505061080d565b6000610b0b836113ca565b610b175750600061057f565b60005b336001600160a01b031660008581548110610b3157fe5b90600052602060002090600a02016005018281548110610b4d57fe5b6000918252602090912001546001600160a01b031614610ba5576000805460019092019185908110610b7b57fe5b90600052602060002090600a0201600501805490508110610ba057600091505061057f565b610b1a565b60005b60008581548110610bb557fe5b90600052602060002090600a020160060180549050811015610c3257336001600160a01b031660008681548110610be857fe5b90600052602060002090600a02016006018281548110610c0457fe5b6000918252602090912001546001600160a01b03161415610c2a5760009250505061057f565b600101610ba8565b50610c3c84611419565b610c615760405162461bcd60e51b8152600401610c5890612147565b60405180910390fd5b610c6a84611443565b15610c875760405162461bcd60e51b8152600401610c5890612119565b60008481548110610c9457fe5b60009182526020808320600a9290920290910160040180546001808201835591845292829020865160039094020180546001600160a01b0319166001600160a01b03909416939093178355858201519083015560408501518051869392610d0292600285019291019061167c565b5060009150610d0e9050565b60008581548110610d1b57fe5b600091825260209091206007600a90920201015460ff166001811115610d3d57fe5b1415610d875760008481548110610d5057fe5b90600052602060002090600a0201600301836020015181548110610d7057fe5b600091825260209091200180546001019055610dfd565b60005b836040015151811015610dfb5783604001518181518110610da757fe5b602002602001015160011415610df35760008581548110610dc457fe5b90600052602060002090600a02016003018181548110610de057fe5b6000918252602090912001805460010190555b600101610d8a565b505b60008481548110610e0a57fe5b600091825260208083206006600a9093020191909101805460018181018355918452919092200180546001600160a01b0319163317905591505092915050565b60005460609081906000190167ffffffffffffffff81118015610e6c57600080fd5b50604051908082528060200260200182016040528015610ea657816020015b610e936116c3565b815260200190600190039081610e8b5790505b50905060005b60005481101561103d5760008181548110610ec357fe5b90600052602060002090600a020160000154828281518110610ee157fe5b6020908102919091010151526000805482908110610efb57fe5b90600052602060002090600a02016001018054600181600116156101000203166002900480601f016020809104026020016040519081016040528092919081815260200182805460018160011615610100020316600290048015610fa05780601f10610f7557610100808354040283529160200191610fa0565b820191906000526020600020905b815481529060010190602001808311610f8357829003601f168201915b5050505050828281518110610fb157fe5b60200260200101516020018190525060008181548110610fcd57fe5b90600052602060002090600a020160080154828281518110610feb57fe5b602002602001015160400181815250506000818154811061100857fe5b90600052602060002090600a02016009015482828151811061102657fe5b602090810291909101015160600152600101610eac565b50905090565b600080546000190160015583516110619060029060208701906116eb565b506008805486919060ff19166001838181111561107a57fe5b0217905550600880546001600160a01b038816610100908102610100600160a81b0319909216919091179091556009849055600a8381556000805460018181018355918052815492027f290decd9548b62a8d60345a988386fc84ba6bc95484008f6362f93160ef3e563810192835560028054929461112c937f290decd9548b62a8d60345a988386fc84ba6bc95484008f6362f93160ef3e56490930192808716159091026000190116819004611758565b506002828101805461114192840191906117cd565b5060038281018054611156928401919061189b565b506004828101805461116b92840191906118da565b50600582810180546111809284019190611969565b50600682810180546111959284019190611969565b50600782810154908201805460ff9092169160ff1916600183818111156111b857fe5b02179055506007828101549082018054610100600160a81b031916610100928390046001600160a01b0316909202919091179055600880830154908201556009918201549101555050600154949350505050565b6000805b60005481101561057957836000828154811061122857fe5b90600052602060002090600a02016000015414156113c257336001600160a01b03166000828154811061125757fe5b60009182526020909120600a909102016007015461010090046001600160a01b03161415610567576000818154811061128c57fe5b90600052602060002090600a020160020160006112a991906119a9565b600081815481106112b657fe5b90600052602060002090600a020160030160006112d391906119cd565b60005b835181101561055c57600082815481106112ec57fe5b90600052602060002090600a020160020184828151811061130957fe5b602090810291909101810151825460018101845560009384529282902081518051929460040290910192611342928492909101906116eb565b50602082810151805161135b92600185019201906116eb565b50604082015180516113779160028401916020909101906116eb565b50606082015181600301555050600080838154811061139257fe5b90600052602060002090600a020160030182815481106113ae57fe5b6000918252602090912001556001016112d6565b600101611210565b6000805b60005481101561141057600081815481106113e557fe5b90600052602060002090600a02016000015483141561140857600191505061080d565b6001016113ce565b50600092915050565b6000426000838154811061142957fe5b90600052602060002090600a020160080154109050919050565b6000426000838154811061145357fe5b90600052602060002090600a020160090154109050919050565b81818082141561147e5750506115e7565b60008660028686030586018151811061149357fe5b602002602001015190505b8183136115bb575b808784815181106114b357fe5b602002602001015110156114cc576001909201916114a6565b8682815181106114d857fe5b60200260200101518110156114f357600019909101906114cc565b8183136115b65786828151811061150657fe5b602002602001015187848151811061151a57fe5b602002602001015188858151811061152e57fe5b6020026020010189858151811061154157fe5b602002602001018281525082815250505085828151811061155e57fe5b602002602001015186848151811061157257fe5b602002602001015187858151811061158657fe5b6020026020010188858151811061159957fe5b602090810291909101019190915252600190920191600019909101905b61149e565b818512156115cf576115cf8787878561146d565b838312156115e3576115e38787858761146d565b5050505b50505050565b828054828255906000526020600020908101928215611642579160200282015b8281111561164257825182546001600160a01b0319166001600160a01b0390911617825560209092019160019091019061160d565b5061164e9291506119eb565b5090565b604051806060016040528060006001600160a01b0316815260200160008152602001606081525090565b8280548282559060005260206000209081019282156116b7579160200282015b828111156116b757825182559160200191906001019061169c565b5061164e929150611a0a565b6040518060800160405280600081526020016060815260200160008152602001600081525090565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061172c57805160ff19168380011785556116b7565b828001600101855582156116b757918201828111156116b757825182559160200191906001019061169c565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061179157805485556116b7565b828001600101855582156116b757600052602060002091601f016020900482015b828111156116b75782548255916001019190600101906117b2565b82805482825590600052602060002090600402810192821561188f5760005260206000209160040282015b8281111561188f57825483908390611826908290849060026000196101006001841615020190911604611758565b506001820181600101908054600181600116156101000203166002900461184e929190611758565b5060028281018054611873928481019291600019610100600183161502011604611758565b50600382015481600301555050916004019190600401906117f8565b5061164e929150611a1f565b8280548282559060005260206000209081019282156116b7576000526020600020918201828111156116b75782548255916001019190600101906117b2565b82805482825590600052602060002090600302810192821561195d5760005260206000209160030282015b8281111561195d57825482546001600160a01b0319166001600160a01b0390911617825560018084015490830155600280840180548592859261194b929184019161189b565b50505091600301919060030190611905565b5061164e929150611a5f565b8280548282559060005260206000209081019282156116425760005260206000209182015b8281111561164257825482559160010191906001019061198e565b50805460008255600402906000526020600020908101906119ca9190611a1f565b50565b50805460008255906000526020600020908101906119ca9190611a0a565b5b8082111561164e5780546001600160a01b03191681556001016119ec565b5b8082111561164e5760008155600101611a0b565b8082111561164e576000611a338282611a94565b611a41600183016000611a94565b611a4f600283016000611a94565b5060006003820155600401611a1f565b8082111561164e5780546001600160a01b0319168155600060018201819055611a8b60028301826119cd565b50600301611a5f565b50805460018160011615610100020316600290046000825580601f10611aba57506119ca565b601f0160209004906000526020600020908101906119ca9190611a0a565b80356001600160a01b038116811461080d57600080fd5b600082601f830112611aff578081fd5b813567ffffffffffffffff811115611b1357fe5b611b26601f8201601f19166020016121f6565b9150808252836020828501011115611b3d57600080fd5b8060208401602084013760009082016020015292915050565b600080600080600060a08688031215611b6d578081fd5b611b7686611ad8565b9450602086013560028110611b89578182fd5b9350604086013567ffffffffffffffff811115611ba4578182fd5b611bb088828901611aef565b9598949750949560608101359550608001359392505050565b600060208284031215611bda578081fd5b5035919050565b60008060408385031215611bf3578182fd5b8235915060208084013567ffffffffffffffff811115611c11578283fd5b8401601f81018613611c21578283fd5b8035611c34611c2f8261221a565b6121f6565b81815283810190838501858402850186018a1015611c50578687fd5b8694505b83851015611c7957611c6581611ad8565b835260019490940193918501918501611c54565b5080955050505050509250929050565b60008060408385031215611c9b578182fd5b8235915060208084013567ffffffffffffffff80821115611cba578384fd5b818601915086601f830112611ccd578384fd5b8135611cdb611c2f8261221a565b81815284810190848601875b84811015611d9857813587016080818e03601f19011215611d0657898afd5b611d1060806121f6565b8982013588811115611d20578b8cfd5b611d2e8f8c83860101611aef565b825250604082013588811115611d42578b8cfd5b611d508f8c83860101611aef565b8b83015250606082013588811115611d66578b8cfd5b611d748f8c83860101611aef565b60408301525060809190910135606082015284529287019290870190600101611ce7565b50979a909950975050505050505050565b60008060408385031215611dbb578182fd5b8235915060208084013567ffffffffffffffff80821115611dda578384fd5b9085019060608288031215611ded578384fd5b604051606081018181108382111715611e0257fe5b604052611e0e83611ad8565b81528383013584820152604083013582811115611e29578586fd5b80840193505087601f840112611e3d578485fd5b82359150611e4d611c2f8361221a565b82815284810190848601868502860187018b1015611e69578788fd5b8795505b84861015611e8b578035835260019590950194918601918601611e6d565b50604083015250949794965093945050505050565b60008282518085526020808601955080818302840101818601855b84811015611f3557601f19868403018952815160808151818652611ee182870182611f42565b9150508582015185820387870152611ef98282611f42565b91505060408083015186830382880152611f138382611f42565b6060948501519790940196909652505098840198925090830190600101611ebb565b5090979650505050505050565b60008151808452815b81811015611f6757602081850181015186830182015201611f4b565b81811115611f785782602083870101525b50601f01601f19169290920160200192915050565b60008154600180821660008114611fab5760018114611fc957612007565b60028304607f16865260ff1983166020870152604086019350612007565b60028304808752611fd986612238565b60005b82811015611ffd5781546020828b0101528482019150602081019050611fdc565b8801602001955050505b50505092915050565b6000602082526120236020830184611ea0565b9392505050565b60006040825261203d6040830185611ea0565b828103602084810191909152845180835285820192820190845b81811015611f3557845183529383019391830191600101612057565b60208082528251828201819052600091906040908185019080840286018301878501865b838110156120ed57603f19898403018552815160808151855288820151818a8701526120c582870182611f42565b838a0151878b0152606093840151939096019290925250509386019390860190600101612097565b509098975050505050505050565b901515815260200190565b6000602082526120236020830184611f42565b602080825260149082015273159bdd1a5b99c8185b1c9958591e48195b99195960621b604082015260600190565b6020808252601a908201527f566f74696e6720686173206e6f74207374617274656420796574000000000000604082015260600190565b600060808252600e60808301526d02cb7ba903b37ba32b2103337b9160951b60a083015260c060208301526121b660c0830185611f8d565b82810380604085015260018252600160fd1b6020830152604081016060850152506121e46040820185611f8d565b95945050505050565b90815260200190565b60405181810167ffffffffffffffff8111828210171561221257fe5b604052919050565b600067ffffffffffffffff82111561222e57fe5b5060209081020190565b6000908152602090209056fea26469706673582212208ea3c1c0b982a58b8a2bf93fad8bb1aa1c0d5c99865c502db9c57d694cbedf9d64736f6c63430007020033";
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
        [Parameter("tuple[]", "", 1)]
        public virtual List<TmpElectionObject> ReturnValue1 { get; set; }
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
