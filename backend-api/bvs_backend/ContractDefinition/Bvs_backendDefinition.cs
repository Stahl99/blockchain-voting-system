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
        public static string BYTECODE = "608060405234801561001057600080fd5b5061267e806100206000396000f3fe608060405234801561001057600080fd5b506004361061009e5760003560e01c8063c7ec836711610066578063c7ec836714610142578063da2b86d114610157578063ef8e51e61461016a578063f35953cc1461017f578063f7f09547146101945761009e565b80631840f0ca146100a35780632df0f580146100cd5780634aea2f6a146100ed5780635a55c1f01461010d578063934422a91461012d575b600080fd5b6100b66100b1366004611fbd565b6101a7565b6040516100c492919061247e565b60405180910390f35b6100e06100db366004611fd5565b610557565b6040516100c49190612564565b6101006100fb366004611fbd565b610625565b6040516100c49190612464565b61012061011b366004611fbd565b6108b2565b6040516100c4919061256f565b610135610ba0565b6040516100c49190612404565b61014a610cde565b6040516100c491906125f1565b61014a61016536600461219d565b610ce8565b610172611006565b6040516100c491906124c7565b61019261018d366004611f4a565b611255565b005b6100e06101a236600461207d565b611413565b6060806101b383611687565b15806101c557506101c3836116d6565b155b156101cf57610552565b6060600084815481106101de57fe5b90600052602060002090600a0201600201805480602002602001604051908101604052809291908181526020016000905b8282101561040e5760008481526020908190206040805160048602909201805460026001821615610100026000190190911604601f8101859004909402830160a090810190925260808301848152929390928492909184918401828280156102b85780601f1061028d576101008083540402835291602001916102b8565b820191906000526020600020905b81548152906001019060200180831161029b57829003601f168201915b50505050508152602001600182018054600181600116156101000203166002900480601f01602080910402602001604051908101604052809291908181526020018280546001816001161561010002031660029004801561035a5780601f1061032f5761010080835404028352916020019161035a565b820191906000526020600020905b81548152906001019060200180831161033d57829003601f168201915b5050509183525050600282810180546040805160206001841615610100026000190190931694909404601f810183900483028501830190915280845293810193908301828280156103ec5780601f106103c1576101008083540402835291602001916103ec565b820191906000526020600020905b8154815290600101906020018083116103cf57829003601f168201915b505050505081526020016003820154815250508152602001906001019061020f565b50505050905060606000858154811061042357fe5b90600052602060002090600a020160030180548060200260200160405190810160405280929190818152602001828054801561047e57602002820191906000526020600020905b81548152602001906001019080831161046a575b5050505050905061048e85611700565b15806104c7575060008086815481106104a357fe5b600091825260209091206007600a90920201015460ff1660018111156104c557fe5b145b156104e4576104dd81836000600185510361172a565b5050610552565b6104ed85611700565b80610526575060016000868154811061050257fe5b600091825260209091206007600a90920201015460ff16600181111561052457fe5b145b1561054f575b610535856118aa565b61054f5761054a81836000600185510361172a565b61052c565b50505b915091565b6000805b60005481101561061957836000828154811061057357fe5b90600052602060002090600a020160000154141561061157336001600160a01b0316600082815481106105a257fe5b60009182526020909120600a909102016007015461010090046001600160a01b031614156106075782600082815481106105d857fe5b90600052602060002090600a020160050190805190602001906105fc929190611a23565b50600191505061061f565b600091505061061f565b60010161055b565b50600090505b92915050565b606060005b6000548110156108ab576000818154811061064157fe5b90600052602060002090600a0201600001548314156108a3576000818154811061066757fe5b90600052602060002090600a0201600201805480602002602001604051908101604052809291908181526020016000905b828210156108975760008481526020908190206040805160048602909201805460026001821615610100026000190190911604601f8101859004909402830160a090810190925260808301848152929390928492909184918401828280156107415780601f1061071657610100808354040283529160200191610741565b820191906000526020600020905b81548152906001019060200180831161072457829003601f168201915b50505050508152602001600182018054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156107e35780601f106107b8576101008083540402835291602001916107e3565b820191906000526020600020905b8154815290600101906020018083116107c657829003601f168201915b5050509183525050600282810180546040805160206001841615610100026000190190931694909404601f810183900483028501830190915280845293810193908301828280156108755780601f1061084a57610100808354040283529160200191610875565b820191906000526020600020905b81548152906001019060200180831161085857829003601f168201915b5050505050815260200160038201548152505081526020019060010190610698565b505050509150506108ad565b60010161062a565b505b919050565b60606108bd82611687565b6108f15750604080518082019091526013815272125b9d985b1a5908115b1958dd1a5bdb881251606a1b60208201526108ad565b60005b336001600160a01b03166000848154811061090b57fe5b90600052602060002090600a0201600601828154811061092757fe5b6000918252602090912001546001600160a01b0316146109a457600080546001909201918490811061095557fe5b90600052602060002090600a020160060180549050811061099f575050604080518082019091526011815270139bc81d9bdd19481cdd589b5a5d1d1959607a1b60208201526108ad565b6108f4565b6109ac611a88565b60005b600085815481106109bc57fe5b90600052602060002090600a0201600401805490508111610aab57600085815481106109e457fe5b90600052602060002090600a02016004018181548110610a0057fe5b600091825260209182902060408051606081018252600390930290910180546001600160a01b031683526001810154838501526002810180548351818702810187018552818152949592949386019392830182828015610a7f57602002820191906000526020600020905b815481526020019060010190808311610a6b575b5050509190925250508151919350506001600160a01b0316331415610aa357610aab565b6001016109af565b506000808581548110610aba57fe5b600091825260209091206007600a90920201015460ff166001811115610adc57fe5b1415610b875760008481548110610aef57fe5b90600052602060002090600a0201600201816020015181548110610b0f57fe5b906000526020600020906004020160000160008581548110610b2d57fe5b90600052602060002090600a0201600201826020015181548110610b4d57fe5b9060005260206000209060040201600101604051602001610b6f929190612582565b604051602081830303815290604052925050506108ad565b60405180602001604052806000815250925050506108ad565b600054606090819067ffffffffffffffff81118015610bbe57600080fd5b50604051908082528060200260200182016040528015610bf257816020015b6060815260200190600190039081610bdd5790505b50905060005b600054811015610cd85760008181548110610c0f57fe5b90600052602060002090600a02016001018054600181600116156101000203166002900480601f016020809104026020016040519081016040528092919081815260200182805460018160011615610100020316600290048015610cb45780601f10610c8957610100808354040283529160200191610cb4565b820191906000526020600020905b815481529060010190602001808311610c9757829003601f168201915b5050505050828281518110610cc557fe5b6020908102919091010152600101610bf8565b50905090565b6000546000190190565b6000610cf383611687565b610cff5750600061061f565b60005b336001600160a01b031660008581548110610d1957fe5b90600052602060002090600a02016005018281548110610d3557fe5b6000918252602090912001546001600160a01b031614610d8d576000805460019092019185908110610d6357fe5b90600052602060002090600a0201600501805490508110610d8857600091505061061f565b610d02565b60005b60008581548110610d9d57fe5b90600052602060002090600a020160060180549050811015610e1a57336001600160a01b031660008681548110610dd057fe5b90600052602060002090600a02016006018281548110610dec57fe5b6000918252602090912001546001600160a01b03161415610e125760009250505061061f565b600101610d90565b50610e24846116d6565b1580610e345750610e3484611700565b15610e4357600091505061061f565b60008481548110610e5057fe5b60009182526020808320600a9290920290910160040180546001808201835591845292829020865160039094020180546001600160a01b0319166001600160a01b03909416939093178355858201519083015560408501518051869392610ebe926002850192910190611ab2565b5060009150610eca9050565b60008581548110610ed757fe5b600091825260209091206007600a90920201015460ff166001811115610ef957fe5b1415610f435760008481548110610f0c57fe5b90600052602060002090600a0201600301836020015181548110610f2c57fe5b600091825260209091200180546001019055610fb9565b60005b836040015151811015610fb75783604001518181518110610f6357fe5b602002602001015160011415610faf5760008581548110610f8057fe5b90600052602060002090600a02016003018181548110610f9c57fe5b6000918252602090912001805460010190555b600101610f46565b505b60008481548110610fc657fe5b600091825260208083206006600a9093020191909101805460018181018355918452919092200180546001600160a01b0319163317905591505092915050565b600054606090819067ffffffffffffffff8111801561102457600080fd5b5060405190808252806020026020018201604052801561105e57816020015b61104b611aed565b8152602001906001900390816110435790505b50905060005b600054811015610cd8576000818154811061107b57fe5b90600052602060002090600a02016000015482828151811061109957fe5b60209081029190910101515260008054829081106110b357fe5b90600052602060002090600a02016001018054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156111585780601f1061112d57610100808354040283529160200191611158565b820191906000526020600020905b81548152906001019060200180831161113b57829003601f168201915b505050505082828151811061116957fe5b6020026020010151602001819052506000818154811061118557fe5b90600052602060002090600a0201600801548282815181106111a357fe5b60200260200101516040018181525050600081815481106111c057fe5b90600052602060002090600a0201600901548282815181106111de57fe5b60200260200101516060018181525050600081815481106111fb57fe5b90600052602060002090600a020160070160009054906101000a900460ff1682828151811061122657fe5b602002602001015160800190600181111561123d57fe5b9081600181111561124a57fe5b905250600101611064565b600054600155825161126e906002906020860190611b26565b506008805485919060ff19166001838181111561128757fe5b0217905550600880546001600160a01b038716610100908102610100600160a81b0319909216919091179091556009839055600a8281556000805460018181018355918052815492027f290decd9548b62a8d60345a988386fc84ba6bc95484008f6362f93160ef3e5638101928355600280549294611339937f290decd9548b62a8d60345a988386fc84ba6bc95484008f6362f93160ef3e56490930192808716159091026000190116819004611ba1565b506002828101805461134e9284019190611c24565b50600382810180546113639284019190611cf2565b50600482810180546113789284019190611d31565b506005828101805461138d9284019190611cf2565b50600682810180546113a29284019190611cf2565b50600782810154908201805460ff9092169160ff1916600183818111156113c557fe5b02179055506007828101549082018054610100600160a81b031916610100928390046001600160a01b0316909202919091179055600880830154908201556009918201549101555050505050565b6000805b60005481101561061957836000828154811061142f57fe5b90600052602060002090600a020160000154141561167f57336001600160a01b03166000828154811061145e57fe5b60009182526020909120600a909102016007015461010090046001600160a01b03161415610607576000818154811061149357fe5b90600052602060002090600a020160020160006114b09190611dc0565b600081815481106114bd57fe5b90600052602060002090600a020160030160006114da9190611de4565b60005b83518110156105fc578381815181106114f257fe5b60209081029190910181015151805161150f92600b920190611b26565b5083818151811061151c57fe5b602090810291909101810151810151805161153b92600c920190611b26565b5083818151811061154857fe5b60209081029190910181015160400151805161156892600d920190611b26565b5083818151811061157557fe5b602002602001015160600151600b600301819055506000828154811061159757fe5b600091825260208083206002600a90930201820180546001818101835591855291909320600b805490946004909302909101926115e892849286926101009282161592909202600019011604611ba1565b5060018201816001019080546001816001161561010002031660029004611610929190611ba1565b5060028281018054611635928481019291600019610100600183161502011604611ba1565b50600391820154910155600080548390811061164d57fe5b600091825260208083206003600a909302019190910180546001818101835591845291832090910191909155016114dd565b600101611417565b6000805b6000548110156116cd57600081815481106116a257fe5b90600052602060002090600a0201600001548314156116c55760019150506108ad565b60010161168b565b50600092915050565b600042600083815481106116e657fe5b90600052602060002090600a020160080154109050919050565b6000426000838154811061171057fe5b90600052602060002090600a020160090154109050919050565b81818082141561173b5750506118a4565b60008660028686030586018151811061175057fe5b602002602001015190505b818313611878575b8087848151811061177057fe5b6020026020010151101561178957600190920191611763565b86828151811061179557fe5b60200260200101518110156117b05760001990910190611789565b818313611873578682815181106117c357fe5b60200260200101518784815181106117d757fe5b60200260200101518885815181106117eb57fe5b602002602001018985815181106117fe57fe5b602002602001018281525082815250505085828151811061181b57fe5b602002602001015186848151811061182f57fe5b602002602001015187858151811061184357fe5b6020026020010188858151811061185657fe5b602090810291909101019190915252600190920191600019909101905b61175b565b8185121561188c5761188c8787878561172a565b838312156118a0576118a08787858761172a565b5050505b50505050565b6000806000905060008084815481106118bf57fe5b90600052602060002090600a020160040180549050905060006060600086815481106118e757fe5b90600052602060002090600a020160030180548060200260200160405190810160405280929190818152602001828054801561194257602002820191906000526020600020905b81548152602001906001019080831161192e575b50939450600193505050505b6000878154811061195b57fe5b90600052602060002090600a0201600201805490508110156119eb5781838151811061198357fe5b602002602001015182828151811061199757fe5b6020026020010151106119e3578092508183815181106119b357fe5b60200260200101518282815181106119c757fe5b602002602001015114156119de57600194506119e3565b600094505b60010161194e565b50600283048183815181106119fc57fe5b602002602001015110611a165760019450505050506108ad565b60009450505050506108ad565b828054828255906000526020600020908101928215611a78579160200282015b82811115611a7857825182546001600160a01b0319166001600160a01b03909116178255602090920191600190910190611a43565b50611a84929150611dfe565b5090565b604051806060016040528060006001600160a01b0316815260200160008152602001606081525090565b828054828255906000526020600020908101928215611a78579160200282015b82811115611a78578251825591602001919060010190611ad2565b6040518060a001604052806000815260200160608152602001600081526020016000815260200160006001811115611b2157fe5b905290565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282611b5c5760008555611a78565b82601f10611b7557805160ff1916838001178555611a78565b82800160010185558215611a785791820182811115611a78578251825591602001919060010190611ad2565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282611bd75760008555611a78565b82601f10611be85780548555611a78565b82800160010185558215611a7857600052602060002091601f016020900482015b82811115611a78578254825591600101919060010190611c09565b828054828255906000526020600020906004028101928215611ce65760005260206000209160040282015b82811115611ce657825483908390611c7d908290849060026000196101006001841615020190911604611ba1565b5060018201816001019080546001816001161561010002031660029004611ca5929190611ba1565b5060028281018054611cca928481019291600019610100600183161502011604611ba1565b5060038201548160030155505091600401919060040190611c4f565b50611a84929150611e13565b828054828255906000526020600020908101928215611a7857600052602060002091820182811115611a78578254825591600101919060010190611c09565b828054828255906000526020600020906003028101928215611db45760005260206000209160030282015b82811115611db457825482546001600160a01b0319166001600160a01b03909116178255600180840154908301556002808401805485928592611da29291840191611cf2565b50505091600301919060030190611d5c565b50611a84929150611e53565b5080546000825560040290600052602060002090810190611de19190611e13565b50565b5080546000825590600052602060002090810190611de191905b5b80821115611a845760008155600101611dff565b80821115611a84576000611e278282611e88565b611e35600183016000611e88565b611e43600283016000611e88565b5060006003820155600401611e13565b80821115611a845780546001600160a01b0319168155600060018201819055611e7f6002830182611de4565b50600301611e53565b50805460018160011615610100020316600290046000825580601f10611eae5750611de1565b601f016020900490600052602060002090810190611de19190611dfe565b80356001600160a01b03811681146108ad57600080fd5b600082601f830112611ef3578081fd5b813567ffffffffffffffff811115611f0757fe5b611f1a601f8201601f19166020016125fa565b9150808252836020828501011115611f3157600080fd5b8060208401602084013760009082016020015292915050565b600080600080600060a08688031215611f61578081fd5b611f6a86611ecc565b9450602086013560028110611f7d578182fd5b9350604086013567ffffffffffffffff811115611f98578182fd5b611fa488828901611ee3565b9598949750949560608101359550608001359392505050565b600060208284031215611fce578081fd5b5035919050565b60008060408385031215611fe7578182fd5b8235915060208084013567ffffffffffffffff811115612005578283fd5b8401601f81018613612015578283fd5b80356120286120238261261e565b6125fa565b81815283810190838501858402850186018a1015612044578687fd5b8694505b8385101561206d5761205981611ecc565b835260019490940193918501918501612048565b5080955050505050509250929050565b6000806040838503121561208f578182fd5b8235915060208084013567ffffffffffffffff808211156120ae578384fd5b818601915086601f8301126120c1578384fd5b81356120cf6120238261261e565b81815284810190848601875b8481101561218c57813587016080818e03601f190112156120fa57898afd5b61210460806125fa565b8982013588811115612114578b8cfd5b6121228f8c83860101611ee3565b825250604082013588811115612136578b8cfd5b6121448f8c83860101611ee3565b8b8301525060608201358881111561215a578b8cfd5b6121688f8c83860101611ee3565b604083015250608091909101356060820152845292870192908701906001016120db565b50979a909950975050505050505050565b600080604083850312156121af578182fd5b8235915060208084013567ffffffffffffffff808211156121ce578384fd5b90850190606082880312156121e1578384fd5b6040516060810181811083821117156121f657fe5b60405261220283611ecc565b8152838301358482015260408301358281111561221d578586fd5b80840193505087601f840112612231578485fd5b823591506122416120238361261e565b82815284810190848601868502860187018b101561225d578788fd5b8795505b8486101561227f578035835260019590950194918601918601612261565b50604083015250949794965093945050505050565b60008282518085526020808601955080818302840101818601855b8481101561232957601f198684030189528151608081518186526122d582870182612336565b91505085820151858203878701526122ed8282612336565b915050604080830151868303828801526123078382612336565b60609485015197909401969096525050988401989250908301906001016122af565b5090979650505050505050565b60008151808452815b8181101561235b5760208185018101518683018201520161233f565b8181111561236c5782602083870101525b50601f01601f19169290920160200192915050565b6000815460018082166000811461239f57600181146123bd576123fb565b60028304607f16865260ff19831660208701526040860193506123fb565b600283048087526123cd8661263c565b60005b828110156123f15781546020828b01015284820191506020810190506123d0565b8801602001955050505b50505092915050565b6000602080830181845280855180835260408601915060408482028701019250838701855b8281101561245757603f19888603018452612445858351612336565b94509285019290850190600101612429565b5092979650505050505050565b6000602082526124776020830184612294565b9392505050565b6000604082526124916040830185612294565b828103602084810191909152845180835285820192820190845b81811015612329578451835293830193918301916001016124ab565b60208082528251828201819052600091906040908185019080840286018301878501865b8381101561255657603f19898403018552815160a08151855288820151818a87015261251982870182612336565b9150508782015188860152606080830151818701525060808083015192506002831061254157fe5b949094015293860193908601906001016124eb565b509098975050505050505050565b901515815260200190565b6000602082526124776020830184612336565b600060808252600e60808301526d02cb7ba903b37ba32b2103337b9160951b60a083015260c060208301526125ba60c0830185612381565b82810380604085015260018252600160fd1b6020830152604081016060850152506125e86040820185612381565b95945050505050565b90815260200190565b60405181810167ffffffffffffffff8111828210171561261657fe5b604052919050565b600067ffffffffffffffff82111561263257fe5b5060209081020190565b6000908152602090209056fea26469706673582212207549ba47edbd52c4525b4650296f532847da37ccfdc40ea779777eb4af9280cb64736f6c63430007050033";
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

    [Function("createElection")]
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

    public partial class GetElectionStringsFunction : GetElectionStringsFunctionBase { }

    [Function("getElectionStrings", "string[]")]
    public class GetElectionStringsFunctionBase : FunctionMessage
    {

    }

    public partial class GetElectoralListFunction : GetElectoralListFunctionBase { }

    [Function("getElectoralList", typeof(GetElectoralListOutputDTO))]
    public class GetElectoralListFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "electionId", 1)]
        public virtual BigInteger ElectionId { get; set; }
    }

    public partial class GetLastElectionIdFunction : GetLastElectionIdFunctionBase { }

    [Function("getLastElectionId", "uint256")]
    public class GetLastElectionIdFunctionBase : FunctionMessage
    {

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

    [Function("vote", "uint256")]
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

    public partial class GetElectionStringsOutputDTO : GetElectionStringsOutputDTOBase { }

    [FunctionOutput]
    public class GetElectionStringsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string[]", "", 1)]
        public virtual List<string> ReturnValue1 { get; set; }
    }

    public partial class GetElectoralListOutputDTO : GetElectoralListOutputDTOBase { }

    [FunctionOutput]
    public class GetElectoralListOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("tuple[]", "candidates", 1)]
        public virtual List<Candidate> Candidates { get; set; }
    }

    public partial class GetLastElectionIdOutputDTO : GetLastElectionIdOutputDTOBase { }

    [FunctionOutput]
    public class GetLastElectionIdOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetVoteOutputDTO : GetVoteOutputDTOBase { }

    [FunctionOutput]
    public class GetVoteOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }






}
